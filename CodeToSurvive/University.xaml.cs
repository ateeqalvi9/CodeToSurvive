using CodeToSurvive.DLL;
using System.Collections.Generic;
using System.Windows;

namespace CodeToSurvive
{
    public partial class University : Window
    {
        private readonly Player _player;
        private int _currentLessonIndex = 0;
        private Course _currentCourse;
        private Dictionary<Course, CourseProgress> _progress = new Dictionary<Course, CourseProgress>();
        private Course _cpp, _csharp, _web;
        private Course _selectedCourse;
        private int _lessonProgress = 0;

        public University(Player player)
        {
            InitializeComponent();
            _player = player;
        }

        // ================= COURSE BUTTONS =================
        private void BtnCpp_Click(object sender, RoutedEventArgs e)
        {
            _selectedCourse = CourseFactory.CreateCppCourse();
            ShowCourse();
        }

        private void BtnCSharp_Click(object sender, RoutedEventArgs e)
        {
            _selectedCourse = CourseFactory.CreateCSharpCourse();
            ShowCourse();
        }

        private void BtnWeb_Click(object sender, RoutedEventArgs e)
        {
            _selectedCourse = CourseFactory.CreateWebCourse();
            ShowCourse();
        }

        private void ShowCourse()
        {
            CourseTitleText.Text = _selectedCourse.Name;
            CourseDescriptionText.Text = _selectedCourse.Description;
            SkillGainText.Text = $"+{_selectedCourse.SkillGain}";
            EnergyCostText.Text = $"-{_selectedCourse.EnergyCost}";
            DurationText.Text = $"{_selectedCourse.DurationDays} Days";
        }

        // ================= ENROLL =================
        private void BtnEnroll_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show("Please select a course first.");
                return;
            }

            StartLesson();
        }

        // ================= LESSON FLOW =================
        private void StartLesson()
        {
            Lesson lesson = _selectedCourse.Lessons[_currentLessonIndex];
            LessonWindow lw = new LessonWindow(_selectedCourse.Lessons, _lessonProgress);
            lw.ShowDialog();

            StartAssignment(lesson.Assignment);
        }

        private void StartAssignment(Assignment assignment)
        {
            CodeEditorWindow editor = new CodeEditorWindow();
            editor.ShowDialog();

            if (string.IsNullOrWhiteSpace(editor.SourceCode))
            {
                MessageBox.Show("Assignment failed.");
                return;
            }

            // Simple validation
            if (!editor.SourceCode.Contains("for"))
            {
                MessageBox.Show("Incorrect code.");
                return;
            }

            _currentLessonIndex++;

            if (_currentLessonIndex >= _selectedCourse.Lessons.Count)
                StartExam();
            else
                StartLesson();
        }

        // ================= EXAM =================
        private void StartExam()
        {
            ExamWindow ew = new ExamWindow(_selectedCourse.Exam);
            ew.ShowDialog();

            if (!ew.IsPassed)
            {
                MessageBox.Show("You failed the exam.");
                return;
            }

            _player.CodingSkill += _selectedCourse.SkillGain;
            _player.Energy -= _selectedCourse.EnergyCost;

            MessageBox.Show("Course completed successfully!");
            LoadHUD();
        }

        private void LoadHUD()
        {
            UniMoneyText.Text = $"💰 Money: ${_player.Money}";
            UniEnergyText.Text = $"⚡ Energy: {_player.Energy}%";
            UniSkillText.Text = $"🧠 Skill: {_player.CodingSkill}";
            UniDayText.Text = $"📅 Day {_player.Day}";
        }
        private void BtnExitUniversity_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
