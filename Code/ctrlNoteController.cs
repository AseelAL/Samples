using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student
{
    public class ctrlNoteController
    {
        public Note CreateNote(int NoteType, int TeacherID, int ClassID, int StudentID, string NoteDetails, string createrName, string NoteDate)
        {
            Note N = new Note();
            N.Type.ID = NoteType;
            N.NoteClass.ID = ClassID;
            N.NoteStudent.ID = StudentID;
            N.Description = NoteDetails;
            N.save(TeacherID, createrName);
            return N;
        }

    }
}