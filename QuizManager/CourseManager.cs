using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager
{
    class CourseManager
    {
        ObservableCollection<Course> Courses;

        public CourseManager()
        {
            Courses = new ObservableCollection<Course>();
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
        public void RemoveCourse(Course course)
        {
            Courses.Remove(course);
        }
        public Course GetCourseById(string id)
        {
            return (Course)Courses.Where(x => x.CourseID.Equals(id));
        }
        public ObservableCollection<Course> GetCourses()
        {
            return Courses;
        }

    }
}
