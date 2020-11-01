using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class QuizItem
    {

        public int ID { get; set; }
        public string QuizTitle { get; set; }

        public List<string> DescriptionStrings { get; }

        public Dictionary<string, List<string>> linkedTo { get; }

        public QuizItem()
        {
            DescriptionStrings = new List<string>();
            linkedTo = new Dictionary<string, List<string>>();
        }
        public void AddQuizDescriptionStrings(List<string> s)
        {
            DescriptionStrings.AddRange(s);
        }
        public void EditQuizDescriptionStrings(List<string> s)
        {
            DescriptionStrings.Clear();
            DescriptionStrings.AddRange(s);
        }
        public bool LinkQuiz(string courseID,List<string> modules)
        {
            //The Logic: if it has the same key => check if value exists in embedded values. ==>return false if exist...else add new values to values list. or loop and add non-existent ones.
            //if key doesn't exist => create new key,value pair
            
            if(linkedTo.ContainsKey(courseID))
            {
                //check if values contain modules and add new ones.
                //first part can be done with linkedTo[courseId]
                //if (!linkedTo.Where(x => x.Key.Equals(courseID)).ToDictionary( i => i.Key, i=>i.Value).Values.Any(x => modules.Any( y => x.Contains(y))))

                var distinctItemsList = linkedTo[courseID].Except(modules).ToList();
                linkedTo[courseID].AddRange(distinctItemsList);
                return true;
            }
            else
            {
                linkedTo.Add(courseID, modules);
                return true;
            }            
        }
        public List<string> GetQuizCourses()
        {
            return linkedTo.Keys.ToList();
        }
        public List<string> GetQuizModulesByCourse(Course course)
        {
            if (linkedTo.ContainsKey(course.CourseID))
            {
                return linkedTo[course.CourseID];
            }
            else
                return null;
        }
    }
}
