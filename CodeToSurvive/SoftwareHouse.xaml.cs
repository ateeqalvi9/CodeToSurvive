using CodeToSurvive.DLL;
using CodeToSurvive.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CodeToSurvive
{
    public partial class SoftwareHouse : Window
    {
        private readonly Player _player;

        // Job parameters
        private int _salary;
        private int _energyCost;
        private int _reputationGain;
        private int _requiredSkill;
        private string _currentJobTitle;

        public SoftwareHouse(Player player)
        {
            InitializeComponent();
            _player = player;

            LoadHUD();
            ResetJobDetails();
        }

        // ==============================
        // HUD
        // ==============================
        private void LoadHUD()
        {
            SHMoneyText.Text = $"💰 Money: ${_player.Money}";
            SHEnergyText.Text = $"⚡ Energy: {_player.Energy}%";
            SHSkillText.Text = $"🧠 Skill: {_player.CodingSkill}";
            SHReputationText.Text = $"⭐ Reputation: {_player.Reputation}";
            SHDayText.Text = $"📅 Day {_player.Reputation + 1}";
        }

        private void ResetJobDetails()
        {
            JobTitleText.Text = "Select a Job";
            JobDescriptionText.Text = "Choose a job from the left panel.";
            SalaryText.Text = "$0";
            EnergyCostText.Text = "-0";
            ReputationText.Text = "+0";
            _currentJobTitle = null;
        }

        // ==============================
        // JOB SELECTION
        // ==============================
        private void BtnJuniorDev_Click(object sender, RoutedEventArgs e)
        {
            SetupJob(
                "Junior Developer Task",
                "Fix basic bugs and implement simple logic.",
                salary: 200,
                energy: 15,
                reputation: 2,
                requiredSkill: 0
            );
        }

        private void BtnWebDev_Click(object sender, RoutedEventArgs e)
        {
            SetupJob(
                "Web Developer Task",
                "Create a basic frontend or backend API.",
                salary: 350,
                energy: 20,
                reputation: 4,
                requiredSkill: 5
            );
        }

        private void BtnBackendDev_Click(object sender, RoutedEventArgs e)
        {
            SetupJob(
                "Backend Developer Task",
                "Implement business logic and database operations.",
                salary: 500,
                energy: 25,
                reputation: 6,
                requiredSkill: 10
            );
        }

        private void BtnAIEngineer_Click(object sender, RoutedEventArgs e)
        {
            SetupJob(
                "AI Engineer Task",
                "Implement a simple algorithmic solution.",
                salary: 800,
                energy: 30,
                reputation: 10,
                requiredSkill: 20
            );
        }

        private void SetupJob(
            string title,
            string description,
            int salary,
            int energy,
            int reputation,
            int requiredSkill)
        {
            _currentJobTitle = title;
            _salary = salary;
            _energyCost = energy;
            _reputationGain = reputation;
            _requiredSkill = requiredSkill;

            JobTitleText.Text = title;
            JobDescriptionText.Text = description;
            SalaryText.Text = $"${salary}";
            EnergyCostText.Text = $"-{energy}";
            ReputationText.Text = $"+{reputation}";
        }

        // ==============================
        // START JOB
        // ==============================
        private void BtnStartJob_Click(object sender, RoutedEventArgs e)
        {
            if (_currentJobTitle == null)
            {
                MessageBox.Show("Please select a job first.");
                return;
            }

            if (_player.CodingSkill < _requiredSkill)
            {
                MessageBox.Show(
                    "Your coding skill is too low for this job.",
                    "Skill Requirement",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (_player.Energy < _energyCost)
            {
                MessageBox.Show(
                    "Not enough energy to work today.",
                    "Low Energy",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Open code editor
            CodeEditorWindow editor = new CodeEditorWindow();
            editor.ShowDialog();

            string submittedCode = editor.SourceCode;

            if (string.IsNullOrWhiteSpace(submittedCode))
            {
                MessageBox.Show("No code submitted.");
                return;
            }

            // ==============================
            // USE YOUR DLL
            // ==============================
            Tokenizer tokenizer = new Tokenizer(submittedCode);
            var tokens = tokenizer.Tokenize();

            Parser parser = new Parser(tokens);
            List<SyntaxError> errors = parser.Parse();

            if (errors.Any())
            {
                StringBuilder errorText = new StringBuilder();
                foreach (var err in errors)
                    errorText.AppendLine($"Line {err.Line}: {err.Message}");

                MessageBox.Show(
                    errorText.ToString(),
                    "Syntax Errors",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                _player.Energy -= 5;
                LoadHUD();
                return;
            }

            // ==============================
            // SUCCESS
            // ==============================
            _player.Money += _salary;
            _player.Energy -= _energyCost;
            _player.Reputation += _reputationGain;
            _player.CodingSkill += 1;

            MessageBox.Show(
                $"{_currentJobTitle} Completed!\n\n" +
                $"Salary: +${_salary}\n" +
                $"Reputation: +{_reputationGain}",
                "Job Successful",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            LoadHUD();
            ResetJobDetails();
        }

        // ==============================
        // EXIT
        // ==============================
        private void BtnExitSoftwareHouse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
