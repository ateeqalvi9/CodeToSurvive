using CodeToSurvive.DLL;
using CodeToSurvive.Language;
using System.Linq;
using System.Text;
using System.Windows;

namespace CodeToSurvive
{
    public partial class SoftwareHouse : Window
    {
        private readonly Player _player;

        private int _salary;
        private int _energyCost;
        private int _reputationGain;
        private int _requiredSkill;
        private JobLevel _requiredLevel;

        public SoftwareHouse(Player player)
        {
            InitializeComponent();
            _player = player;
            LoadHUD();
        }

        // ================= JOB SELECTION =================

        private void Intern_Click(object sender, RoutedEventArgs e)
        {
            SetupJob("Intern Task",
                "Fix simple bugs and write basic logic.",
                salary: 100,
                energy: 10,
                rep: 1,
                skill: 0,
                level: JobLevel.Intern);
        }

        private void Junior_Click(object sender, RoutedEventArgs e)
        {
            SetupJob("Junior Developer",
                "Work on small features.",
                salary: 250,
                energy: 15,
                rep: 3,
                skill: 5,
                level: JobLevel.JuniorDeveloper);
        }

        private void Mid_Click(object sender, RoutedEventArgs e)
        {
            SetupJob("Mid Developer",
                "Implement business logic.",
                salary: 450,
                energy: 20,
                rep: 6,
                skill: 10,
                level: JobLevel.MidDeveloper);
        }

        private void Senior_Click(object sender, RoutedEventArgs e)
        {
            SetupJob("Senior Developer",
                "Architect complex systems.",
                salary: 800,
                energy: 30,
                rep: 10,
                skill: 20,
                level: JobLevel.SeniorDeveloper);
        }

        private void SetupJob(string title, string desc, int salary, int energy, int rep, int skill, JobLevel level)
        {
            JobTitleText.Text = title;
            JobDescriptionText.Text = desc;
            SalaryText.Text = $"💰 Salary: ${salary}";
            EnergyCostText.Text = $"⚡ Energy: -{energy}";
            ReputationText.Text = $"⭐ Reputation: +{rep}";

            _salary = salary;
            _energyCost = energy;
            _reputationGain = rep;
            _requiredSkill = skill;
            _requiredLevel = level;
        }

        // ================= WORK =================

        private void BtnStartWork_Click(object sender, RoutedEventArgs e)
        {
            if (_player.JobLevel < _requiredLevel)
            {
                MessageBox.Show("You are not promoted enough for this job.");
                return;
            }

            if (_player.CodingSkill < _requiredSkill)
            {
                MessageBox.Show("Your coding skill is too low.");
                return;
            }

            if (_player.Energy < _energyCost)
            {
                MessageBox.Show("Not enough energy.");
                return;
            }

            CodeEditorWindow editor = new CodeEditorWindow();
            editor.ShowDialog();

            if (string.IsNullOrWhiteSpace(editor.SourceCode))
            {
                MessageBox.Show("Work failed.");
                return;
            }

            Tokenizer tokenizer = new Tokenizer(editor.SourceCode);
            Parser parser = new Parser(tokenizer.Tokenize());

            if (parser.Parse().Any())
            {
                MessageBox.Show("Code has errors. Work failed.");
                _player.Energy -= 5;
                LoadHUD();
                return;
            }

            // SUCCESS
            _player.Money += _salary;
            _player.Energy -= _energyCost;
            _player.Reputation += _reputationGain;
            _player.TotalWorkDays++;
            PromoteIfEligible();
            _player.AdvanceDay();

            MessageBox.Show("Work completed successfully!");
            LoadHUD();
        }

        private void PromoteIfEligible()
        {
            if (_player.Reputation >= 30 && _player.JobLevel == JobLevel.JuniorDeveloper)
                _player.JobLevel = JobLevel.MidDeveloper;

            if (_player.Reputation >= 60 && _player.JobLevel == JobLevel.MidDeveloper)
                _player.JobLevel = JobLevel.SeniorDeveloper;
        }

        // ================= HUD =================

        private void LoadHUD()
        {
            SHMoneyText.Text = $"💰 Money: ${_player.Money}";
            SHEnergyText.Text = $"⚡ Energy: {_player.Energy}%";
            SHSkillText.Text = $"🧠 Skill: {_player.CodingSkill}";
            SHReputationText.Text = $"⭐ Reputation: {_player.Reputation}";
            SHJobText.Text = $"🏢 Role: {_player.JobLevel}";
        }

        private void BtnExitSoftwareHouse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
