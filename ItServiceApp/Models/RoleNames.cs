using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Models
{
    public static class RoleNames
    {
        public static string Admin = "Admin";
        public static string User = "User";

        public static List<string> Roles => new List<string>() { Admin, User };
    }
}
