using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student
{
    public class ParentController
    {

        internal Parent CreateParent(string stuFatherName, string stuFamilyName, string stuPhoneNumber, User u, string createdBy)
        {
            Parent p = new Parent();
            p.FName = stuFatherName;
            p.LName = stuFamilyName;
            p.PhoneNum = stuPhoneNumber;
            p.SysUser = u;
            p.kids_num++;
            p.Save(createdBy);

            return p;
        }
    }
}