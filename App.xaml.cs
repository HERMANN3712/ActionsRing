using ActionsRing.Views;
using System.Reflection;
using System.Windows;
using FontStyle = System.Drawing.FontStyle;


namespace ActionsRing
{
    public partial class App : System.Windows.Application
    {
        private NotifyIcon? _trayIcon;
        private MainWindow? _mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _mainWindow = new MainWindow();
            _mainWindow.Hide();

            var menu = new ContextMenuStrip();
            var showItem = new ToolStripMenuItem("Afficher / Masquer");
            showItem.Font = new Font(showItem.Font, FontStyle.Bold);
            showItem.Click += (_, _) => ToggleMainWindow();

            var quitItem = new ToolStripMenuItem("Quitter");
            quitItem.Click += (_, _) => ExitApplication();

            menu.Items.Add(showItem);
            menu.Items.Add(quitItem);

            _trayIcon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Visible = true,
                Text = "Actions Ring",
                ContextMenuStrip = menu
            };
            _trayIcon.Click += (_, _) => ToggleMainWindow();
        }

        private void ToggleMainWindow()
        {
            if (_mainWindow is null)
                return;

            if (_mainWindow.IsVisible)
            {
                _mainWindow.Hide();
            }
            else
            {
                _mainWindow.ShowRing();
            }
        }

        private void ExitApplication()
        {
            if (_trayIcon is not null)
            {
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
                _trayIcon = null;
            }

            if (_mainWindow is not null)
            {
                _mainWindow.ForceClose();
            }

            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_trayIcon is not null)
            {
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }

            base.OnExit(e);
        }
    }
}