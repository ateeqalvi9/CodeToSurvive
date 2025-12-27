using CodeToSurvive.DLL;
using Microsoft.VisualBasic;
using System.Windows;

namespace CodeToSurvive
{
    public partial class Bank : Window
    {
        private readonly Player _player;

        public Bank(Player player)
        {
            InitializeComponent();
            _player = player;
            UpdateUI();
        }

        // =============================
        // APPLY LOAN
        // =============================
        private void ApplyLoan_Click(object sender, RoutedEventArgs e)
        {
            if (_player.HasLoan)
            {
                MessageBox.Show("You already have an active loan.");
                return;
            }

            int maxLoan = CalculateMaxLoan();

            if (maxLoan <= 0)
            {
                MessageBox.Show("You are not eligible for a loan.");
                return;
            }

            var confirm = MessageBox.Show(
                $"You can take a loan up to ${maxLoan}\nInterest: 10%\nProceed?",
                "Loan Offer",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes)
                return;

            int finalLoan = maxLoan + (maxLoan / 10); // 10% interest

            _player.ActiveLoan = finalLoan;
            _player.Money += maxLoan;

            MessageBox.Show(
                $"Loan Approved!\nReceived: ${maxLoan}\nTo Repay: ${finalLoan}",
                "Loan Granted");

            UpdateUI();
        }

        private int CalculateMaxLoan()
        {
            if (_player.Money <= 0)
                return 100;

            return _player.Money / 10;
        }

        // =============================
        // REPAY LOAN
        // =============================
        private void RepayLoan_Click(object sender, RoutedEventArgs e)
        {
            if (!_player.HasLoan)
            {
                MessageBox.Show("No active loan to repay.");
                return;
            }

            if (_player.Money < _player.ActiveLoan)
            {
                MessageBox.Show("Not enough money to repay loan.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Repay loan of ${_player.ActiveLoan}?",
                "Confirm Repayment",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            _player.Money -= _player.ActiveLoan;
            _player.ActiveLoan = 0;

            MessageBox.Show("Loan fully repaid!");

            UpdateUI();
        }

        // =============================
        // UI UPDATE
        // =============================
        private void UpdateUI()
        {
            BalanceText.Text = $"${_player.Money}";
            LoanStatusText.Text = _player.HasLoan
                ? $"Active Loan: ${_player.ActiveLoan}"
                : "Active Loan: None";
        }

        private void BtnExitBank_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // ========================
        // DEPOSIT
        // ========================
        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            int amount = AskAmount("Deposit Amount");

            if (amount <= 0)
                return;

            _player.Money += amount;
            UpdateUI();
        }

        // ========================
        // WITHDRAW
        // ========================
        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            int amount = AskAmount("Withdraw Amount");

            if (amount <= 0)
                return;

            if (_player.Money < amount)
            {
                MessageBox.Show("Insufficient balance.", "Bank",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _player.Money -= amount;
            UpdateUI();
        }
        private int AskAmount(string title)
        {
            string input = Interaction.InputBox(
                "Enter amount:", title, "0");

            if (int.TryParse(input, out int amount))
                return amount;

            return 0;
        }
    }
}
