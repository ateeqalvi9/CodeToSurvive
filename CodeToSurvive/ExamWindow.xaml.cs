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

namespace CodeToSurvive
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        public bool IsPassed { get; private set; }
        private int _timeLeft = 60;
        private readonly Exam _exam;

        public ExamWindow(Exam exam)
        {
            InitializeComponent();
            _exam = exam;

            LoadExam();
        }

        private void LoadExam()
        {
            ExamTitleText.Text = _exam.Title;
            ExamQuestionText.Text = _exam.Question;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (AnswerBox.Text.Trim() == _exam.CorrectAnswer)
            {
                IsPassed = true;
                MessageBox.Show("🎉 Exam Passed!");
                Close();
            }
            else
            {
                MessageBox.Show("❌ Exam Failed");
                IsPassed = false;
            }
        }
    }
}
