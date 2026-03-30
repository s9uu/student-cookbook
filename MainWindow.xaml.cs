using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Configuration;
using StudentCookbook.Repositories;
using StudentCookbook.Views;

namespace StudentCookbook
{
    public partial class MainWindow : Window
    {
        //Konstruktor domyślnie ustawia widok na listę przepisów
        public MainWindow()
        {
            InitializeComponent();
            MainContentArea.Content = new Views.RecipeListView();
        }

        //Metody obsługujące zdarzenie kliknięcia w dane przyciski
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
            RefreshRecipes(); //odświeżanie widoku listy by dynamicznie uwzględnić ewentualne zmiany
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditRecipeWindow();
            editWindow.Owner = this;
            editWindow.ShowDialog();
            RefreshRecipes();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new DeleteRecipeWindow();
            deleteWindow.Owner = this;
            deleteWindow.ShowDialog();
            RefreshRecipes();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainContentArea.Content is RecipeListView view)
            {
                view.Search(SearchTextBox.Text);
            }
        }

        public void RefreshRecipes()
        {
            if (MainContentArea.Content is RecipeListView view)
            {
                view.Refresh(); //metoda z pliku RecipeListView.xaml.cs odświeżająca zawartość widoku
            }
        }
    }
}