using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Student;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.IO;

namespace WebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        StudentController studentController = new StudentController();
        QueryManager _manager = new QueryManager();

        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStudents()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetStudents());
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStudentsByUser(string UserID)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetStudentsBySysUser(UserID));
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetNotesByUser(string UserID)
        {
            var jsonSerialiser = new JavaScriptSerializer();

            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri). OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            parentDirectory = parentDirectory.Substring(0, parentDirectory.LastIndexOf("/"));
            var Notes = _manager.GetNotesBYUser(UserID);
            foreach(var aNote in Notes)
            {
                aNote.DocPath = "";
                string physicalPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                physicalPath = Path.Combine(physicalPath, aNote.ID.ToString());

                if (Directory.Exists(physicalPath))
                {
                    string[] files = Directory.GetFiles(physicalPath);
                    if (files.Length > 0)
                    {
                        string fileName = Path.GetFileName(files[0]);
                        string DirectoryPath = Path.Combine(parentDirectory, "UploadedFiles");
                        DirectoryPath = Path.Combine(DirectoryPath, aNote.ID.ToString());
                        aNote.DocPath = Path.Combine(DirectoryPath, fileName);
                        //aNote.DocPath = "<a href=\"" + Path.Combine(DirectoryPath, fileName) + "\" target=\"_blank\">تحميل</a>";

                    }
                }
            }
            var json = jsonSerialiser.Serialize(Notes);
            return json;
        }
        


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveNote(int NoteType, int TeacherID, int ClassID, int StudentID, string NoteDetails, string CreatedBy, string NoteDate)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.SaveNote(NoteType, TeacherID, ClassID, StudentID, NoteDetails, CreatedBy, NoteDate));
            return json;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetClasses()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetClasses());
            return json;
        }

         [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetNotes()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetNotes());
            return json;
        }

        
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetParents()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetParents());
            return json;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetClassByTeacher(int i)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetClassByTeacher(i));
            return json;
        }

        //  [WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string GetClassNameForStudent(int ID)
        //{
        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(_manager.GetClassName4Student(ID));
        //    return json;
        //}
        

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTeacher()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetTeacher());
            return json;
        }

           
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStudentsByClass(int classID)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetStudentsByClass(classID));
            return json;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStudentsByTeacher(int i)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.GetStudentsByTeacher(i));
            return json;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateStudent(string StuName, string StuFamilyName, string StuFatherName, string StuGFName, string stuPhoneNumber, string stuUName, string ClassID, string CreatedBy)
        {

            //string res = _manager.CreateStudent(StuName, StuFamilyName, StuFatherName, StuGFName, stuPhoneNumber, stuUName, ClassID, CreatedBy);
            string res = studentController.CreateStudent(StuName, StuFamilyName, StuFatherName, StuGFName, stuPhoneNumber, stuUName, ClassID, CreatedBy);


            return res;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateStudentByParentID(string StuName, string StuFamilyName, string StuFatherName, string StuGFName, string stuPhoneNumber, string ParentID, string ClassID, string CreatedBy)
        {
            /* Student.Student st = new Student.Student();
             st.FName = StuName;
             st.LName = StuFamilyName;
             st.FatherName = StuFatherName;
             st.GFatherName = StuGFName;
             bool res = _manager.SaveStudent(st);*/

            string res = _manager.CreateStudentByParentID(StuName, StuFamilyName, StuFatherName, StuGFName, stuPhoneNumber, ParentID, ClassID, CreatedBy);


            return res;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Login(string userName, string password)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(_manager.Login(userName, password));
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSTDINFO(string stdID, string UserID)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var aStudentCont = _manager.GetSTDINFO(stdID, UserID);
            var json = jsonSerialiser.Serialize(aStudentCont);
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UploadFile()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            string result = "-1";

            int NoteType = Int32.Parse( HttpContext.Current.Request.Form.GetValues("NoteType")[0]);
            int TeacherID = Int32.Parse( HttpContext.Current.Request.Form.GetValues("TeacherID")[0]);
            int ClassID = Int32.Parse( HttpContext.Current.Request.Form.GetValues("ClassID")[0]);
            int StudentID = Int32.Parse( HttpContext.Current.Request.Form.GetValues("StudentID")[0]);
            string NoteDetails =  HttpContext.Current.Request.Form.GetValues("NoteDetails")[0];
            string CreatedBy =  HttpContext.Current.Request.Form.GetValues("CreatedBy")[0];
            string NoteDate =  HttpContext.Current.Request.Form.GetValues("NoteDate")[0];


            string NoteID = _manager.SaveNote(NoteType, TeacherID, ClassID, StudentID, NoteDetails, CreatedBy, NoteDate);

            result = NoteID;
            if ((HttpContext.Current.Request.Files.AllKeys.Any()) && (NoteID != "-1"))
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    result = "-1";
                    // Get the complete file path
                    string DirecPath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), NoteID);
                    Directory.CreateDirectory(DirecPath);
                    var fileSavePath = Path.Combine(DirecPath, httpPostedFile.FileName);

                    // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(fileSavePath);
                    result = NoteID;
                }
            }

            var json = jsonSerialiser.Serialize(result);
            return json;
        }

    }
}
