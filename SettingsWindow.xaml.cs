using StudentCookbook.Configuration;
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

namespace StudentCookbook
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Light_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Themes/LightTheme.xaml");
        }

        private void Dark_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Themes/DarkTheme.xaml");
        }

        private void ChangeTheme(string themePath)
        {
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newTheme);

            var settings = new AppSettings
            {
                SelectedTheme = themePath
            };

            SettingsService.Save(settings);
        }
    }
}
