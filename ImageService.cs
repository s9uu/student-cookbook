using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StudentCookbook
{
    public static class ImageService
    {
        //Metoda obsługująca wczytanie pliku graficznego do wyświetlenia w oknie programu
        public static BitmapImage Load(string fileName)
        {
            //Obsługa wyjątku błędu przy wczytywaniu
            try
            {
                string path; //zmienna przechowująca ścieżkę do pliku

                //Jeżeli poprawnie wybrano plik, ścieżka będzie ustawiona do niego
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    path = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Images",
                        fileName
                    );
                }
                //Jeżeli pole wyboru będzie puste, wybrany będzie specjalny placeholder
                else
                {
                    path = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Images",
                        "placeholder-food.jpg"
                    );
                }

                //konwersja pliku graficznego na bitmapę
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                return bitmap;
            }
            //W przypadku innych nieoczekiwanych problemów z wczytaniem pliku, również zostanie wczytany placeholder
            catch
            {
                var fallback = new BitmapImage();
                fallback.BeginInit();
                fallback.UriSource = new Uri(
                    System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Images",
                        "placeholder-food.jpg"
                    ),
                    UriKind.Absolute
                );
                fallback.CacheOption = BitmapCacheOption.OnLoad;
                fallback.EndInit();

                return fallback;
            }
        }
    }
}
