using System;
namespace ButterMtn_296.Models
{
	public class RecipeVM
	{ 
		public Recipe Recipe { get; set; }
		public List<Ingredient>? Ingredients {get; set;}
		public List<LogEntry>? Logs { get; set; }
		public string vmUserName { get; set; }
    }
}

