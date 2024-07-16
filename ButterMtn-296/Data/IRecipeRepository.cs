using System;
using ButterMtn_296.Models;

namespace ButterMtn_296.Data
{
	public interface IRecipeRepository
	{
		IQueryable<Recipe> Recipes { get; }
        IQueryable<Ingredient> Ingredients { get; }
        public Task<int> AddRecipeAsync(Recipe recipe);

        public Task<int> UpdateRecipeAsync(Recipe recipe);

        public Task<int> DeleteRecipeAsync(Recipe recipe);
        
        public Task<Recipe> GetRecipeByIdAsync(int id);

        //non async for pulling recipe and log 
        public int AddLogEntry(LogEntry log);
        public int DeleteLogEntry(LogEntry log);
        public int UpdateLogEntry(LogEntry log);
        public LogEntry GetLogOnly(int id);

        //non async for filter by ingredient
        public Recipe GetRecipeById(int id);
        public Task<List<Recipe>> GetRecipesByUserAsync(AppUser user);

        public List<Recipe>? FilterRecipesByDate(DateTime startdate, DateTime enddate);
        public List<Recipe>? FilterRecipesByKeyword(string word);

        public List<int>? GetRecipesByIngredientStep1(string name);
        public List<string> GetDistinctIngredients();
    }
}

