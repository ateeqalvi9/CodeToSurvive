using CodeToSurvive.DLL;
using System.Windows;

namespace CodeToSurvive
{
    public partial class LessonWindow : Window
    {
        private readonly List<Lesson>? _lessons;
        private int _currentIndex;

        public bool LessonsCompleted { get; private set; }
        public LessonWindow(List<Lesson> lessons, int startIndex = 0)
        {
            InitializeComponent();
            _lessons = lessons;
            _currentIndex = startIndex;

            BtnNextLesson.Click += BtnNextLesson_Click;
            BtnStartAssignment.Click += BtnStartAssignment_Click;

            LoadLesson();
        }

        private void LoadLesson()
        {
            Lesson current = _lessons[_currentIndex];

            LessonTitleText.Text = current.Title;
            LessonContentText.Text = current.Content;

            LessonProgress.Value =
                ((_currentIndex + 1) / (double)_lessons.Count) * 100;

            // If last lesson → show assignment button
            if (_currentIndex == _lessons.Count - 1)
            {
                BtnNextLesson.Visibility = Visibility.Collapsed;
                BtnStartAssignment.Visibility = Visibility.Visible;
            }
            else
            {
                BtnNextLesson.Visibility = Visibility.Visible;
                BtnStartAssignment.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnNextLesson_Click(object sender, RoutedEventArgs e)
        {
            _currentIndex++;
            LoadLesson();
        }

        private void BtnStartAssignment_Click(object sender, RoutedEventArgs e)
        {
            LessonsCompleted = true;
            Close();
        }
    }
}