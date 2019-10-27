using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
    public enum USERTYPE { PARENT = 0, ADMIN = 13, STUDENT = 1}
    public class User
    {
        public int ID;
        public USERTYPE Type;
        public string UserName;
        public string Password;
        QueryManager _manager = new QueryManager();

        public string Save()
        {
            ID = Int32.Parse( _manager.CreateUser(this));
            return ID.ToString();
        }
    }
}
