using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.Win32;

namespace QuizManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CourseManager courseManager = new CourseManager();
        QuizesManager quizesManager = new QuizesManager();

        public MainWindow()
        {
            InitializeComponent();
            linkButton.IsEnabled = false;

            courseListBox.ItemsSource = courseManager.GetCourses();
            allQuizItems_ListBox.ItemsSource = quizesManager.GetAllQuizItemsAsStrings();
        }

        private void AddCourse_ButtonClick(object sender, RoutedEventArgs e)
        {
            Course course = new Course(
                courseId_textBox.Text,
                 courseName_textBox.Text,
                 stringToArray(courseMoments_textBox.Text,new[] {';'})
                );
            courseManager.AddCourse(course);
        }

        private void courseListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CategoryQuizItems_ListBox.ItemsSource = null;
            modulesListBox.UnselectAll();
            var item = (ListBox)sender;
            if(item.SelectedItem != null)
            {
                modulesListBox.ItemsSource = (item.SelectedItem as Course).Modules;
                CategoryQuizItems_ListBox.ItemsSource =
                    quizesManager.GetQuizItemsByCourseAsString(item.SelectedItem as Course);
            }
        }

        private void RemoveCourse_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(courseListBox.SelectedItem != null)
            {
                courseManager.RemoveCourse((courseListBox.SelectedItem as Course));
                modulesListBox.ItemsSource =null;
            }
        }
        private void AddQuiz_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(quizItem_TextBox.Text))
            {
                QuizItem quiz = new QuizItem();
                quiz.AddQuizDescriptionStrings(
                    stringToArray(quizItem_TextBox.Text,new[] {'\r','\n'}));

                quizesManager.AddQuizItem(quiz);
                //allQuizItems_ListBox.ItemsSource = 
                //    quizesManager.ConvertQuizListToStringList(quizesManager.GetAllQuizItems());
                allQuizItems_ListBox.ItemsSource = quizesManager.GetAllQuizItemsAsStrings();

                quizItem_TextBox.Clear();
            }
        }
        /// <summary>
        /// Functionary to convert a string to an array. Takes two parameters: string to convert and char[] of delimiters. Returns a List<string>
        /// </summary>
        private Func<string, char[], List<string>> stringToArray = (s, d) => s.Split(d).ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        private void allQuizItems_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            if(item.SelectedItem  != null)
            {
                int id = ((KeyValuePair<int, string>)item.SelectedItem).Key;
                quizItem_TextBox.Text = String.Join("\n", quizesManager.GetQuizItemById(id).DescriptionStrings);
            }
        }

        private void UpdateText_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (allQuizItems_ListBox.SelectedItem != null)
            {
                int id = ((KeyValuePair<int, string>)allQuizItems_ListBox.SelectedItem).Key;
                QuizItem quiz = quizesManager.GetQuizItemById(id);
                quiz.EditQuizDescriptionStrings(
                    stringToArray(quizItem_TextBox.Text, new[] { '\r', '\n' }));
                allQuizItems_ListBox.ItemsSource = quizesManager.GetAllQuizItemsAsStrings();

            }
        }
        
        private void RemoveQuiz_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (allQuizItems_ListBox.SelectedItem != null)
            {
                var item = allQuizItems_ListBox.SelectedItem;
                int id = ((KeyValuePair<int, string>)item).Key;
                QuizItem quiz = quizesManager.GetQuizItemById(id);
                quizesManager.RemoveQuizItem(quiz);

                allQuizItems_ListBox.ItemsSource = quizesManager.GetAllQuizItemsAsStrings();

                quizItem_TextBox.Clear();
            }
        }
        private void ClearText_ButtonClick(object sender, RoutedEventArgs e)
        {
            quizItem_TextBox.Clear();
            allQuizItems_ListBox.UnselectAll(); //prevent from deleting the quizitems list if user pressed "clear button" then "update text" on the empty quizItem_TextBox
        }
        private void CopyText_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(allQuizItems_ListBox.SelectedItem != null)
            {
                var item = allQuizItems_ListBox.SelectedItem;
                int id = ((KeyValuePair<int, string>)item).Key;
                quizTexts_ListBox.ItemsSource =
                    quizesManager.GetQuizItemById(id).DescriptionStrings;
            }
        }
        private void CopyToClipboard_ButtonClick(object sender, RoutedEventArgs e)
        { 
            if (quizTexts_ListBox.Items.Count > 0)
            {
                var item = allQuizItems_ListBox.SelectedItem;
                int id = ((KeyValuePair<int, string>)item).Key;
                Clipboard.SetText(string.Join(Environment.NewLine, quizesManager.GetQuizItemById(id).DescriptionStrings));
            }
        }

        private void CheckBox_Toggle(object sender, RoutedEventArgs e)
        {
            linkButton.IsEnabled = !linkButton.IsEnabled;
        }

        private void Link_ButtonClick(object sender, RoutedEventArgs e)
        {

            if(courseListBox.SelectedItem != null && allQuizItems_ListBox.SelectedItem !=null  && modulesListBox.SelectedItems.Count >0)
            {
                QuizItem quiz =
                    quizesManager.GetQuizItemById(((KeyValuePair<int, string>)allQuizItems_ListBox.SelectedItem).Key);
                bool result = quiz.LinkQuiz(
                    (courseListBox.SelectedItem as Course).CourseID,
                    modulesListBox.SelectedItems.Cast<string>().ToList());
                string s = result == true ? "Done" : "Something went wrong. Could'nt link quizes with modules";
                MessageBox.Show(s);
            }
        }

        private void modulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryQuizItems_ListBox.ItemsSource = null;
            if ( modulesListBox.SelectedItems?.Count > 0 && courseListBox.SelectedItem != null)
            {
               CategoryQuizItems_ListBox.ItemsSource =
                    quizesManager.GetQuizByModules(
                    courseListBox.SelectedItem as Course,
                    modulesListBox.SelectedItems.Cast<string>().ToList()
                    );
            }
        }

        private void Save_MenuClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                bool result = SerializationHelper.Serialize(courseManager, quizesManager, saveFileDialog.FileName);
                string message = result ? "Saved!" : "Something went wrong while saving the file";
                MessageBox.Show(message);
            }            
        } 
        private void Open_MenuClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string err = "";
                ManagersContainer container = SerializationHelper.Deserialize(openFileDialog.FileName, out err);
                if (string.IsNullOrEmpty(err))
                {
                    courseManager = container.CourseManager;
                    quizesManager = container.QuizesManager;

                    courseListBox.ItemsSource = courseManager.GetCourses();
                    allQuizItems_ListBox.ItemsSource = quizesManager.GetAllQuizItemsAsStrings();
                }
                else
                    MessageBox.Show(err);
            }                        
        }
    }
}
