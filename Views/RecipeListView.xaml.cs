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

        //Metoda wykorzystujące repozytorium do wczytania danych z bazy
        private void LoadRecipes()
        {
            var recipes = repository.GetAll(); //metoda wczytująca wszystkie wiersze z tabeli
            DisplayRecipes(recipes);
        }

        //Metoda służąca do wyświetlenia danych w postaci kafelków
        private void DisplayRecipes(List<Recipe> recipes)
        {
            RecipesWrapPanel.Children.Clear(); //metoda czyszcząca wcześniejsze dane

            //Pętla która tworzy osobny kafelek dla każdego z wiersza danych
            foreach (var recipe in recipes)
            {
                AddRecipeTile(recipe);
            }
        }

        //Metoda tworząca kafelek
        private void AddRecipeTile(Recipe recipe)
        {

            //Ustawienie własności kafelka (borderu)
            var tile = new Border
            {
                Width = 220,
                Height = 250,
                Margin = new Thickness(10),
                CornerRadius = new CornerRadius(15),
                Cursor = Cursors.Hand
            };

            //Przypisanie dynamicznych kolorów (dzięki motywom)
            tile.SetResourceReference(Control.BackgroundProperty, "CardBackgroundBrush");
            tile.SetResourceReference(Control.ForegroundProperty, "PrimaryTextBrush");

            var stack = new StackPanel();

            //Ustawienie własności fragmentu graficznego
            var image = new Image
            {
                Height = 170,
                Stretch = Stretch.UniformToFill
            };

            image.Source = ImageService.Load(recipe.ImagePath); //metoda z pliku klasy ImageService

            //Ustawienie właności fragmentu tekstowego
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

            //Obsługa zdarzenia kliknięcia kafelka
            tile.MouseLeftButtonUp += (s, e) =>
            {
                OpenRecipeDetails(recipe); //po kliknięciu metoda zmieni widok na szczegóły danego przepisu
            };

            RecipesWrapPanel.Children.Add(tile); //dodanie przygotowanego kafelka do WrapPanelu w oknie XAML
        }

        //Metoda zmieniająca widok na szczegóły przepisu
        private void OpenRecipeDetails(Recipe recipe)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var detailsView = new RecipeDetailsView(recipe);
            mainWindow.MainContentArea.Content = detailsView;
        }

        //Metoda obsługująca funkcję wyszukiwania
        public void Search(string text)
        {
            var allRecipes = repository.GetAll(); //metoda z repozytorium wczytuje przepisy

            //Sprawdzenie czy pole wyszukiwanie nie jest puste
            if (string.IsNullOrWhiteSpace(text))
            {
                DisplayRecipes(allRecipes); //jeśli jest, pokazane są wszystkie przepisy
                return;
            }
            
            //W przypadku jeśli nie jest, następuje filtrowanie zawartości listy
            var filtered = allRecipes
                .Where(r => r.Title.ToLower().Contains(text.ToLower()))
                .ToList();

            DisplayRecipes(filtered);
        } 
        
        //Metoda odświeżająca zawartość widoku, by dynamicznie reagowała na zmiany, wykorzystywana w
        //pliku głównego okna MainWindow.xaml.cs
        public void Refresh()
        {
            LoadRecipes();
        }
    }
}
