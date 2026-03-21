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
    public partial class EditRecipeWindow : Window
    {
        private IRecipeRepository repository;

        public EditRecipeWindow()
        {
            InitializeComponent();

            repository = new RecipeRepository();

            LoadRecipes();
        }

        private void LoadRecipes()
        {
            var recipes = repository.GetAll();
            RecipeComboBox.ItemsSource = recipes;
        }
        private void RecipeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is Recipe recipe)
            {
                TitleTextBox.Text = recipe.Title;
                IngredientsTextBox.Text = recipe.Ingredients;
                InstructionsTextBox.Text = recipe.Instructions;
                ImagePathTextBox.Text = recipe.ImagePath;
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is Recipe recipe)
            {
                recipe.Title = TitleTextBox.Text;
                recipe.Ingredients = IngredientsTextBox.Text;
                recipe.Instructions = InstructionsTextBox.Text;
                recipe.ImagePath = ImagePathTextBox.Text;

                repository.Update(recipe);

                this.Close();
            }
        }
    }
}
