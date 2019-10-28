using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student
{
    public class ctrlNoteController
    {
        public Note CreateNote(int NoteType, int TeacherID, int ClassID, int StudentID, string NoteDetails, string createrName, DateTime NoteDate)
        {
            Note N = new Note();
            N.Type = new NoteType();
            N.NoteClass = new ClassRoom();
            N.NoteStudent = new Student();
            N.Date = new DateTime();
            N.Type.ID = NoteType;
            N.NoteClass.ID = ClassID;
            N.NoteStudent.ID = StudentID;
            N.Description = NoteDetails;
            N.Date= NoteDate;
            N.save(TeacherID, createrName);
            return N;
        }

    }
}