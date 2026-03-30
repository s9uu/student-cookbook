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
using Microsoft.Win32;
using System.IO;

namespace StudentCookbook
{
    public partial class AddRecipeWindow : Window
    {
        private IRecipeRepository repository;
        private string selectedImageFullPath = ""; //zmienna przechowuje ścieżkę do wybranego pliku graficznego

        public AddRecipeWindow()
        {
            InitializeComponent();
            repository = new RecipeRepository();
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

        //Metoda obsługująca zdarzenia wciśnięcia przycisku dodania przepisu
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";

            //Jeżeli wybrano w oknie plik graficzny, to następuje próba
            //skopiowania go do folderu przechowującego dane programu
            if (!string.IsNullOrEmpty(selectedImageFullPath))
            {
                fileName = System.IO.Path.GetFileName(selectedImageFullPath);

                //Ustalenie docelowej ścieżki kopiowania pliku, z tą samą nazwą
                string targetPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Images",
                    fileName
                );

                //Obsługa wyjątku błędu kopiowania
                try
                {
                    File.Copy(selectedImageFullPath, targetPath, true); //"true" włącza nadpisywanie
                }
                catch
                {
                    MessageBox.Show("Błąd kopiowania obrazu");
                }
            }

            //Wczytanie danych z formularza do instancji obiektu
            var recipe = new Recipe
            {
                Title = TitleTextBox.Text,
                Ingredients = IngredientsTextBox.Text,
                Instructions = InstructionsTextBox.Text,
                ImagePath = fileName //w bazie danych zapisana jest tylko nazwa pliku, nie cała ścieżka
            };

            repository.Add(recipe); //metoda z repozytorium dodająca przepis do bazy
            this.Close();
        }
    }
}
