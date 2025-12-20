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
    /// Interaction logic for StockExchange.xaml
    /// </summary>
    public partial class StockExchange : Window
    {
        private readonly Player _player;

        // Simple stock model
        private class Stock
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public string Trend { get; set; }
            public string Risk { get; set; }
            public int Owned { get; set; }
        }

        private Dictionary<string, Stock> _stocks = new Dictionary<string, Stock>();
        private Stock _selectedStock;

        public StockExchange(Player player)
        {
            InitializeComponent();
            _player = player;

            InitializeMarket();
            UpdateHUD();
        }

        // ==============================
        // MARKET INITIALIZATION
        // ==============================
        private void InitializeMarket()
        {
            _stocks["TECH Corp (TC)"] = new Stock
            {
                Name = "TECH Corp (TC)",
                Price = 120,
                Trend = "Up",
                Risk = "Medium",
                Owned = 0
            };

            _stocks["CODE Systems (CS)"] = new Stock
            {
                Name = "CODE Systems (CS)",
                Price = 90,
                Trend = "Stable",
                Risk = "Low",
                Owned = 0
            };

            _stocks["DATA Labs (DL)"] = new Stock
            {
                Name = "DATA Labs (DL)",
                Price = 150,
                Trend = "Down",
                Risk = "High",
                Owned = 0
            };

            _stocks["AI Ventures (AI)"] = new Stock
            {
                Name = "AI Ventures (AI)",
                Price = 200,
                Trend = "Up",
                Risk = "High",
                Owned = 0
            };

            _stocks["GAME Studios (GS)"] = new Stock
            {
                Name = "GAME Studios (GS)",
                Price = 75,
                Trend = "Stable",
                Risk = "Low",
                Owned = 0
            };

            StockList.SelectionChanged += StockList_SelectionChanged;
            BtnBuyStock.Click += BtnBuyStock_Click;
            BtnSellStock.Click += BtnSellStock_Click;
            BtnExitStockExchange.Click += BtnExitStockExchange_Click;
        }

        // ==============================
        // STOCK SELECTION
        // ==============================
        private void StockList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StockList.SelectedItem == null)
                return;

            string key = StockList.SelectedItem.ToString();
            _selectedStock = _stocks[key];

            StockTitleText.Text = _selectedStock.Name;
            StockDescriptionText.Text =
                "Market performance varies daily. Higher risk stocks can give higher returns.";
            StockPriceText.Text = $"${_selectedStock.Price}";
            StockTrendText.Text = _selectedStock.Trend;
            StockRiskText.Text = _selectedStock.Risk;
        }

        // ==============================
        // BUY STOCK
        // ==============================
        private void BtnBuyStock_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStock == null)
            {
                MessageBox.Show("Select a stock first.");
                return;
            }

            if (_player.Money < _selectedStock.Price)
            {
                MessageBox.Show("Not enough money to buy this stock.");
                return;
            }

            _player.Money -= _selectedStock.Price;
            _selectedStock.Owned++;

            MessageBox.Show($"Bought 1 share of {_selectedStock.Name}.");

            UpdateHUD();
            UpdatePortfolio();
        }

        // ==============================
        // SELL STOCK
        // ==============================
        private void BtnSellStock_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStock == null || _selectedStock.Owned <= 0)
            {
                MessageBox.Show("You don't own this stock.");
                return;
            }

            _player.Money += _selectedStock.Price;
            _selectedStock.Owned--;

            MessageBox.Show($"Sold 1 share of {_selectedStock.Name}.");

            UpdateHUD();
            UpdatePortfolio();
        }

        // ==============================
        // UPDATE HUD & PORTFOLIO
        // ==============================
        private void UpdateHUD()
        {
            StockMoneyText.Text = $"💰 Money: ${_player.Money}";
            UpdatePortfolio();
        }

        private void UpdatePortfolio()
        {
            PortfolioList.Items.Clear();
            int totalValue = 0;

            foreach (var stock in _stocks.Values)
            {
                if (stock.Owned > 0)
                {
                    int value = stock.Owned * stock.Price;
                    totalValue += value;
                    PortfolioList.Items.Add($"{stock.Name} x{stock.Owned} (${value})");
                }
            }

            InvestmentValueText.Text = $"📈 Investment Value: ${totalValue}";
        }

        // ==============================
        // EXIT
        // ==============================
        private void BtnExitStockExchange_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
