using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class LoginRepo
    {
        string select;

        internal string selectLoginType(string name, string pass)
        {
            return select = "Select type From login where username='" + name + "' and password ='" + pass + "';";
        }

        internal string countRowLogin(string name, string pass, string type)
        {
            return select = "Select count(*) From login where username='" + name + "' and password ='" + pass + "'and type ='" + type + "';";
        }


        
    }
}
