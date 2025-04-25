using System.Threading.Channels;

namespace HappyInventory.Helpers.AppConstant
{
    public static class ResponseMessages
    {
        // Success messages
        public const string SuccessLogin = "Login successful";
        public const string SuccessTokenRefresh = "Token refreshed successfully";
        public const string SuccessUserCreated = "User created successfully";
        public const string SuccessUserFetched = "User fetched successfully";
        public const string PasswordChangedSuccessfully = "Password changed successfully";
        public const string UserUpdatedSuccessfully = "User updated successfully";
        public const string UserDeletedSuccessfully = "User deleted successfully";

        // Error messages
        public const string ErrorInvalidCredentials = "Invalid credentials";
        public const string ErrorInvalidRefreshToken = "Invalid refresh token";
        public const string ErrorUserNotFound = "User not found";
        public const string ErrorFailedToCreateUser = "Failed to create user";
        public const string UserAlreadyExist = "User Already Exist";


        // Other messages
        public const string YourAccountIsDisabled = "Your account is disabled. Please contact support";
        public const string AdminUserCannotDeleted = "Admin User Cannot be deleted";
        public const string InvalidEmailFormat = "Invalid email format";
    }
}
