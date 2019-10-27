using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
    public class Student : Person
    {
        public string FatherName;

        public string GFatherName;
        public string PhoneNum;
        public string FullName;
        public string ClassDesc;


        public Parent StudentParent;
        //public List<Note> StudentNotes;
        QueryManager _manager = new QueryManager();

        public Student()
        {
            //StudentNotes = new List<Note>();
        }

        public Student(int id)
        {
            ID = id;
            //StudentNotes = new List<Note>();
        }

        public string Save(string CreatedBy)
        {
            ID = Int32.Parse(_manager.SaveStudent(this, CreatedBy));
            return ID.ToString();
        }

        public bool SaveStudentClass(string classID)
        {
            return _manager.SaveStudentClass(ID.ToString(), classID);
        }
    }

    public class StudentContainer
    {
        public Student aStudent;
        public List<Note> Notes;
    }
}
