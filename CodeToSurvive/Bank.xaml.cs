using CodeToSurvive.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for Bank.xaml
    /// </summary>
    public partial class Bank : Window
    {
        private Player _player;
        public Bank(Player player)
        {
            InitializeComponent();
            _player = player;
            UpdateUI();
        }
        // ========================
        // DEPOSIT
        // ========================
        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            int amount = AskAmount("Deposit Amount");

            if (amount <= 0) return;

            _player.Money += amount;
            UpdateUI();
        }

        // ========================
        // WITHDRAW
        // ========================
        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            int amount = AskAmount("Withdraw Amount");

            if (amount <= 0) return;

            if (_player.Money < amount)
            {
                ShowError("Insufficient balance.");
                return;
            }

            _player.Money -= amount;
            UpdateUI();
        }

        // ========================
        // APPLY LOAN
        // ========================
        private void ApplyLoan_Click(object sender, RoutedEventArgs e)
        {
            if (_player.ActiveLoan > 0)
            {
                ShowError("You already have an active loan.");
                return;
            }

            if (_player.CreditScore < 600)
            {
                ShowError("Credit score too low for loan approval.");
                return;
            }

            int amount = AskAmount("Loan Amount (Max 5000)");

            if (amount <= 0 || amount > 5000)
            {
                ShowError("Invalid loan amount.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Apply for loan of ${amount}?\nInterest: 10%",
                "Loan Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            _player.ActiveLoan = amount + (amount / 10); // 10% interest
            _player.Money += amount;
            _player.CreditScore -= 50;

            UpdateUI();
        }

        // ========================
        // REPAY LOAN
        // ========================
        private void RepayLoan_Click(object sender, RoutedEventArgs e)
        {
            if (_player.ActiveLoan <= 0)
            {
                ShowError("No active loan to repay.");
                return;
            }

            if (_player.Money < _player.ActiveLoan)
            {
                ShowError("Not enough money to repay the loan.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Repay loan of ${_player.ActiveLoan}?",
                "Confirm Repayment",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            _player.Money -= _player.ActiveLoan;
            _player.ActiveLoan = 0;
            _player.CreditScore += 30;

            UpdateUI();
        }

        // ========================
        // UI HELPERS
        // ========================
        private void UpdateUI()
        {
            BalanceText.Text = $"${_player.Money}";
            LoanStatusText.Text = _player.ActiveLoan > 0
                ? $"Active Loan: ${_player.ActiveLoan}"
                : "Active Loan: None";

            CreditScoreText.Text = $"Credit Score: {_player.CreditScore}";
        }

        private int AskAmount(string title)
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter amount:", title, "0");

            if (int.TryParse(input, out int amount))
                return amount;

            return 0;
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Bank Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
