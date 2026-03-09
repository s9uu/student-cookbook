using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace StudentCookbook.Configuration
{
    internal class SettingsService
    {
        private static readonly string filePath = "appsettings.json";

        public static void Save(AppSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static AppSettings Load()
        {
            if (!File.Exists(filePath))
                return new AppSettings { SelectedTheme = "Themes/LightTheme.xaml" };

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<AppSettings>(json);
        }
    }
}
