using System;
using System.Windows;
using ClickOnce;

namespace ClickOnceAutoStart
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            if (!ClickOnceHelper.IsApplicationNetworkDeployed)
            {
                DemoValid.Visibility = Visibility.Visible;
                AutoStartApp.IsEnabled = false;
                AutoStartUrl.IsEnabled = false;
            }

            // Display autostart status based on shortcut types and existence
            AutoStartApp.IsChecked = ClickOnceHelper.DoesStartupShortcutExist(ClickOnceHelper.AssemblyProductName,
                                                                              ClickOnceHelper.ShortcutType.Application);

            AutoStartUrl.IsChecked = ClickOnceHelper.DoesStartupShortcutExist(ClickOnceHelper.AssemblyProductName,
                                                                              ClickOnceHelper.ShortcutType.Url);

            // Provide system folder/file locations for user reference
            StartMenuProgramLocation.Text = "Start Menu Program Shortcut: " +
                                            ClickOnceHelper.GetProgramShortcut(ClickOnceHelper.AssemblyProductName,
                                                                               ClickOnceHelper.AssemblyCompanyName);
            StartupLocation.Text = "Startup Location: " + Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        }

        private void AutoStartApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppShortcut.AutoStart((Boolean) AutoStartApp.IsChecked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AutoStartUrl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UrlShortcut.AutoStart((Boolean) AutoStartUrl.IsChecked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}