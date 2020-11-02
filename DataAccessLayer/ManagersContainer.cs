using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    [Serializable]
    public class ManagersContainer
    {
        public CourseManager CourseManager { get; set; }
        public QuizesManager QuizesManager { get; set; }

        public ManagersContainer()
        {

        }
        public ManagersContainer(CourseManager courseManager, QuizesManager quizesManager)
        {
            CourseManager = courseManager;
            QuizesManager = quizesManager;
        }        
    }
}
