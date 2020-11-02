using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLayer
{
    /// <summary>
    /// Class that holds the data of all courses. It includes the needed methods to manipulate courses and retrieve them.
    /// </summary>
    [Serializable]
    public class CourseManager
    {
        ObservableCollection<Course> Courses;
        /// <summary>
        /// Constructor that initializes the list of courses.
        /// </summary>
        public CourseManager()
        {
            Courses = new ObservableCollection<Course>();
        }
        /// <summary>
        /// Method to add a new course.
        /// </summary>
        /// <param name="course">course to add</param>
        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
        /// <summary>
        /// Method to remove a course
        /// </summary>
        /// <param name="course">course to remove</param>
        public void RemoveCourse(Course course)
        {
            Courses.Remove(course);
        }
        /// <summary>
        /// Method to get a course by its id.
        /// </summary>
        /// <param name="id">course id of the required course</param>
        /// <returns>course</returns>
        public Course GetCourseById(string id)
        {
            return (Course)Courses.Where(x => x.CourseID.Equals(id));
        }
        /// <summary>
        /// Method to retrieve all courses.
        /// </summary>
        /// <returns> Courses list</returns>
        public ObservableCollection<Course> GetCourses()
        {
            return Courses;
        }

    }
}
