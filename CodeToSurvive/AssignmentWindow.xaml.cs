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
        private readonly DispatcherTimer _timer;
        private int _timeLeft;
        public bool IsCompleted { get; private set; } = false;

        public AssignmentWindow(Assignment assignment)
        {
            InitializeComponent();
            if (assignment == null)
            {
                MessageBox.Show("Assignment not found.");
                Close();
                return;
            }

            _assignment = assignment;

            QuestionText.Text = assignment.Question;

            // timer setup
            _timeLeft = _assignment.TimeLimitSeconds > 0
                ? _assignment.TimeLimitSeconds
                : 120; // default fallback

            TimerText.Text = $"Time Left: {_timeLeft}s";

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _timeLeft--;

            TimerText.Text = $"Time Left: {_timeLeft}s";

            if (_timeLeft <= 0)
            {
                _timer.Stop();
                MessageBox.Show("⏳ Time is up! Assignment failed.");
                Close();
            }
        }

        private void OpenEditor_Click(object sender, RoutedEventArgs e)
        {
            CodeEditorWindow editor = new CodeEditorWindow();
            editor.ShowDialog();

            if (string.IsNullOrWhiteSpace(editor.SourceCode))
            {
                MessageBox.Show("You must submit some code.");
                return;
            }

            try
            {
                Tokenizer t = new Tokenizer(editor.SourceCode);
                Parser p = new Parser(t.Tokenize());

                var errors = p.Parse();

                if (errors.Any())
                {
                    MessageBox.Show("❌ Your code contains errors.\nFix and try again.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Compiler error — invalid submission.");
                return;
            }

            // success
            _timer.Stop();
            IsCompleted = true;
            DialogResult = true;

            MessageBox.Show("✅ Assignment Submitted Successfully!");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_timer != null)
                _timer.Stop();

            base.OnClosing(e);
        }
    }
}
