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
    /// Interaction logic for House.xaml
    /// </summary>
    public partial class House : Window
    {
        private Player _player;
        public House(Player player)
        {
            InitializeComponent();
            _player = new Player();
            UpdateUI();
            _player = player;
        }
        // =========================
        // FOOD ACTIONS
        // =========================
        private void EatSnacks_Click(object sender, RoutedEventArgs e)
        {
            _player.Energy = Math.Min(100, _player.Energy + 10);
            UpdateUI();
        }

        private void CookMeal_Click(object sender, RoutedEventArgs e)
        {
            _player.Energy = Math.Min(100, _player.Energy + 25);
            UpdateUI();
        }

        private void OrderFood_Click(object sender, RoutedEventArgs e)
        {
            if (_player.Money < 20)
            {
                ShowError("Not enough money to order food.");
                return;
            }

            _player.Money -= 20;
            _player.Energy = Math.Min(100, _player.Energy + 20);
            UpdateUI();
        }

        // =========================
        // CLOTHES ACTIONS
        // =========================
        private void CasualOutfit_Click(object sender, RoutedEventArgs e)
        {
            BuyOutfit("Casual Outfit", "casual", 50);
        }

        private void FormalOutfit_Click(object sender, RoutedEventArgs e)
        {
            BuyOutfit("Formal Outfit", "formal", 150);
        }

        private void HoodieOutfit_Click(object sender, RoutedEventArgs e)
        {
            BuyOutfit("Developer Hoodie", "hoodie", 100);
        }

        // =========================
        // CORE LOGIC
        // =========================
        private void BuyOutfit(string name, string outfitKey, int price)
        {
            var result = MessageBox.Show(
                $"Do you want to buy {name} for ${price}?",
                "Confirm Purchase",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            if (_player.Money < price)
            {
                ShowError("Not enough money to buy this outfit.");
                return;
            }

            _player.Money -= price;
            _player.CurrentOutfitPath = outfitKey;
            ChangeOutfitImage(outfitKey);
            UpdateUI();
        }

        private void ChangeOutfitImage(string outfit)
        {
            string path = outfit switch
            {
                "casual" => "/Assets/Characters/player_casual.png",
                "formal" => "/Assets/Characters/player_formal.png",
                "hoodie" => "/Assets/Characters/player_hoodie.png",
                _ => "/Assets/Characters/player_default.png"
            };

            PlayerCharacterImage.Source =
                new BitmapImage(new Uri(path, UriKind.Relative));
        }

        // =========================
        // UI HELPERS
        // =========================
        private void UpdateUI()
        {
            EnergyText.Text = $"Energy: {_player.Energy}%";
            MoneyText.Text = $"Money: ${_player.Money}";
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Action Failed",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}
