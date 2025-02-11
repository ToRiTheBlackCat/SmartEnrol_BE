using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Constant
{
    public static class ConstantEnum
    {
        public static class Roles
        {
            public const string ADMIN = "Admin";
            public const string STUDENT = "Student";
            public const string STAFF = "Staff";
        }

        public enum RoleID
        {
            STUDENT = 3,
            ADMIN = 1
        }

    }
}
