using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using DBContext.Models;
using System;

namespace Site.Helpers
{
    public static class Constants
    {
        public static string SUCCESS = "success";
        public static int SUCCESS_CODE = 1;

        public static string ERROR = "error";
        public static int ERROR_CODE = -7;

        public static string WARNING = "warning";
        public static int WARNING_CODE = -8;

        public static string INFO = "info";
        public static int INFO_CODE = 2;

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
        
        public static string PAYMENT_ERROR = "Payment error";
        public static int PAYMENT_ERROR_CODE = -7;
        
        public static string EMAIL_FAILED_TO_DELIVER = "Failed to deliver email";
        public static int EMAIL_FAILED_TO_DELIVER_CODE = -8;

        public static string CONFIRMATION_EMAIL_TYPE = "confirm_mail";
        public static string RESET_PASSWORD_EMAIL_TYPE = "reset_password";
        public static string DELETE_ACCOUNT_EMAIL_TYPE = "delete_account";
        public static string SUPPLLIER_REQUEST_NOTIFICATION = "supplier_request_notification";

        public static List<string> ContentTypes = new List<string>()
        {
            "what_we_offer",
            "news",
            "Privacy"
        };

        public static List<string> Units
        {
            get => new List<string>() {  "USD" };
        }
        public static List<string> Roles
        {
            get => new List<String>
            {
                "Admin",
                "Customer",
                "Supplier",
                "Handler"
            };
        }

    }
}
