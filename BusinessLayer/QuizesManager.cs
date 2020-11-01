using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BusinessLayer
{
    public class QuizesManager
    {
        private int id;
        List<QuizItem> Quizes;

        public QuizesManager()
        {
            id = 0;
            Quizes = new List<QuizItem>();
        }

        public void AddQuizItem(QuizItem quiz)
        {
            quiz.ID = id;
            Quizes.Add(quiz);
            id++;
        }

        public void RemoveQuizItem(QuizItem quiz)
        {
            Quizes.Remove(quiz);
        }

        public QuizItem GetQuizItemById(int id)
        {
            return (QuizItem)Quizes.Where(x => x.ID == id).FirstOrDefault();
        }
        public List<QuizItem> GetQuizItemsByCourse(Course course)
        {
            return Quizes.Where(x => x.GetQuizCourses().Contains(course.CourseID)).ToList();
        }
        public Dictionary<int, string> GetQuizItemsByCourseAsString(Course course)
        {
            return ConvertQuizListToStringList(GetQuizItemsByCourse(course));
        }
        public List<QuizItem> GetAllQuizItems()
        {
            return Quizes;
        }
        public Dictionary<int, string> GetAllQuizItemsAsStrings()
        {
            return ConvertQuizListToStringList(Quizes);
        }
        public Dictionary<int, string> ConvertQuizListToStringList(List<QuizItem> l)
        {
            Dictionary<int, string> stringlist = new Dictionary<int, string>();
            l.ForEach(x => stringlist.Add(x.ID, String.Join(" ", x.DescriptionStrings)));
            return stringlist;
        }
        public Dictionary<int, string> GetQuizByModules(Course course, List<string> modules)
        {
            var list = GetQuizItemsByCourse(course).Where(
                x => modules.All(y => x.linkedTo[course.CourseID].Contains(y))).ToList();
            //Logic: search for quizes that have gives course in the linkedTo. Then intersect with modules.
            list.ForEach(x => Console.WriteLine(x.ID));
            return ConvertQuizListToStringList(list);
        }
    }
}
