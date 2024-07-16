using System;
using System.ComponentModel.DataAnnotations;
using ButterMtn_296.Models;

namespace ButterMtn_296.Models
{
	public class LogEntry
	{
		[Key]
		public int LogEntryID { get; set; }
        [Required(ErrorMessage = "Headline is required.")]
        [StringLength(100, ErrorMessage = "That headline is too long. It must be under 100 characters.")]
        public string Headline { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength (1000, ErrorMessage = "That Description is too long. It must be under 1000 characters.")]
        public string Description { get; set; }
		public int Rating { get; set; }
		public DateTime LogDate { get; set; }
		public AppUser LogUser { get; set; }
		
		//FK to link to Recipe
		public int RecipeID { get; set; }
	}
}

