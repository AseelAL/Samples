using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
    public abstract class Person
    {
        public int ID;
        public string FName;
        public string LName;

        public User SysUser;

        public override string ToString()
        {
            return ID.ToString() + ":" + FName + " " + LName;
        }
    }
}
