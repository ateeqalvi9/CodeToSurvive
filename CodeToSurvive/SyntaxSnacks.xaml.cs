using CodeToSurvive.DLL;
using CodeToSurvive.Models;
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
    /// Interaction logic for SyntaxSnacks.xaml
    /// </summary>
    public partial class SyntaxSnacks : Window
    {
        private readonly GameDbContext _db;
        private readonly Player _player;
        private List<MenuMeal> _meals;
        private MenuMeal _selectedMeal;

        public SyntaxSnacks(Player player)
        {
            InitializeComponent();
            _player = player;
            _db = new GameDbContext();
            LoadMeals();
            MealList.ItemsSource = _meals;
            RefreshPlayerStats();
        }
        private void LoadMeals()
        {
            _meals = new List<MenuMeal>
            {
                new MenuMeal { Name="Debug Burger", Description="Greasy but powerful.", Price=20, EnergyRestore=15, ImagePath="/Assets/Meals/BudagetMeal.png"},
                new MenuMeal { Name="Algorithm Pizza", Description="Cheesy problem solving fuel.", Price=35, EnergyRestore=25, ImagePath="/Assets/Meals/HealthyMeal.png"},
                new MenuMeal { Name="Compiler Coffee", Description="Keeps you coding… too long.", Price=10, EnergyRestore=10, ImagePath="/Assets/Meals/StandardMeal.png"}
            };
        }

        private void MealItem_Click(object sender, RoutedEventArgs e)
        {
            _selectedMeal = (sender as Button)?.DataContext as MenuMeal;
            if (_selectedMeal == null) return;

            MealNameText.Text = _selectedMeal.Name;
            MealDescText.Text = _selectedMeal.Description;
            MealPriceText.Text = "$" + _selectedMeal.Price;
            MealEnergyText.Text = "+" + _selectedMeal.EnergyRestore + " Energy";

            MealImage.Source = new BitmapImage(new Uri(_selectedMeal.ImagePath, UriKind.Relative));
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMeal == null)
            {
                MessageBox.Show("Please select a meal first.");
                return;
            }

            if (_player.Money < _selectedMeal.Price)
            {
                MessageBox.Show("You don't have enough money!");
                return;
            }

            // Update player
            _player.Money -= _selectedMeal.Price;
            _player.Energy = Math.Min(100, _player.Energy + _selectedMeal.EnergyRestore);

            _db.Attach(_player);
            _db.Entry(_player).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            // Log purchase
            _db.Meals.Add(new Meal
            {
                PlayerId = _player.Id,
                MealName = _selectedMeal.Name,
                Price = _selectedMeal.Price,
                EnergyGained = _selectedMeal.EnergyRestore,
                PurchaseTime = DateTime.Now
            });

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                return;
            }

            MessageBox.Show($"{_selectedMeal.Name} purchased!");

            RefreshPlayerStats();
        }

        private void RefreshPlayerStats()
        {
            PlayerStatsText.Text = $"Money: ${_player.Money}\nEnergy: {_player.Energy}/100";
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
