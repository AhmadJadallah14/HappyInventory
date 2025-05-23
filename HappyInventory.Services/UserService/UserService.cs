﻿using AutoMapper;
using HappyInventory.Data.Context;
using HappyInventory.Helpers.AppConstant;
using HappyInventory.Helpers.Enum;
using HappyInventory.Helpers.Helper;
using HappyInventory.Models.DTOs.User;
using HappyInventory.Models.Models.Identity;
using HappyInventory.Models.PaginationHelper;
using HappyInventory.Models.Response;
using HappyInventory.Services.JwtService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace HappyInventory.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            ApplicationDbContext context,
            IJwtService jwtService,
            IMapper mapper ,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _jwtService = jwtService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto loginDto)
        {

            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return new ApiResponse<string>(false, null,
                    ResponseMessages.ErrorInvalidCredentials, null, HttpStatusCode.BadRequest);
            }

            var user = await GetUserByEmailAsync(loginDto.Email);
            if (user == null || !VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                return new ApiResponse<string>(false, null,
                    ResponseMessages.ErrorInvalidCredentials, null, HttpStatusCode.Unauthorized);
            }

            if (!user.IsActive)
            {
                return new ApiResponse<string>(false, null, ResponseMessages.YourAccountIsDisabled,
                    null, HttpStatusCode.Forbidden);
            }

            var token = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, token, ResponseMessages.SuccessLogin, null, HttpStatusCode.OK);
        }
        public async Task<ApiResponse<string>> CreateNewUser(NewUserDto dto)
        {
            var userExists = await GetUserByEmailAsync(dto.Email);
            if (userExists != null)
                return new ApiResponse<string>(false, null, ResponseMessages.UserAlreadyExist,
                    null, HttpStatusCode.BadRequest);

            if (!EmailValidation.IsValidEmail(dto.Email))
                return new ApiResponse<string>(false, null, ResponseMessages.InvalidEmailFormat,
                    null, HttpStatusCode.BadRequest);


            var user = _mapper.Map<User>(dto);
            user.PasswordHash = Encryption.HashPassword(dto.Password);
            user.IsActive = true;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, ResponseMessages.SuccessUserCreated,
                ResponseMessages.SuccessUserCreated, null, HttpStatusCode.Created);
        }

        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user is null)
                return new ApiResponse<UserDto>(false, null, ResponseMessages.ErrorUserNotFound,
                    null, HttpStatusCode.NotFound);

            var userDto = _mapper.Map<UserDto>(user);

            return new ApiResponse<UserDto>(true, userDto, ResponseMessages.SuccessUserFetched,
                null, HttpStatusCode.OK);
        }

        public async Task<ApiResponse<PaginatedResponse<UserDto>>> GetAllUsersAsync(int pageIndex, int pageSize)
        {
            var query = _context.Users.AsQueryable().OrderByDescending(u =>  u.Id);

            var paginatedResult = await PaginationHelper.GetPaginatedResultAsync(query, pageIndex, pageSize);

            var userDtos = _mapper.Map<List<UserDto>>(paginatedResult.Data);

            return new ApiResponse<PaginatedResponse<UserDto>>(
                true,
                new PaginatedResponse<UserDto>
                {
                    Data = userDtos,
                    TotalRecords = paginatedResult.TotalRecords,
                    TotalPages = paginatedResult.TotalPages,
                    CurrentPage = paginatedResult.CurrentPage,
                    PageSize = paginatedResult.PageSize
                },
                ResponseMessages.SuccessUserFetched,
                null,
                HttpStatusCode.OK
            );
        }


        public async Task<ApiResponse<string>> EditUser(UpdateUserDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == dto.Id);
            if (user == null)
                return new ApiResponse<string>(false, null,
                    ResponseMessages.ErrorUserNotFound, null, HttpStatusCode.NotFound);

            if (!EmailValidation.IsValidEmail(dto.Email))
                return new ApiResponse<string>(false, null, ResponseMessages.InvalidEmailFormat,
                    null, HttpStatusCode.BadRequest);

            _mapper.Map(dto, user);

            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, ResponseMessages.UserUpdatedSuccessfully,
                ResponseMessages.UserUpdatedSuccessfully, null, HttpStatusCode.OK);
        }

        public async Task<ApiResponse<string>> DeleteUser(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return new ApiResponse<string>(false, null, ResponseMessages.ErrorUserNotFound,
                    null, HttpStatusCode.NotFound);

            if (user.Role == UserRole.Admin)
                return new ApiResponse<string>(false, null,
                    ResponseMessages.AdminUserCannotDeleted, null, HttpStatusCode.BadRequest);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, ResponseMessages.UserDeletedSuccessfully,
                ResponseMessages.UserDeletedSuccessfully, null, HttpStatusCode.OK);
        }

        public async Task<ApiResponse<string>> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == dto.UserId);
            if (user is null)
                return new ApiResponse<string>(false, null,
                    ResponseMessages.ErrorUserNotFound, null, HttpStatusCode.NotFound);

            user.PasswordHash = Encryption.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, ResponseMessages.PasswordChangedSuccessfully,
                ResponseMessages.PasswordChangedSuccessfully, null, HttpStatusCode.OK);
        }


        #region PRIVATE METHODS

        private async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }

        private bool VerifyPassword(string storedHash, string password)
        {
            string hashedPassword = Encryption.HashPassword(password);
            return storedHash.Equals(hashedPassword);
        }
        public string GetCurrentUserEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                return null;

            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public string GetCurrentUserFullName()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                return string.Empty;  

            var fullNameClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return fullNameClaim?.Value ?? string.Empty;  
        }
        #endregion
    }



}

