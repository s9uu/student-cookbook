using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StudentCookbook.Model;

namespace StudentCookbook.Repositories
{
    internal interface IRecipeRepository
    {
        List<Recipe> GetAll();

        Recipe GetById(int id);

        void Add(Recipe recipe);

        void Update(Recipe recipe);

        void Delete(int id);

        List<Recipe> SearchByTitle(string title);
    }
}
