using System.Configuration;
using System.Data;
using System.Windows;

namespace CodeToSurvive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            StartupWindow win = new StartupWindow();
            win.Show();
        }

    }

}
