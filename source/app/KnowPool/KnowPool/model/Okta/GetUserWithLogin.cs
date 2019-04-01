using System;
namespace KnowPool.model.Okta
{
    public class UserProfile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string mobilePhone { get; set; }
        public string user_type { get; set; }
    }

    public class UserPassword
    {
    }

    public class RecoveryQuestion
    {
        public string question { get; set; }
    }

    public class UserProvider
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class UserCredentials
    {
        public UserPassword password { get; set; }
        public RecoveryQuestion recovery_question { get; set; }
        public UserProvider provider { get; set; }
    }

    public class UserResetPassword
    {
        public string href { get; set; }
    }

    public class UserResetFactors
    {
        public string href { get; set; }
    }

    public class UserExpirePassword
    {
        public string href { get; set; }
    }

    public class UserForgotPassword
    {
        public string href { get; set; }
    }

    public class UserChangeRecoveryQuestion
    {
        public string href { get; set; }
    }

    public class UserDeactivate
    {
        public string href { get; set; }
    }

    public class UserChangePassword
    {
        public string href { get; set; }
    }

    public class UserLinks
    {
        public UserResetPassword resetPassword { get; set; }
        public UserResetFactors resetFactors { get; set; }
        public UserExpirePassword expirePassword { get; set; }
        public UserForgotPassword forgotPassword { get; set; }
        public UserChangeRecoveryQuestion changeRecoveryQuestion { get; set; }
        public UserDeactivate deactivate { get; set; }
        public UserChangePassword changePassword { get; set; }
    }

    public class GetUserWithLogin
    {
        public string id { get; set; }
        public string status { get; set; }
        //public string created { get; set; }
        //public string activated { get; set; }
        //public string statusChanged { get; set; }
        //public string lastLogin { get; set; }
        //public string lastUpdated { get; set; }
        //public string passwordChanged { get; set; }
        public UserProfile profile { get; set; }
        //public UserCredentials credentials { get; set; }
        public UserLinks _links { get; set; }
    }
}
