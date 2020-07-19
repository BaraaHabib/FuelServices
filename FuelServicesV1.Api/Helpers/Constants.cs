using System.Collections.Generic;

namespace FuelServices.Api.Helpers
{
    public static class Constants
    {
        public static string SUCCESS = "Success";
        public static int SUCCESS_CODE = 1;

        public static string TWO_FACTORS_AUTH = "Login with two factors is needed";
        public static int TWO_FACTORS_AUTH_CODE = 2;

        public static string NOT_FOUND = "Not found";
        public static int NOT_FOUND_CODE = -1;

        public static string BAD_REQUEST = "Bad request";
        public static int BAD_REQUEST_CODE = -2;

        public static string SOMETHING_WRONG = "Something went wrong";
        public static int SOMETHING_WRONG_CODE = -3;

        public static string LOCKOUT = "Lockout";
        public static int LOCKOUT_CODE = -4;

        public static string INVALID_LOGIN = "Invalid login attempt";
        public static int INVALID_LOGIN_CODE = -5;

        public static string NOT_VERIFIED = "User account not verified, please check your email for verification code";
        public static int NOT_VERIFIED_CODE = -6;

        public static string Access_Denied = "Access denied";
        public static int Access_Denied_CODE = -7;

        public static string INVALID_INPUT = "Invalide Input";
        public static int INVALID_INPUT_CODE = -8;
        
        public static string RESET_PASSWORD_ERR = "Invalide Reset Password Attempt";
        public static int RESET_PASSWORD_ERR_CODE = -9;

        public static string CONFIRMATION_EMAIL_TYPE = "confirm_mail";
        public static string RESET_PASSWORD_EMAIL_TYPE = "reset_password";
        public static string DELETE_ACCOUNT_EMAIL_TYPE = "delete_account";
        
        public static partial  class LogTemplates
        {
            public static string LOGOUT = "Logout";
            public static string LOGIN = "Login";
            public static string LOGIN_ERROR = "Login Error";
            public static string LOGIN_ERROR_EX = "Login Exception";
            public static string REQUESTS_ERROR_EX = "REQUESTS Exception";
            public static string CONFIRM_ACCOUNT_EX = "CONFIRM ACCOUNT EXCEPTION";
            public static string RESET_PASSWORD_EX = "RESET PASSWORD EXCEPTION";
        }
        public static List<string> ContentTypes = new List<string>()
        {
            "about_us",
            "our_services",
            "web_news",
            "mobile_news",
        };

        public static List<string> Units
        {
            get => new List<string>() { "$", "€", "USD/USG" };
        }
        public static List<string> Roles
        {
            get => new List<string>
            {
                "Admin",
                "Customer",
                "Supplier",
                "Handler"
            };
        }
    }
    
}
