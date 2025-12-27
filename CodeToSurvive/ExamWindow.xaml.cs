using CodeToSurvive.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CodeToSurvive
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        public bool IsPassed { get; private set; } = false;
        private readonly Exam _exam;
        private readonly DispatcherTimer _timer;
        private int _timeLeft;
        public ExamWindow(Exam exam)
        {
            InitializeComponent();
            if (exam == null)
            {
                MessageBox.Show("Exam could not be loaded.");
                Close();
                return;
            }

            _exam = exam;

            LoadExam();

            // -------- TIMER SETUP ----------
            _timeLeft = _exam.TimeLimitSeconds > 0
                ? _exam.TimeLimitSeconds
                : 60;   // fallback 60s if not set

            Title = $"Exam — Time Left: {_timeLeft}s";

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void LoadExam()
        {
            ExamTitleText.Text = _exam.Title;
            ExamQuestionText.Text = _exam.Question;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _timeLeft--;

            Title = $"Exam — Time Left: {_timeLeft}s";

            if (_timeLeft <= 0)
            {
                _timer.Stop();
                MessageBox.Show("⏳ Time up! Exam failed.");
                Close();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string answer = AnswerBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(answer))
            {
                MessageBox.Show("Please enter an answer before submitting.");
                return;
            }

            if (string.Equals(answer, _exam.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                _timer.Stop();
                IsPassed = true;
                MessageBox.Show("🎉 Exam Passed!");
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("❌ Incorrect Answer. Exam Failed.");
                IsPassed = false;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_timer != null)
                _timer.Stop();

            base.OnClosing(e);
        }
    }
}
