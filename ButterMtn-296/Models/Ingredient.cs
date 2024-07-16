using System;
using System.ComponentModel.DataAnnotations;

namespace ButterMtn_296.Models
{
	public class Ingredient
	{
		[Key]
		public int IngredientID { get; set; }
		public string Name { get; set; }
		public string Units { get; set; }
		public double Amount { get; set; }

		//FK
		public int RecipeID { get; set; }

        public Ingredient Scale(Ingredient i, double ratio)
        {
			i.Amount = i.Amount * ratio;
			return i;
		}

        public static implicit operator List<object>(Ingredient? v)
        {
            throw new NotImplementedException();
        }
    }

	
}

