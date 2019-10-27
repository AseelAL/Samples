using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
   public class Parent : Person
    {
        public int kids_num;
        public string PhoneNum;
        public string FullName;
        QueryManager _manager = new QueryManager();

        public Parent()
        { }

        public Parent(int id)
        {
            ID = id;
        }

        public string Save(string createdBy)
        {
            ID = Int32.Parse(_manager.SaveParent(this, createdBy));
            return ID.ToString();
        }
    }
}
