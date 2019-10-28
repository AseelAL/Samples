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
        QueryManager _manager = new QueryManager();

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

        internal void save(int TeacherID, string createrName)//,string NoteDate)
        {
            //throw new NotImplementedException();
            ID = Int32.Parse(_manager.SaveNote(this, TeacherID, createrName));//,NoteDate));
        }
    }
}
