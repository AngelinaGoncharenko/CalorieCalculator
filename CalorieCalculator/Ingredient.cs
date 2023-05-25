using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCalculator
{
    public class Ingredient
    {
        public string? Name { get; set; }
        public double Weight { get; set; }
        public double CaloriesPer100g { get; set; }
        public double Calories { get { return (Weight / 100) * CaloriesPer100g; } }
    }
}
