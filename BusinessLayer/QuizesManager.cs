using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BusinessLayer
{
    /// <summary>
    /// Method to hold all data for quizes
    /// </summary>
    [Serializable]
    public class QuizesManager
    {
        private int id;
        List<QuizItem> Quizes;
        /// <summary>
        /// Constructor that initializes the QuizItem list and the ID.
        /// </summary>
        public QuizesManager()
        {
            id = 0;
            Quizes = new List<QuizItem>();
        }
        /// <summary>
        /// Method to add a new quiz and give it an id, then increments the id for the next quiz.
        /// </summary>
        /// <param name="quiz"></param>
        public void AddQuizItem(QuizItem quiz)
        {
            quiz.ID = id;
            Quizes.Add(quiz);
            id++;
        }
        /// <summary>
        /// Removes a quiz from list.
        /// </summary>
        /// <param name="quiz">the quiz to remove</param>
        public void RemoveQuizItem(QuizItem quiz)
        {
            Quizes.Remove(quiz);
        }
        /// <summary>
        /// Method to get a quiz by its id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>result quiz</returns>
        public QuizItem GetQuizItemById(int id)
        {
            return (QuizItem)Quizes.Where(x => x.ID == id).FirstOrDefault();
        }
        /// <summary>
        /// Get all quizes that are linkted to a course.
        /// </summary>
        /// <param name="course">Search criteria</param>
        /// <returns>quiz</returns>
        public List<QuizItem> GetQuizItemsByCourse(Course course)
        {
            return Quizes.Where(x => x.GetQuizCourses().Contains(course.CourseID)).ToList();
        }
        /// <summary>
        /// Method to get all quizes for a course as a dictionary of <quiz id, string of quiz description> pairs.
        /// </summary>
        /// <param name="course">search criteria</param>
        /// <returns>dictionary</returns>
        public Dictionary<int, string> GetQuizItemsByCourseAsString(Course course)
        {
            return ConvertQuizListToStringList(GetQuizItemsByCourse(course));
        }
        /// <summary>
        /// retrieves all quizes.
        /// </summary>
        /// <returns></returns>
        public List<QuizItem> GetAllQuizItems()
        {
            return Quizes;
        }
        /// <summary>
        /// retrieves all quizes as a dictionary of <quiz id, string of quiz description> pairs
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetAllQuizItemsAsStrings()
        {
            return ConvertQuizListToStringList(Quizes);
        }
        /// <summary>
        /// Method to convert any quiz list into a dictionary of <quiz id, string of quiz description> pairs
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public Dictionary<int, string> ConvertQuizListToStringList(List<QuizItem> l)
        {
            Dictionary<int, string> stringlist = new Dictionary<int, string>();
            l.ForEach(x => stringlist.Add(x.ID, String.Join(" ", x.DescriptionStrings)));
            return stringlist;
        }
        /// <summary>
        /// Method to retrieve quizes for a selected course, filtered by a list of selected modules.
        /// </summary>
        /// <param name="course">The course that is selected to search for its quizes.</param>
        /// <param name="modules"> list of selected modules</param>
        /// <returns>Formatted list</returns>
        public Dictionary<int, string> GetQuizByModules(Course course, List<string> modules)
        {
            var list = GetQuizItemsByCourse(course).Where(
                x => modules.All(y => x.linkedTo[course.CourseID].Contains(y))).ToList();
            //Logic: search for quizes that have gives course in the linkedTo. Then intersect with modules.
            return ConvertQuizListToStringList(list);
        }
    }
}
