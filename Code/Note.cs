using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student
{
  public class Note

    {

        public int ID;
        public NoteType Type; 
        public string Description;
        public string DocPath;
        public DateTime Date;
        public Student NoteStudent;
        public ClassRoom NoteClass;

        public Note()
        { }

        public Note(int id)
        {
            ID = id;
        }

        public string FormattedDate
        {
            get
            {
                return Date.ToShortDateString();
            }
        }
    }
}
