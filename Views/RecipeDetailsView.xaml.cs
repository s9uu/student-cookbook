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

namespace StudentCookbook.Views
{
    /// <summary>
    /// Logika interakcji dla klasy RecipeDetailsView.xaml
    /// </summary>
    public partial class RecipeDetailsView : UserControl
    {
        public RecipeDetailsView()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.MainContentArea.Content = new RecipeListView();
        }
    }
}
