using StudentCookbook.Model;
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

namespace StudentCookbook
{
    /// <summary>
    /// Logika interakcji dla klasy AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe
            {
                Title = TitleTextBox.Text,
                Ingredients = IngredientsTextBox.Text,
                Instructions = InstructionsTextBox.Text,
                ImagePath = ImagePathTextBox.Text
            };

            var repo = new RecipeRepository();
            repo.Add(recipe);

            this.Close();
        }
    }
}
