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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentCookbook
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContentArea.Content = new Views.RecipeListView();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddRecipeWindow();
            addWindow.Owner = this;
            addWindow.ShowDialog();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditRecipeWindow();
            editWindow.Owner = this;
            editWindow.ShowDialog();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new DeleteRecipeWindow();
            deleteWindow.Owner = this;
            deleteWindow.ShowDialog();
        }

    }
}
