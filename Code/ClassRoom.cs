using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
   public class ClassRoom
    {
        public int ID;
        public string Name;
        public string Desc;
        public int Number;

        public ClassRoom()
        {

        }


        public ClassRoom(int id)
        { 
        ID=id;
        }

        public override string ToString()
        {
            return ID.ToString() + ":" + Name + " " + Number.ToString();
        }

    }
}
