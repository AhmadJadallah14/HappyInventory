namespace HappyInventory.Helpers.AppConstant
{
    public static class ResponseMessages
    {
        // Success messages
        public const string SuccessLogin = "Login successful.";
        public const string SuccessTokenRefresh = "Token refreshed successfully.";
        public const string SuccessUserCreated = "User created successfully.";
        public const string SuccessUserFetched = "User fetched successfully.";

        // Error messages
        public const string ErrorInvalidCredentials = "Invalid credentials.";
        public const string ErrorInvalidRefreshToken = "Invalid refresh token.";
        public const string ErrorUserNotFound = "User not found.";
        public const string ErrorFailedToCreateUser = "Failed to create user.";
    }
}
