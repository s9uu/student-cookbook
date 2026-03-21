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
        private readonly string connectionString;

        public RecipeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public List<Recipe> GetAll()
        {
            var recipes = new List<Recipe>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Title, Ingredients, Instructions, ImagePath FROM Recipes";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
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

        // reszta metod na razie pusta
        public Recipe GetById(int id) { throw new System.NotImplementedException(); }
        public void Add(Recipe recipe)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"
                                INSERT INTO Recipes (Title, Ingredients, Instructions, ImagePath)
                                VALUES (@Title, @Ingredients, @Instructions, @ImagePath)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                    command.Parameters.AddWithValue("@ImagePath", recipe.ImagePath);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void Update(Recipe recipe)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"
                                UPDATE Recipes
                                SET Title = @Title,
                                Ingredients = @Ingredients,
                                Instructions = @Instructions,
                                ImagePath = @ImagePath
                                WHERE Id = @Id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                    command.Parameters.AddWithValue("@ImagePath", recipe.ImagePath);
                    command.Parameters.AddWithValue("@Id", recipe.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Recipes WHERE Id = @Id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Recipe> SearchByTitle(string title) { throw new System.NotImplementedException(); }
    }
}
