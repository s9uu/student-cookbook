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
    public partial class RecipeDetailsView : UserControl
    {
        private Recipe _recipe;

        public RecipeDetailsView(Recipe recipe)
        {
            InitializeComponent();

            _recipe = recipe;
            LoadData();
        }
        
        //Metoda wczytująca dane z obiektu do pozycji w oknie XAML
        private void LoadData()
        {
            TitleTextBlock.Text = _recipe.Title;
            IngredientsTextBlock.Text = _recipe.Ingredients;
            InstructionsTextBlock.Text = _recipe.Instructions;

            RecipeImage.Source = ImageService.Load(_recipe.ImagePath); //metoda z pliku klasy ImageService
        }

        //Metoda obsługująca zdarzenie kliknięcia przycisku powrotu
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.MainContentArea.Content = new RecipeListView(); //zmiana widoku na listę
        }
    }
}
