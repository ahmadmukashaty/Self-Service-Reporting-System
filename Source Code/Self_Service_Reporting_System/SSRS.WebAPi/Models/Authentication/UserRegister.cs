using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Authentication
{
    public class UserRegister
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string department { get; set; }
    }
}