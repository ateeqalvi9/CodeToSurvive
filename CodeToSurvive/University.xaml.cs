using CodeToSurvive.DLL;
using System.Windows;

namespace CodeToSurvive
{
    public partial class University : Window
    {
        private readonly Player _player;
        private Course _selectedCourse;
        private int _lessonIndex = 0;
        private CourseProgress _progress;

        public University(Player player, int playerId)
        {
            InitializeComponent();
            _player = player;
            LoadHUD();
        }

        // ================= SELECT COURSE =================

        private void BtnCpp_Click(object sender, RoutedEventArgs e)
        {
            LoadCourse(CourseFactory.CreateCppCourse());
        }

        private void BtnCSharp_Click(object sender, RoutedEventArgs e)
        {
            LoadCourse(CourseFactory.CreateCSharpCourse());
        }

        private void BtnWeb_Click(object sender, RoutedEventArgs e)
        {
            LoadCourse(CourseFactory.CreateWebCourse());
        }

        private void LoadCourse(Course c)
        {
            _selectedCourse = c;

            CourseTitleText.Text = c.Name;
            CourseDescriptionText.Text = c.Description;

            SkillGainText.Text = $"🧠 Skill +{c.SkillGain}";
            EnergyCostText.Text = $"⚡ Energy -{c.EnergyCost}";

            LoadOrCreateProgress();
            UpdateProgressBar();
        }

        // ================= ENROLL =================

        private void BtnEnroll_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show("Please select a course first.");
                return;
            }

            LoadOrCreateProgress();

            StartLesson();
        }

        // ================= DB LOGIC =================

        private void LoadOrCreateProgress()
        {
            using var db = new GameDbContext();

            _progress = db.CourseProgress
                .FirstOrDefault(p => p.PlayerId == _player.Id
                                  && p.CourseId == _selectedCourse.CourseId);

            if (_progress == null)
            {
                _progress = new CourseProgress
                {
                    PlayerId = _player.Id,
                    CourseId = _selectedCourse.CourseId,
                    CurrentLessonIndex = 0,
                    IsCompleted = false
                };

                db.CourseProgress.Add(_progress);
                db.SaveChanges();
            }

            _lessonIndex = _progress.CurrentLessonIndex;
        }

        private void SaveProgress()
        {
            using var db = new GameDbContext();

            var p = db.CourseProgress.First(x => x.CourseId == _progress.CourseId);

            p.CurrentLessonIndex = _lessonIndex;
            p.LastUpdated = DateTime.Now;

            db.SaveChanges();
        }

        // ================= LESSON FLOW =================

        private void StartLesson()
        {
            LessonWindow lw = new LessonWindow(
                _selectedCourse.Lessons,
                _lessonIndex
            );

            lw.ShowDialog();

            if (!lw.LessonsCompleted)
                return;

            StartAssignment(_selectedCourse.Lessons[_lessonIndex].Assignment);
        }

        private void StartAssignment(Assignment assignment)
        {
            AssignmentWindow aw = new AssignmentWindow(assignment);
            bool? ok = aw.ShowDialog();

            if (ok != true)
                return;

            _lessonIndex++;
            SaveProgress();
            UpdateProgressBar();

            if (_lessonIndex >= _selectedCourse.Lessons.Count)
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
                MessageBox.Show("Exam Failed ❌");
                return;
            }

            using var db = new GameDbContext();
            var p = db.CourseProgress.First(x => x.CourseId == _progress.CourseId);
            p.IsCompleted = true;
            db.SaveChanges();

            _player.CodingSkill += _selectedCourse.SkillGain;
            _player.Energy -= _selectedCourse.EnergyCost;
            _player.AdvanceDay();

            MessageBox.Show("🎓 Course Completed!");

            LoadHUD();
        }

        // ================= UI =================

        private void UpdateProgressBar()
        {
            double percent =
                ((double)_lessonIndex / _selectedCourse.Lessons.Count) * 100;

            CourseProgressBar.Value = percent;
        }

        private void LoadHUD()
        {
            UniMoneyText.Text = $"💰 Money: ${_player.Money}";
            UniEnergyText.Text = $"⚡ Energy: {_player.Energy}%";
            UniSkillText.Text = $"🧠 Skill: {_player.CodingSkill}";
            UniDayText.Text = $"📅 Day {_player.CurrentDay}";
        }

        private void BtnExitUniversity_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
