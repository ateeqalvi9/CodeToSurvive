using CodeToSurvive.DLL;
using CodeToSurvive.Language;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace CodeToSurvive
{
    public partial class AssignmentWindow : Window
    {
        private readonly Assignment _assignment;
        private DispatcherTimer _timer;
        private int _timeLeft;
        private Course selectedCourse;

        public bool IsCompleted { get; internal set; }

        public AssignmentWindow(Assignment assignment)
        {
            InitializeComponent();
            _assignment = assignment;
            QuestionText.Text = assignment.Question;
            StartTimer();
        }

        public AssignmentWindow(Course selectedCourse)
        {
            this.selectedCourse = selectedCourse;
        }

        private void StartTimer()
        {
            _timeLeft = _assignment.TimeLimitSeconds;
            _timer = new DispatcherTimer { Interval = System.TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) =>
            {
                _timeLeft--;
                TimerText.Text = $"Time Left: {_timeLeft}s";
                if (_timeLeft <= 0)
                {
                    _timer.Stop();
                    MessageBox.Show("Time Up!");
                    Close();
                }
            };
            _timer.Start();
        }

        private void OpenEditor_Click(object sender, RoutedEventArgs e)
        {
            CodeEditorWindow editor = new CodeEditorWindow();
            editor.ShowDialog();

            Tokenizer t = new Tokenizer(editor.SourceCode);
            Parser p = new Parser(t.Tokenize());

            if (p.Parse().Any())
            {
                MessageBox.Show("Code has errors.");
                return;
            }

            _timer.Stop();
            DialogResult = true;
        }
    }
}
