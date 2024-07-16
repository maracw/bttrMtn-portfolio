using System;
using ButterMtn_296.Models;
using System.ComponentModel.DataAnnotations;

namespace ButterMtn_296.Models
{
	public class Recipe
	{
        private readonly List<LogEntry> logs = new(); // Backing field for Logs
        private readonly List<Ingredient> ingredients = new(); // Backing field for ingredients
        //root entity
        [Key]
        public int RecipeID { get; set; }
        public DateTime RecipeDate { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "That Title is too long. It must be under 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [Range(1, 5, ErrorMessage ="5 is the most difficult. This must be between 1 and 5.")]
        public int Difficulty { get; set; }

        [Required(ErrorMessage = "Instructions are required.")]
        [StringLength (2000, MinimumLength =3)]
        public string Instructions{ get; set; }

        //to do later
        //[RegularExpression("^images/",
                //ErrorMessage = "Super secret image path must start with /images/.")]
        public string? ImageURL { get; set; }
        public string? Category { get; set; }
        public AppUser? RecipeUser { get; set; } //aggregation 
        //nullable to allow for VM 
        public ICollection<LogEntry> Logs => logs; //compostition Logs are part of a Recipe
        public ICollection<Ingredient> Ingredients => ingredients; //compostition Ingredients are part of a Recipe
    }
}
