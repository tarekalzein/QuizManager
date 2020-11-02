using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{    
    [Serializable]
   public class Course
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public List<string> Modules { get; set; }
        public Course()
        {

        }
        public Course(string courseID, string courseName, List<string> modules)
        {
            CourseID = courseID;
            CourseName = courseName;
            Modules = modules;
        }
    }
}
