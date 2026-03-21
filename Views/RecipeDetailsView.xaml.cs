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
using StudentCookbook.Repositories;
using StudentCookbook.Model;

namespace StudentCookbook.Views
{
    /// <summary>
    /// Logika interakcji dla klasy RecipeDetailsView.xaml
    /// </summary>
    public partial class RecipeDetailsView : UserControl
    {
        private Recipe _recipe;

        public RecipeDetailsView(Recipe recipe)
        {
            InitializeComponent();

            _recipe = recipe;
            LoadData();
        }

        private void LoadData()
        {
            TitleTextBlock.Text = _recipe.Title;
            IngredientsTextBlock.Text = _recipe.Ingredients;
            InstructionsTextBlock.Text = _recipe.Instructions;

            try
            {
                string imagePath;

                if (!string.IsNullOrWhiteSpace(_recipe.ImagePath))
                {
                    imagePath = $"pack://application:,,,/Images/{_recipe.ImagePath}";
                }
                else
                {
                    imagePath = "pack://application:,,,/Images/placeholder-food.jpg";
                }

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                RecipeImage.Source = bitmap;
            }
            catch
            {
                var fallback = new BitmapImage();
                fallback.BeginInit();
                fallback.UriSource = new Uri("pack://application:,,,/Images/placeholder-food.jpg");
                fallback.CacheOption = BitmapCacheOption.OnLoad;
                fallback.EndInit();

                RecipeImage.Source = fallback;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.MainContentArea.Content = new RecipeListView();
        }
    }
}
