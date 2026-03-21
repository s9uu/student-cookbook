using StudentCookbook.Repositories;
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
using StudentCookbook.Repositories;
using StudentCookbook.Model;

namespace StudentCookbook
{
    public partial class DeleteRecipeWindow : Window
    {
        private IRecipeRepository repository;

        public DeleteRecipeWindow()
        {
            InitializeComponent();

            repository = new RecipeRepository();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            RecipeComboBox.ItemsSource = repository.GetAll();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is Recipe recipe)
            {
                var result = MessageBox.Show(
                    $"Czy na pewno chcesz usunąć '{recipe.Title}'?",
                    "Potwierdzenie",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    repository.Delete(recipe.Id);
                    this.Close();
                }
            }
        }
    }
}
