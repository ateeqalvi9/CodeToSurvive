using CodeToSurvive.DLL;
using CodeToSurvive.Services;
using CodeToSurvive_P1;
using System.Windows;
using static CodeToSurvive.DLL.Player;

namespace CodeToSurvive
{
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            var player = GameSaveService.CreateNewGame();
            MainWindow mw = new MainWindow(player);
            mw.Show();
            Close();
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            var player = GameSaveService.LoadLastGame();
            if (player == null)
            {
                MessageBox.Show("No saved game found.");
                return;
            }
            MainWindow mw = new MainWindow(player);
            mw.Show();
            Close();
        }
    }
}
