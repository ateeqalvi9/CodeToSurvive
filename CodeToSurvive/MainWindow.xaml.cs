using CodeToSurvive;
using CodeToSurvive.DLL;
using CodeToSurvive.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeToSurvive_P1
{
    public partial class MainWindow : Window
    {
        private const double MoveStep = 15;
        private Button _currentBuilding;

        private Player _player = new Player();
        private int _day = 1;
        private int playerId;
        public MainWindow() : this(new Player())
        {
        }
        public MainWindow(Player player)
        {
            InitializeComponent();
            _player = player;
            GameState.Initialize();
            Loaded += (s, e) =>
            {
                GameCanvas.Focus();
                Keyboard.Focus(GameCanvas);
                UpdateHUD();
            };
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            GameSaveService.SaveGame(_player);
            base.OnClosing(e);
        }
        // ==============================
        // MOVEMENT
        // ==============================
        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            double x = Canvas.GetLeft(PlayerAvatar);
            double y = Canvas.GetTop(PlayerAvatar);

            if (e.Key == Key.W) y -= MoveStep;
            if (e.Key == Key.S) y += MoveStep;
            if (e.Key == Key.A) x -= MoveStep;
            if (e.Key == Key.D) x += MoveStep;

            Canvas.SetLeft(PlayerAvatar, x);
            Canvas.SetTop(PlayerAvatar, y);

            CheckCollision();

            if (e.Key == Key.Enter && _currentBuilding != null)
                EnterBuilding();
        }

        // ==============================
        // COLLISION
        // ==============================
        private void CheckCollision()
        {
            _currentBuilding = null;
            InteractionPrompt.Visibility = Visibility.Collapsed;

            Rect playerRect = new Rect(
                Canvas.GetLeft(PlayerAvatar),
                Canvas.GetTop(PlayerAvatar),
                PlayerAvatar.Width,
                PlayerAvatar.Height);

            Button[] buildings =
            {
                BtnHouse,
                BtnUniversity,
                BtnSoftwareHouse,
                BtnBank
            };

            foreach (Button b in buildings)
            {
                Rect bRect = new Rect(
                    Canvas.GetLeft(b),
                    Canvas.GetTop(b),
                    b.Width,
                    b.Height);

                if (playerRect.IntersectsWith(bRect))
                {
                    _currentBuilding = b;
                    InteractionPrompt.Visibility = Visibility.Visible;
                    return;
                }
            }
        }

        // ==============================
        // ENTER
        // ==============================
        private void EnterBuilding()
        {
            if (_currentBuilding == BtnHouse)
                BtnHouse_Click(null, null);
            else if (_currentBuilding == BtnUniversity)
                BtnUniversity_Click(null, null);
            else if (_currentBuilding == BtnSoftwareHouse)
                BtnSoftwareHouse_Click(null, null);
            else if (_currentBuilding == BtnBank)
                BtnBank_Click(null, null);
        }

        // ==============================
        // BUILDINGS
        // ==============================
        private void BtnHouse_Click(object sender, RoutedEventArgs e)
        {
            new House(_player).ShowDialog();
            EndDay();
        }

        private void BtnUniversity_Click(object sender, RoutedEventArgs e)
        {
            new University(_player, playerId).ShowDialog();
            EndDay();
        }

        private void BtnSoftwareHouse_Click(object sender, RoutedEventArgs e)
        {
            new SoftwareHouse(_player).ShowDialog();
            EndDay();
        }

        private void BtnBank_Click(object sender, RoutedEventArgs e)
        {
            new Bank(_player).ShowDialog();
            UpdateHUD();
        }

        private void BtnStockExchange_Click(object sender, RoutedEventArgs e)
        {
            StockExchange stock = new StockExchange(_player);
            stock.ShowDialog();

            EndDay(); // Stock exchange advances day
        }

        private void BtnSyntaxSnacks_Click(object sender, RoutedEventArgs e)
        {
            new SyntaxSnacks(_player).ShowDialog();
            EndDay();
        }

        private void BtnBinaryBoutique_Click(object sender, RoutedEventArgs e)
        {
            new BinaryBotique(_player).ShowDialog();
            UpdateHUD();
        }

        // ==============================
        // DAY / HUD
        // ==============================
        private void EndDay()
        {
            _day++;
            _player.Energy = Math.Max(0, _player.Energy - 10);
            UpdateHUD();
        }

        private void UpdateHUD()
        {
            HudMoneyText.Text = $"💰 Money: ${_player.Money}";
            HudEnergyText.Text = $"⚡ Energy: {_player.Energy}%";
            HudSkillText.Text = $"🧠 Skill: {_player.CodingSkill}";
            HudReputationText.Text = $"⭐ Reputation: {_player.Reputation}";
            HudDayText.Text = $"📅 Day {_day}";
        }
    }
}
