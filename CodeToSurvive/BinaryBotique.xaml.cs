using CodeToSurvive.DLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BinaryBotique.xaml
    /// </summary>
    public partial class BinaryBotique : Window
    {
        private Player _player;
        private List<Outfit> _outfits;
        private Outfit _selectedOutfit;
        public BinaryBotique(Player player)
        {
            InitializeComponent();
            _player = player;

            LoadOutfits();
            OutfitList.ItemsSource = _outfits;

            UpdatePlayerPreview();
        }
        private void LoadOutfits()
        {
            _outfits = new List<Outfit>
            {
                new Outfit { Name="Casual Hoodie", Description="Comfy but cool.", Price=40, StyleBonus=2, ImagePath="/Assets/Outfits/hoodie.png" },
                new Outfit { Name="Formal Suit", Description="Professional vibes.", Price=100, StyleBonus=6, ImagePath="/Assets/Outfits/suit.png" },
                new Outfit { Name="Streetwear Jacket", Description="Trendy coder look.", Price=75, StyleBonus=4, ImagePath="/Assets/Outfits/jacket.png" }
            };
        }

        private void OutfitItem_Click(object sender, RoutedEventArgs e)
        {
            _selectedOutfit = (sender as Button)?.DataContext as Outfit;
            if (_selectedOutfit == null) return;

            OutfitNameText.Text = _selectedOutfit.Name;
            OutfitDescText.Text = _selectedOutfit.Description;
            OutfitPriceText.Text = "$" + _selectedOutfit.Price;
            OutfitStyleText.Text = "+" + _selectedOutfit.StyleBonus + " Style";

            PlayerOutfitImage.Source =
                new BitmapImage(new System.Uri(_selectedOutfit.ImagePath, System.UriKind.Relative));
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOutfit == null)
            {
                MessageBox.Show("Select an outfit first.");
                return;
            }

            if (_player.Money < _selectedOutfit.Price)
            {
                MessageBox.Show("You don't have enough money!");
                return;
            }

            if (_player.OwnedOutfits.Contains(_selectedOutfit))
            {
                MessageBox.Show("You already own this outfit!");
                return;
            }

            _player.Money -= _selectedOutfit.Price;
            _player.OwnedOutfits.Add(_selectedOutfit);

            MessageBox.Show("Outfit purchased!");
        }

        private void BtnEquip_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOutfit == null)
            {
                MessageBox.Show("Select an outfit first.");
                return;
            }

            if (!_player.OwnedOutfits.Contains(_selectedOutfit))
            {
                MessageBox.Show("You must buy the outfit first.");
                return;
            }

            _player.CurrentOutfit = _selectedOutfit;
            _player.Style = _selectedOutfit.StyleBonus;

            UpdatePlayerPreview();

            MessageBox.Show("Outfit equipped!");
        }

        private void UpdatePlayerPreview()
        {
            if (_player.CurrentOutfit != null)
            {
                PlayerOutfitImage.Source =
                    new BitmapImage(new System.Uri(_player.CurrentOutfit.ImagePath, System.UriKind.Relative));
            }
        }
    }
}
