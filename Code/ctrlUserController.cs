using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student
{
    public class UserController
    {

        internal User CreateUser(string stuUName)
        {
            User u = new User();
            u.UserName = stuUName;
            u.Password = "123";
            u.Type = USERTYPE.PARENT;
            u.ID = Int32.Parse(u.Save());

            return u;
        }
    }
}