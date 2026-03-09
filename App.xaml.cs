using StudentCookbook.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StudentCookbook
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var settings = SettingsService.Load();

            var theme = new ResourceDictionary
            {
                Source = new Uri(settings.SelectedTheme, UriKind.Relative)
            };

            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}
