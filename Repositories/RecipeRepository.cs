using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentCookbook.Model;

namespace StudentCookbook.Repositories
{
    internal class RecipeRepository : IRecipeRepository
    {
        //Zmienna przechowująca informacje o konfiguracji połączenia
        private readonly string connectionString;

        public RecipeRepository()
        {
            //w App.config jest ustanowione połączenie z plikiem Database.db pod nazwą "Default"
            connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        //Metoda pobierająca wszystkie wiersze z bazy do listy
        public List<Recipe> GetAll()
        {
            var recipes = new List<Recipe>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open(); //otwarcie połączenia

                string query = "SELECT Id, Title, Ingredients, Instructions, ImagePath FROM Recipes"; //kwerenda sql

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    //Pętla odczytuje kolejne wiersze tabeli i dodaje zebrane informacje do listy obiektów
                    while (reader.Read())
                    {
                        var recipe = new Recipe
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Ingredients = reader.GetString(2),
                            Instructions = reader.GetString(3),
                            ImagePath = reader.GetString(4)
                        };

                        recipes.Add(recipe);
                    }
                }
            }

            return recipes;
        }

        //Metoda dodawania nowego przepisu
        public void Add(Recipe recipe)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open(); //otwarcie połączenia

                string query = @"
                                INSERT INTO Recipes (Title, Ingredients, Instructions, ImagePath)
                                VALUES (@Title, @Ingredients, @Instructions, @ImagePath)"; //kwerenda sql

                //zamienia dane pozycje z kwerendy na odpowiednie wartości
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                    command.Parameters.AddWithValue("@ImagePath", recipe.ImagePath);

                    command.ExecuteNonQuery(); //wykonuje kwerendę
                }
            }
        }

        //Metoda zmiany szczegółów przepisu
        public void Update(Recipe recipe)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open(); //otwarcie połączenia

                string query = @"
                                UPDATE Recipes
                                SET Title = @Title,
                                Ingredients = @Ingredients,
                                Instructions = @Instructions,
                                ImagePath = @ImagePath
                                WHERE Id = @Id"; //kwerenda sql

                //zamienia dane pozycje z kwerendy na odpowiednie wartości
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                    command.Parameters.AddWithValue("@ImagePath", recipe.ImagePath);
                    command.Parameters.AddWithValue("@Id", recipe.Id);

                    command.ExecuteNonQuery(); //wykonuje kwerendę
                }
            }
        }

        //Metoda usuwania przepisu
        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open(); //otwarcie połączenia

                string query = "DELETE FROM Recipes WHERE Id = @Id"; //kwerenda sql

                //zamienia dane pozycje z kwerendy na odpowiednie wartości
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery(); //wykonuje kwerendę
                }
            }
        }
    }
}
