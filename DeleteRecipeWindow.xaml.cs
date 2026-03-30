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
using System.IO;

namespace StudentCookbook
{
    public partial class DeleteRecipeWindow : Window
    {
        private IRecipeRepository repository;

        public DeleteRecipeWindow()
        {
            InitializeComponent();

            repository = new RecipeRepository();
            LoadRecipes(); //metoda wczytuje ponownie wszystkie przepisy z bazy, by móc je wyświetlić w ComboBoxie
        }

        //Metoda wypełniająca zawartość ComboBoxa listą przepisów
        private void LoadRecipes()
        {
            RecipeComboBox.ItemsSource = repository.GetAll();
        }

        //Metoda obsługująca zdarzenie wciśnięcia przycisku do potwierdzenia usunięcia
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is Recipe recipe)
            {
                //Obsługa wyjątku błędu przy usuwaniu
                try
                {
                    //usuwa plik graficzny jeśli wciąż istnieje w folderze "Images" programu
                    if (!string.IsNullOrWhiteSpace(recipe.ImagePath))
                    {
                        string fullPath = System.IO.Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "Images",
                            recipe.ImagePath
                        );

                        if (System.IO.File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }

                    repository.Delete(recipe.Id); //metoda z repozytorium usuwająca wybraną zawartość z bazy
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas usuwania: " + ex.Message);
                }
            }
        }
    }
}
