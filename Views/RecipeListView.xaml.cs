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
    /// Logika interakcji dla klasy RecipeListView.xaml
    /// </summary>
    public partial class RecipeListView : UserControl
    {
        public RecipeListView()
        {
            InitializeComponent();
            GenerateTestRecipes();
        }

        private void GenerateTestRecipes()
        {
            for (int i = 1; i <= 20; i++)
            {
                Border card = new Border
                {
                    Width = 220,
                    Height = 250,
                    CornerRadius = new CornerRadius(15),
                    Margin = new Thickness(10),
                };

                card.SetResourceReference(Control.BackgroundProperty, "CardBackgroundBrush");
                card.SetResourceReference(Control.ForegroundProperty, "PrimaryTextBrush");

                card.Cursor = Cursors.Hand;
                card.MouseLeftButtonUp += Card_Click;

                StackPanel stack = new StackPanel();

                Border imagePlaceholder = new Border
                {
                    Height = 170,
                    Background = System.Windows.Media.Brushes.LightGray,
                    CornerRadius = new CornerRadius(15, 15, 0, 0)
                };

                TextBlock title = new TextBlock
                {
                    Text = "Przepis testowy " + i,
                    Margin = new Thickness(10),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 16,
                    TextWrapping = TextWrapping.Wrap
                };

                stack.Children.Add(imagePlaceholder);
                stack.Children.Add(title);

                card.Child = stack;

                RecipesWrapPanel.Children.Add(card);
            }
        }

        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            RecipeDetailsView details = new RecipeDetailsView();
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.MainContentArea.Content = details;
        }

    }
}
