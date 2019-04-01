using System;
namespace KnowPool.model.Okta
{
    public class Profile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string mobilePhone { get; set; }
        public string user_type { get; set; }
    }

    public class Password
    {
        public string value { get; set; }
    }

    public class Credentials
    {
        public Password password { get; set; }
    }

    public class SignUpUser
    {
        public Profile profile { get; set; }
        //public Credentials credentials { get; set; }
    }
}
