using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCookbook
{
    internal class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Ingredients { get; set; } = "";

        public string Instructions { get; set; } = "";

        public string ImagePath { get; set; } = "";
    }
}
