using System;
using System.Windows;

namespace CalorieCalculator
{
    public partial class AddIngredientWindow : Window
    {
        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        public Ingredient GetIngredient()
        {
            return new Ingredient
            {
                Name = IngredientTextBox.Text,
                Weight = Convert.ToDouble(WeightTextBox.Text),
                CaloriesPer100g = Convert.ToDouble(CaloriesTextBox.Text)
            };
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(IngredientTextBox.Text) && !string.IsNullOrEmpty(WeightTextBox.Text) && !string.IsNullOrEmpty(CaloriesTextBox.Text))
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter ingredient, weight, and calories.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

