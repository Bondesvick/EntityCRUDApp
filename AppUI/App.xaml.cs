using CodeFirstModel.Data;
using System.Windows;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ICrudOperations operations = GlobalConfig.Inject();

            MainWindow mw = new MainWindow(operations);
            mw.Show();
        }
    }
}