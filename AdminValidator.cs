using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindergarten
{
    class AdminValidator
    {
        private static String login = "admin";
        private static String pswd = "1111";

        public static bool Validate(String login, String pswd)
        {
            if (login.Equals(AdminValidator.login) && pswd.Equals(AdminValidator.pswd))
                return true;
            else
                return false;
        }
    }
}
