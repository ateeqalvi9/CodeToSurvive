using CodeToSurvive.DLL;
using System.Windows;

namespace CodeToSurvive
{
    public partial class LessonWindow : Window
    {
        private readonly List<Lesson> _lessons;
        private int _currentIndex;
        public bool LessonsCompleted { get; private set; } = false;
        public LessonWindow(List<Lesson> lessons, int startIndex = 0)
        {
            InitializeComponent();
            if (lessons == null || lessons.Count == 0)
            {
                MessageBox.Show("No lessons found for this course.");
                Close();
                return;
            }

            _lessons = lessons.OrderBy(l => l.LessonId).ToList();

            _currentIndex = Math.Max(0, Math.Min(startIndex, _lessons.Count - 1));

            BtnNextLesson.Click += BtnNextLesson_Click;
            BtnStartAssignment.Click += BtnStartAssignment_Click;

            LoadLesson();
        }

        private void LoadLesson()
        {
            if (_currentIndex < 0 || _currentIndex >= _lessons.Count)
            {
                MessageBox.Show("Lesson index out of range.");
                Close();
                return;
            }

            Lesson current = _lessons[_currentIndex];

            LessonTitleText.Text = current.Title;
            LessonContentText.Text = current.Content;

            LessonProgress.Value =
                ((_currentIndex + 1.0) / _lessons.Count) * 100;

            // show correct buttons
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
            if (_currentIndex < _lessons.Count - 1)
            {
                _currentIndex++;
                LoadLesson();
            }
        }

        private void BtnStartAssignment_Click(object sender, RoutedEventArgs e)
        {
            LessonsCompleted = true;
            Close();
        }
    }
}