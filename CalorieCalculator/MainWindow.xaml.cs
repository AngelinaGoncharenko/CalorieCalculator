using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace CalorieCalculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        private double _totalWeight;
        public double TotalWeight
        {
            get { return _totalWeight; }
            set
            {
                _totalWeight = value;
                OnPropertyChanged(nameof(TotalWeight));
            }
        }
        private double _totalCalories;
        public double TotalCalories
        {
            get { return _totalCalories; }
            set
            {
                _totalCalories = value;
                OnPropertyChanged(nameof(TotalCalories));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Ingredients = new ObservableCollection<Ingredient>();
            TotalWeight = 0;
            TotalCalories = 0;
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow addIngredientWindow = new AddIngredientWindow();
            if (addIngredientWindow.ShowDialog() == true)
            {
                Ingredient newIngredient = addIngredientWindow.GetIngredient();
                Ingredients.Add(newIngredient);
                TotalWeight += newIngredient.Weight;
                TotalCalories += newIngredient.Calories;
            }
        }

        private void DeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is Ingredient selectedIngredient)
            {
                TotalWeight -= selectedIngredient.Weight;
                TotalCalories -= selectedIngredient.Calories;
                Ingredients.Remove(selectedIngredient);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DishNameTextBox.Text))
            {
                MessageBox.Show("Please enter the dish name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Ingredients.Count == 0)
            {
                MessageBox.Show("Please add at least one ingredient.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter("dish.txt"))
                {
                    writer.WriteLine($"Dish Name: {DishNameTextBox.Text}");
                    writer.WriteLine();

                    writer.WriteLine("Ingredients:");

                    foreach (Ingredient ingredient in Ingredients)
                    {
                        double roundedCalories = Math.Round(ingredient.CaloriesPer100g, 2);
                        writer.WriteLine($"{ingredient.Name}, Weight(g): {ingredient.Weight}, Calories (per 100g): {roundedCalories}");
                    }
                    writer.WriteLine();
                    writer.WriteLine($"Total Weight: {TotalWeight}");
                    writer.WriteLine($"Total Calories: {TotalCalories}");
                }

                MessageBox.Show("Dish saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while saving dish: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

