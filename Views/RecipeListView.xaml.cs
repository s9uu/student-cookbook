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
    public partial class RecipeListView : UserControl
    {
        private IRecipeRepository repository;
        
        public RecipeListView()
        {
            InitializeComponent();
            
            repository = new RecipeRepository();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            var recipes = repository.GetAll();

            RecipesWrapPanel.Children.Clear();

            foreach (var recipe in recipes)
            {
                AddRecipeTile(recipe);
            }
        }

        private void AddRecipeTile(Recipe recipe)
        {
            var tile = new Border
            {
                Width = 220,
                Height = 250,
                Margin = new Thickness(10),
                CornerRadius = new CornerRadius(15),
                Cursor = Cursors.Hand
            };

            tile.SetResourceReference(Control.BackgroundProperty, "CardBackgroundBrush");
            tile.SetResourceReference(Control.ForegroundProperty, "PrimaryTextBrush");

            var stack = new StackPanel();

            var image = new Image
            {
                Height = 170,
                Stretch = Stretch.UniformToFill
            };

            try
            {
                string imagePath;

                if (!string.IsNullOrWhiteSpace(recipe.ImagePath))
                {
                    imagePath = $"pack://application:,,,/Images/{recipe.ImagePath}";
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

                image.Source = bitmap;
            }
            catch
            {
                var fallback = new BitmapImage();
                fallback.BeginInit();
                fallback.UriSource = new Uri("pack://application:,,,/Images/placeholder-food.jpg");
                fallback.CacheOption = BitmapCacheOption.OnLoad;
                fallback.EndInit();

                image.Source = fallback;
            }


            var text = new TextBlock
            {
                Text = recipe.Title,
                Margin = new Thickness(10),
                FontWeight = FontWeights.SemiBold,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            };

            stack.Children.Add(image);
            stack.Children.Add(text);

            tile.Child = stack;

            // 🔥 kliknięcie kafelka
            tile.MouseLeftButtonUp += (s, e) =>
            {
                OpenRecipeDetails(recipe);
            };

            RecipesWrapPanel.Children.Add(tile);
        }

        private void OpenRecipeDetails(Recipe recipe)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var detailsView = new RecipeDetailsView(recipe);
            mainWindow.MainContentArea.Content = detailsView;
        }
        public void Refresh()
        {
            LoadRecipes();
        }
    }
}
