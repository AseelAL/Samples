using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
    public class Teacher : Person
    {
        public string CreatedBy;

       public  Teacher()
        {
        }

       public Teacher(int id)
        {
            id = ID;
        }

       public override string ToString()
       {
           return ID.ToString() + ":" + FName + "       " + CreatedBy;
       }

    }
}
