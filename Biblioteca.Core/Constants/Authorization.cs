using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Core.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            Dev,
            Employee,
            Client
        }


        public const string default_username = "dev";
        public const string default_email = "dev@secureapi.com";
        public const string default_password = "Atec123_*";
        public const Roles default_role = Roles.Dev;
    }
}
