using DBContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelServices.DBContext.Models
{
    public class ResetPasswordToken : BaseEntity
    {

        public string ResetPasswordCode { get; set; }

        public DateTime ResetPasswordCodeValidityEndDate { get; set; }


        public ResetPasswordTokenStatus Status { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
    public enum ResetPasswordTokenStatus
    {
        Validated,
        Expired,
        PendingValidation
    }
    public static class ResetPasswordConstants
    {
        /// <summary>
        /// token validity (minites)
        /// </summary>
        public static int Validity = 30;   

    }

}
