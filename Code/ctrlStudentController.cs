using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student
{
    public class StudentController
    {
        public string CreateStudent(string stuName, string stuFamilyName, string stuFatherName, string stuGFName, string stuPhoneNumber, string stuUName, string classID, string createdBy)
        {
            string CreatedUserID = "-1";
            Student st = new Student();
            st.FName = stuName;
            st.LName = stuFamilyName;
            st.FatherName = stuFatherName;
            st.GFatherName = stuGFName;
            st.PhoneNum = stuPhoneNumber;
            string createruser = createdBy;

            UserController uController = new UserController();
            User parentUSer = uController.CreateUser(stuUName);
            if (parentUSer.ID != -1)
            {
                ParentController pController = new ParentController();
                Parent stuParent = pController.CreateParent(stuFatherName, stuFamilyName, stuPhoneNumber, parentUSer, createdBy);

                if (stuParent.ID != -1)
                {
                    st.StudentParent = stuParent;
                    CreatedUserID = st.Save(createruser);
                    //CreatedUserID = _manager.SaveStudent(st, createruser);
                    //if (_manager.SaveStudentClass(CreatedUserID, classID))
                    if (st.SaveStudentClass(classID))
                    {
                        // Throw warning TODO
                    }
                }
            }

            return CreatedUserID;
        }
    }
}