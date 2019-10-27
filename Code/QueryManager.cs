using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student
{
    public class QueryManager
    {
        static DBManager DBMgr = new DBManager();
        public SymCryptography CryptManager = new SymCryptography();


        public void TestSqlDB()
        {
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            aConnection.Open();
        }



///SAVE
        public string SaveStudent(Student aStudent,string createrName)
        {
            string Result = "-1";

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();                
                SqlCommand myCmnd = aConnection.CreateCommand();
                myCmnd.CommandText = @"insert into Student ([First Name], [Last Name], [Father Name],[GrandFather Name],[Phone Number], [Parent ID], [Created by], [Created Date]) values ('" + aStudent.FName +
                            @"', '" + aStudent.LName +
                            @"', '" + aStudent.FatherName +
                            @"', '" + aStudent.GFatherName +
                            @"', '" + aStudent.PhoneNum +
                            @"', " + aStudent.StudentParent.ID +
                            @", '" + createrName +
                            @"', GETDATE()); select top 1 ID from Student order by [Created Date] desc";

                SqlDataReader aReader = myCmnd.ExecuteReader();
                if (aReader.Read())
                {
                    Result = aReader["ID"].ToString();
                }
                aReader.Close();
            }
            catch (SqlException se)
            {
                Result = "-1";
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return Result;
        }


        public string SaveParent(Parent p, string createrName)
        {
            string Result = "-1";           
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                myCmnd.CommandText = @"insert into Parents ([Name],[Last Name],[Phone], [Kids num], [User ID], [Created by], [Created Date]) values ('" + p.FName +
                    @"', '" + p.LName +
                    @"', '" + p.PhoneNum +
                    @"'," + p.kids_num +
                    @", " + p.SysUser.ID +
                    @", '" + createrName +
                    @"', GETDATE()); select top 1 ID from Parents order by [Created Date] desc";


                SqlDataReader aReader = myCmnd.ExecuteReader();
                if (aReader.Read())
                {
                    Result = aReader["ID"].ToString();
                }
                aReader.Close();
            }
            catch (SqlException se)
            {
                Result = "-1";
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return Result;
        }


        public string SaveNote(int NoteType, int TeacherID, int ClassID, int StudentID, string NoteDetails, string createrName, string NoteDate)
        {
            string Result = "-1";
            string studentIdValue = StudentID.ToString();
            if (StudentID == -1)
                studentIdValue = "NULL";
           
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                myCmnd.CommandText = @"insert into Note ([Type],[TeacherID],[ClassID],[StudentID],[Description],[Created by], [Note_Date], [Created Date]) values (" + NoteType +
                      @", " + TeacherID +
                     @", " + ClassID +
                     @", " + studentIdValue +
                     ", '" + NoteDetails +
                    @"', '" + createrName +
                    @"', '"+  NoteDate+
                    @"', GETDATE()); select top 1 ID from Note order by [Created Date] desc";


                SqlDataReader aReader = myCmnd.ExecuteReader();
                if (aReader.Read())
                {
                    Result = aReader["ID"].ToString();
                }
                aReader.Close();
            }
            catch (SqlException se)
            {
                Result = "-1";
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return Result;
        }


        public bool SaveNoteType(NoteType NT)
        {
            bool Result = true;

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"insert into NoteType (Description)values('" + NT.TypeOFNote + "')";

                myCmnd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                Result = false;
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return Result;
        }

        public bool SaveStudentClass(string StudentID, string ClassID)
        {

            bool Result = false;
            HttpCookie userCookies = new HttpCookie("userCookies");
            string userName = userCookies.Name;

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                myCmnd.CommandText = @"insert into Student_Class ([StudentID],[ClassID], [From Date],[Created by],[Created Date]) values (" + StudentID +
                     "," + ClassID +
                     ", GETDATE() " +
                     "," + "'" + userName + "'" +
                     ", GETDATE())";


                myCmnd.ExecuteNonQuery();

            }
            catch (SqlException se)
            {
                Result = false;
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return Result;
        }

///GET
        public List<NoteType> GetNotes()
        {
            List<NoteType> notes = new List<NoteType>();
            SqlConnection aConnection = DBMgr.GetSQLConnection();

            try
            {

                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select * from NoteType";

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    NoteType n = new NoteType();

                    n.ID = Int32.Parse(aReader["ID"].ToString());
                    n.TypeOFNote = aReader["Description"].ToString();


                    notes.Add(n);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return notes;
        }


        public List<ClassRoom> GetClasses()
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();
            SqlConnection aConnection = DBMgr.GetSQLConnection();

            try
            {

                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select ID, Name, Number, Name + '-' + cast (Number as nvarchar) as Description from Class";

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    ClassRoom cRooms = new ClassRoom();
                    cRooms.ID = Int32.Parse(aReader["ID"].ToString());
                    cRooms.Name = aReader["Name"].ToString();
                    cRooms.Desc = aReader["Description"].ToString();
                    cRooms.Number = Int32.Parse(aReader["Number"].ToString());


                    classRooms.Add(cRooms);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return classRooms;
        }


        public List<ClassRoom> GetClassByTeacher(int teacherID)
        {
            List<ClassRoom> cRoom = new List<ClassRoom>();

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                ClassRoom c = new ClassRoom();

                myCmnd.CommandText = @"select *  from Class  inner join Class_Teacher On Class.ID = Class_Teacher.ClassID  where TeacherID=" + teacherID.ToString();

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {

                    c.ID = Int32.Parse(aReader["ID"].ToString());
                    c.Name = aReader["Name"].ToString();
                    c.Number = Int32.Parse(aReader["Number"].ToString());


                    cRoom.Add(c);

                }

                aReader.Close();

            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return cRoom;
        }


        public List<Teacher> GetTeacher()
        {


            List<Teacher> teachers = new List<Teacher>();

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select * from Teacher";
                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    Teacher ateacher = new Teacher();
                    ateacher.ID = Int32.Parse(aReader["ID"].ToString());
                    ateacher.FName = aReader["Name"].ToString();
                    ateacher.CreatedBy = aReader["Created By"].ToString();


                    teachers.Add(ateacher);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
              
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return teachers;
        }

        
        public List<Student> GetStudents()
        {            
            List<Student> studends = new List<Student>();

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select * from Student";
                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    Student aStudent = new Student();
                    aStudent.ID = Int32.Parse(aReader["ID"].ToString());
                    aStudent.FName = aReader["First Name"].ToString();
                    aStudent.LName = aReader["Last Name"].ToString();
                    aStudent.FatherName = aReader["Father Name"].ToString();

                    studends.Add(aStudent);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return studends; 
        }

        public List<Student> GetStudentsBySysUser(string UserID)
        {
            User aUser = GetUserByID(UserID);
            List<Student> students = new List<Student>();
            if (aUser.Type == USERTYPE.ADMIN)
                students = GetAllStudents();
            else if (aUser.Type == USERTYPE.PARENT)
                students = GetStudentsByParent(aUser.ID);

            return students;


            
        }

        private List<Student> GetStudentsByParent(int parentID)
        {

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            List<Student> students = new List<Student>();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select 
	                Student.id
	                , Student.[First Name]
	                , Student.[Last Name]
	                ,Student.[Father Name]
	                ,Class.Name + '-' + cast(Class.Number as nvarchar) as [Class]
                from Student 
                inner join Student_Class on Student_Class.StudentID = Student.ID
                inner join Class on Student_Class.ClassID = Class.ID
                inner join Parents on Parents.ID=Student.[Parent ID]
                where [Parent ID] is not null
                And Parents.[User ID]=" + parentID.ToString();

 
                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    Student aStudent = new Student();
                    aStudent.ID = Int32.Parse(aReader["ID"].ToString());
                    aStudent.FName = aReader["First Name"].ToString();
                    aStudent.LName = aReader["Last Name"].ToString();
                    aStudent.FatherName = aReader["Father Name"].ToString();
                    aStudent.ClassDesc = aReader["Class"].ToString();

                    students.Add(aStudent);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return students;
        }

 

        private List<Student> GetAllStudents()
        {
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            List<Student> students = new List<Student>();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select 
	                                        Student.id
	                                        , Student.[First Name]
	                                        , Student.[Last Name]
	                                        ,Student.[Father Name]
	                                        ,Class.Name + '-' + cast(Class.Number as nvarchar) as [Class]
                                        from Student 
                                        inner join Student_Class on Student_Class.StudentID = Student.ID
                                        inner join Class on Student_Class.ClassID = Class.ID
                                        where [Parent ID] is not null";
                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    Student aStudent = new Student();
                    aStudent.ID = Int32.Parse(aReader["ID"].ToString());
                    aStudent.FName = aReader["First Name"].ToString();
                    aStudent.LName = aReader["Last Name"].ToString();
                    aStudent.FatherName = aReader["Father Name"].ToString();
                    aStudent.ClassDesc = aReader["Class"].ToString();

                    students.Add(aStudent);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return students;
        }

        public User GetUserByID(string UserID)
        {
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            User aUser = new User();

            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                myCmnd.CommandText = @"  select * from [Sys_User] where ID= " + UserID;
                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    aUser.ID = Int32.Parse(aReader["ID"].ToString());
                    aUser.UserName = aReader["User Name"].ToString();
                    aUser.Type = (USERTYPE)aReader["Type"];
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return aUser;
        }



        public StudentContainer GetSTDINFO(string stdID, string UserID)
        {
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            StudentContainer aStuCont = new StudentContainer();
            aStuCont.aStudent = new Student();
            aStuCont.Notes = new List<Note>();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                
                string query = @"(select Note.ID
	                                    , Note.Description
	                                    , Note.[Note_date]
                                    	, NoteType.ID as TypeID
	                                    , NoteType.Description as Type
	                                    , Student.id as StudID
	                                    , Student.[First Name]
	                                    , Student.[Last Name] 
                                        , Student.[Father Name] 
	                                    , Student.[GrandFather Name]
	                                    , Student.[First Name] + ' ' + Student.[Last Name] as [Full Name]
	                                    ,' '  as [ClassD]
                                        ,Sys_User.[User Name]

                                    from Note
                                    inner join NoteType On NoteType.ID = Note.Type
                                    inner join Class On Class.ID = Note.ClassID
                                    inner join Student_Class On Class.ID = Student_Class.ClassID
                                    inner join Student on Note.StudentID = Student.ID
                                    inner join Parents on Parents.ID = Student.[Parent ID]
                                    inner join Sys_User on Sys_User.ID = Parents.[User ID] where Student.ID =" + stdID + " AND (Sys_User.ID = " + UserID + " OR (select [TYPE] from Sys_User SU where SU.ID = " + UserID + " ) = 13) )";

                query += @" union (
                            select Note.ID
	                            , Note.Description
	                            , Note.[Note_date]
	                            , NoteType.ID as TypeID
	                            , NoteType.Description as Type
	                            , Student.id
	                            , Student.[First Name]
	                            , Student.[Last Name] 
	                            , Student.[Father Name] 
	                            , Student.[GrandFather Name] 
	                            , Student.[First Name] + ' ' + Student.[Last Name] as [Full Name]
	                            ,Class.Name + '-' + cast(Class.Number as nvarchar) as [ClassD]
                                ,Sys_User.[User Name]
                                
                            from Note
                            inner join NoteType On NoteType.ID = Note.Type
                            inner join Class On Class.ID = Note.ClassID
                            inner join Student_Class On Class.ID = Student_Class.ClassID
                            inner join Student on Student.ID = Student_Class.StudentID
                            inner join Parents on Parents.ID = Student.[Parent ID]
                            inner join Sys_User on Sys_User.ID = Parents.[User ID]
                            where Note.StudentID is null and Student_Class.StudentID =" + stdID + " AND (Sys_User.ID = " + UserID + " OR (select [TYPE] from Sys_User SU where SU.ID = " + UserID + " ) = 13))";


                myCmnd.CommandText = query;

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                 {
                     Note aNote = new Note();
                    aNote.ID = Int32.Parse(aReader["ID"].ToString());
                    aNote.Description = aReader["Description"].ToString();
                    aNote.Date = DateTime.Parse(aReader["Note_date"].ToString());

                    NoteType aNoteType = new NoteType();
                    aNoteType.ID = Int32.Parse(aReader["TypeID"].ToString());
                    aNoteType.TypeOFNote = aReader["Type"].ToString();
                    aNote.Type = aNoteType;

                    if (aStuCont.aStudent.ID <= 0)
                    {
                        aStuCont.aStudent.ID = Int32.Parse(aReader["StudID"].ToString());
                        aStuCont.aStudent.FName = aReader["First Name"].ToString();
                        aStuCont.aStudent.FatherName = aReader["Father Name"].ToString();
                        aStuCont.aStudent.LName = aReader["Last Name"].ToString();
                        aStuCont.aStudent.GFatherName = aReader["GrandFather Name"].ToString();
                        aStuCont.aStudent.FullName = aReader["Full Name"].ToString();
                        aStuCont.aStudent.ClassDesc = aReader["ClassD"].ToString();
                    }
                    aNote.NoteStudent = aStuCont.aStudent;

                    aStuCont.Notes.Add(aNote);
                }

                aReader.Close();

                if (aStuCont.aStudent.ID <= 0)
                {
                    myCmnd.CommandText = @"select 
	                            Student.id
	                            , Student.[First Name]
	                            , Student.[Last Name] 
	                            , Student.[Father Name] 
	                            , Student.[GrandFather Name] 
	                            , Student.[First Name] + ' ' + Student.[Last Name] as [Full Name]
	                            ,Class.Name + '-' + cast(Class.Number as nvarchar) as [ClassD]
                        from Student
                            inner join Student_Class On Student.ID = Student_Class.StudentID
                            inner join Class On Class.ID = Student_Class.ClassID
                            inner join Parents on Parents.ID = Student.[Parent ID]
                            inner join Sys_User on Sys_User.ID = Parents.[User ID]
                        Where Student.id = " + stdID + " AND (Sys_User.ID = " + UserID + " OR (select [TYPE] from Sys_User SU where SU.ID = " + UserID + " ) = 13)";
                }

                
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return aStuCont;
        }

        internal List<Note> GetNotesBYUser(string UserID)
        {
            User aUser = GetUserByID(UserID);
            SqlConnection aConnection = DBMgr.GetSQLConnection();
            List<Note> notes = new List<Note>();
            
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
              
                string query = @"(select Note.ID
	                                    , Note.Description
	                                    , Note.[Note_date]
                                    	, NoteType.ID as TypeID
	                                    , NoteType.Description as Type
	                                    , Student.id as StudID
	                                    , Student.[First Name]
	                                    , Student.[Last Name] 
	                                    , Student.[First Name] + ' ' + Student.[Last Name] as [Full Name]
	                                    ,' '  as [Class]

                                    from Note
                                    inner join NoteType On NoteType.ID = Note.Type
                                    inner join Class On Class.ID = Note.ClassID
                                    inner join Student_Class On Class.ID = Student_Class.ClassID
                                    inner join Student on Note.StudentID = Student.ID
                                    inner join Parents on Parents.ID = Student.[Parent ID]
                                    inner join Sys_User on Sys_User.ID = Parents.[User ID]";

                if (aUser.Type != USERTYPE.ADMIN)
                    query += " where Sys_User.ID =" + UserID + ")";
                else
                    query += ") ";

                query += @" union (
                            select Note.ID
	                            , Note.Description
	                            , Note.[Note_date]
	                            , NoteType.ID as TypeID
	                            , NoteType.Description as Type
	                            , Student.id
	                            , Student.[First Name]
	                            , Student.[Last Name] 
	                            , Student.[First Name] + ' ' + Student.[Last Name] as [Full Name]
	                            ,Class.Name + '-' + cast(Class.Number as nvarchar) as [Class]
                                
                            from Note
                            inner join NoteType On NoteType.ID = Note.Type
                            inner join Class On Class.ID = Note.ClassID
                            inner join Student_Class On Class.ID = Student_Class.ClassID
                            inner join Student on Student.ID = Student_Class.StudentID
                            inner join Parents on Parents.ID = Student.[Parent ID]
                            inner join Sys_User on Sys_User.ID = Parents.[User ID]
                            where Note.StudentID is null";

                if (aUser.Type != USERTYPE.ADMIN)
                    query += " and Sys_User.ID =" + UserID + ")";
                else
                    query += ") ";


                myCmnd.CommandText = query;

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                 {

                    Note aNote = new Note();
                    aNote.ID = Int32.Parse(aReader["ID"].ToString());
                    aNote.Description = aReader["Description"].ToString();
                    aNote.Date = DateTime.Parse(aReader["Note_date"].ToString());

                    NoteType aNoteType = new NoteType();
                    aNoteType.ID = Int32.Parse(aReader["TypeID"].ToString());
                    aNoteType.TypeOFNote = aReader["Type"].ToString();
                    aNote.Type = aNoteType;

                    Student aStudent = new Student();
                    aStudent.ID = Int32.Parse(aReader["StudID"].ToString());
                    aStudent.FName = aReader["First Name"].ToString();
                    aStudent.LName = aReader["Last Name"].ToString();
                    aStudent.FullName = aReader["Full Name"].ToString();
                    aStudent.ClassDesc = aReader["Class"].ToString();
                    aNote.NoteStudent = aStudent;

                    notes.Add(aNote);
                }

                aReader.Close();
                

                
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return notes;
        }

//        public string GetClassName4Student(int std_id)
//        {
//            string theClassName;
//            SqlConnection aConnection = DBMgr.GetSQLConnection();
//            try
//            {
//                aConnection.Open();
//                SqlCommand myCmnd = aConnection.CreateCommand();
                
//                myCmnd.CommandText = @"Select  Name
//                                        from Class  inner join Student_Class On Class.ID = Student_Class.ClassID  where StudentID=" + std_id.ToString();

//                SqlDataReader aReader = myCmnd.ExecuteReader();
//                theClassName = aReader["Name"].ToString();

             
//                aReader.Close();

//            }
//            catch (SqlException se)
//            {
//                aConnection.Close();
//                throw (se);
//            }

//            aConnection.Close();

//            return theClassName;
//        }


        public List<Student> GetStudentsByClass(int classID)
        {
            List<Student> studends = new List<Student>();

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                
//myCmnd.CommandText = @"select *  from Student  inner join Student_Class On Student.ID = Student_Class.StudentID  where ClassID=" + classID.ToString();
                myCmnd.CommandText = @"Select ID,[First Name],[Last Name], [Father Name], [First Name] + ' ' + [Last Name] as Description
                                        from Student  inner join Student_Class On Student.ID = Student_Class.StudentID  where ClassID=" + classID.ToString();

                /*
                 
                 @"SELECT Parents.ID as ID,
	                                    Name,
	                                    [Last Name],
                                        Name + ' ' + [Last Name] as FullName,
	                                    Sys_User.ID as UserID,
	                                    Sys_User.[User Name]
      
                                      FROM [Parents]
                                      inner join Sys_User on Sys_User.ID = Parents.[User ID]";

                 */
                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {

                    Student aStudent = new Student();
                    aStudent.ID = Int32.Parse(aReader["ID"].ToString());
                    aStudent.FName = aReader["First Name"].ToString();
                    aStudent.LName = aReader["Last Name"].ToString();
                    aStudent.FatherName = aReader["Father Name"].ToString();
                    aStudent.FullName = aReader["Description"].ToString(); 

                    studends.Add(aStudent);

                }

                aReader.Close();

            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return studends;
        }


        public List<Student> GetStudentsByTeacher(int teacherID)
        {
            List<Student> studends = new List<Student>();

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();
                Student aStudent = new Student();

                myCmnd.CommandText = @"select *  from Student  inner join Student_Class On Student.ID = Student_Class.StudentID  where ClassID=" + teacherID.ToString();

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {

                    aStudent.ID = Int32.Parse(aReader["ID"].ToString());
                    aStudent.FName = aReader["First Name"].ToString();
                    aStudent.LName = aReader["Last Name"].ToString();
                    aStudent.FatherName = aReader["Father Name"].ToString();

                    studends.Add(aStudent);

                }

                aReader.Close();

            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();

            return studends;
        }

        public List<Parent> GetParents()
        {
            List<Parent> parents = new List<Parent>();
            SqlConnection aConnection = DBMgr.GetSQLConnection();

            try
            {

                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"SELECT Parents.ID as ID,
	                                    Name,
	                                    [Last Name],
                                        Name + ' ' + [Last Name] as FullName,
	                                    Sys_User.ID as UserID,
	                                    Sys_User.[User Name]
      
                                      FROM [Parents]
                                      inner join Sys_User on Sys_User.ID = Parents.[User ID]";

                SqlDataReader aReader = myCmnd.ExecuteReader();
                while (aReader.Read())
                {
                    Parent aParent = new Parent();
                    aParent.ID = Int32.Parse(aReader["ID"].ToString());
                    aParent.FName = aReader["Name"].ToString();
                    aParent.LName = aReader["Last Name"].ToString();
                    aParent.FullName=aReader["FullName"].ToString(); 
                    User parentUser = new User();
                    parentUser.ID = Int32.Parse(aReader["UserID"].ToString());
                    parentUser.UserName = aReader["User Name"].ToString();
                    aParent.SysUser = parentUser;

                    parents.Add(aParent);
                }

                aReader.Close();
            }
            catch (SqlException se)
            {
                aConnection.Close();
                throw (se);
            }

            aConnection.Close();
            return parents;
        }

//CREATE

        public string CreateUser(User aUser)
        {
            string res= "-1";
            HttpCookie userCookies = new HttpCookie("BHCUser");
            string userName = userCookies.Name;

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"insert into [Sys_User] ([User Name],[Password] ,[Type], [Dummy Password], [Created Date],[Created by]) values ('"
                        + aUser.UserName+ "'" + ",' "
                        + CryptManager.Encrypt(aUser.Password) + "', "
                        + (int)aUser.Type + ", 1, GETDATE()," + " '" + userName + "');" +
                        @"select ID from Sys_User where [User Name] = '" + aUser.UserName + "'";

                SqlDataReader aReader = myCmnd.ExecuteReader();
                if (aReader.Read())
                {
                    res = aReader["ID"].ToString();
                }

                aReader.Close();
             

            }
            catch (SqlException se)
            {
                res = "-1";
                aConnection.Close();
                throw (se);
               
            }

            aConnection.Close();
            return res;
        }


        internal string CreateStudent(string StuName, string StuFamilyName, string StuFatherName, string StuGFName, string stuPhoneNumber, string stuUName, string ClassID, string CreatedBy)
        {
            string CreatedUserID = "-1";
            Student st = new Student();
            st.FName = StuName;
            st.LName = StuFamilyName;
            st.FatherName = StuFatherName;
            st.GFatherName = StuGFName;
            st.PhoneNum = stuPhoneNumber;
            string createruser = CreatedBy;

            User u = new User();
            u.UserName = stuUName;
            u.Password = "123";
            u.Type = USERTYPE.PARENT;
            u.ID = Int32.Parse( CreateUser(u));

            if (u.ID != -1)
            {
                Parent p = new Parent();
                p.FName = StuFatherName;
                p.LName = StuFamilyName;
                p.PhoneNum = stuPhoneNumber;
                p.SysUser = u;
                p.kids_num++;

                p.ID = Int32.Parse(SaveParent(p, createruser));
                if (p.ID != -1)
                {
                    st.StudentParent = p;
                    CreatedUserID = SaveStudent(st,createruser);
                    if (SaveStudentClass(CreatedUserID, ClassID))
                    {
                        // Throw warning TODO
                    }
                }
            }



            return CreatedUserID;
        }






        internal string CreateStudentByParentID(string StuName, string StuFamilyName, string StuFatherName, string StuGFName, string stuPhoneNumber, string ParentID, string ClassID, string CreatedBy)
        {
            string CreatedUserID = "-1";
            Student st = new Student();
            st.FName = StuName;
            st.LName = StuFamilyName;
            st.FatherName = StuFatherName;
            st.GFatherName = StuGFName;
            st.PhoneNum = stuPhoneNumber;
            string createruser = CreatedBy;


            Parent aParent = new Parent(Int32.Parse(ParentID));
            st.StudentParent = aParent;
            CreatedUserID = SaveStudent(st, createruser);
            if (SaveStudentClass(CreatedUserID, ClassID))
            {
                // Throw warning TODO
            }

            return CreatedUserID;
        }

        internal User Login(string userName, string password)
        {
            User loggedUser = new User();
            loggedUser.ID = -1;

            SqlConnection aConnection = DBMgr.GetSQLConnection();
            try
            {
                if (aConnection.State == System.Data.ConnectionState.Open)
                    aConnection.Close();


                aConnection.Open();
                SqlCommand myCmnd = aConnection.CreateCommand();

                myCmnd.CommandText = @"Select * from Sys_User where [User Name] = '" + userName + "' AND Password like '%" + CryptManager.Encrypt(password) + "%'";

                SqlDataReader aReader = myCmnd.ExecuteReader();
                if (aReader.Read())
                {
                    loggedUser.ID = Int32.Parse( aReader["ID"].ToString());
                    loggedUser.UserName = aReader["User Name"].ToString();
                    loggedUser.Type = (USERTYPE) Int32.Parse(aReader["Type"].ToString());
                }

                aReader.Close();


            }
            catch (SqlException se)
            {
                loggedUser.ID = -1;
                aConnection.Close();
                throw (se);

            }

            aConnection.Close();
            return loggedUser;
        }

       
    }
 }
