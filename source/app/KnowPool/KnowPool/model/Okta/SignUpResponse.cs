using System;
using System.Collections.Generic;

namespace KnowPool.model.Okta
{
    public class ProfileResponse
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string mobilePhone { get; set; }
        public string user_type { get; set; }
    }

    public class PasswordResponse
    {
    }

    public class Provider
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class CredentialsResponse
    {
        public PasswordResponse password { get; set; }
        public Provider provider { get; set; }
    }

    public class Activate
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Activate activate { get; set; }
    }

    public class SignUpResponse
    {
        public string id { get; set; }
        //public string status { get; set; }
        //public DateTime created { get; set; }
        //public object activated { get; set; }
        //public object statusChanged { get; set; }
        //public object lastLogin { get; set; }
        //public object lastUpdated { get; set; }
        //public object passwordChanged { get; set; }
        public ProfileResponse profile { get; set; }
        //public CredentialsResponse credentials { get; set; }
        public Links _links { get; set; }
    }
}
