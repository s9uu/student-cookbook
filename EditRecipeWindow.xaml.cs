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
using Microsoft.Win32;
using System.IO;

namespace StudentCookbook
{
    public partial class EditRecipeWindow : Window
    {
        private IRecipeRepository repository;
        private string selectedImageFullPath = "";

        public EditRecipeWindow()
        {
            InitializeComponent();

            repository = new RecipeRepository();
            LoadRecipes(); //metoda wczytuje ponownie wszystkie przepisy z bazy, by móc je wyświetlić w ComboBoxie
        }

        //Metoda wypełniająca zawartość ComboBoxa listą przepisów
        private void LoadRecipes()
        {
            //metoda GetAll z repozytorium wczytuje do listy wszystkie wiersze z bazy
            RecipeComboBox.ItemsSource = repository.GetAll();
        }

        //Metoda obsługująca zdarzenie zmiany wybranego w ComboBoxie przepisu
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

        //Metoda obsługująca okno dialogowe wybrania pliku graficznego
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png";

            if (dialog.ShowDialog() == true)
            {
                selectedImageFullPath = dialog.FileName;
                ImagePathTextBox.Text = System.IO.Path.GetFileName(selectedImageFullPath);
            }
        }

        //Metoda obsługująca zdarzenie wciśnięcia przycisku do potwierdzenia zmian
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is Recipe recipe)
            {
                recipe.Title = TitleTextBox.Text;
                recipe.Ingredients = IngredientsTextBox.Text;
                recipe.Instructions = InstructionsTextBox.Text;

                //obsługa zdjęcia, jeśli nie wybrano nowego zostaje stare ImagePath
                if (!string.IsNullOrEmpty(selectedImageFullPath))
                {
                    string fileName = System.IO.Path.GetFileName(selectedImageFullPath);

                    string targetPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Images",
                        fileName
                    );

                    try
                    {
                        File.Copy(selectedImageFullPath, targetPath, true);
                        recipe.ImagePath = fileName;
                    }
                    catch
                    {
                        MessageBox.Show("Błąd kopiowania obrazu");
                    }
                }

                repository.Update(recipe); //metoda z repozytorium aktualizująca dane w bazie
                this.Close();
            }
        }
    }
}
