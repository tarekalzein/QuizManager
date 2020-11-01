using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLayer
{
    public class CourseManager
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
