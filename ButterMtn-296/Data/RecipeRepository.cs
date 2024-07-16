using System;
using ButterMtn_296.Data;
using ButterMtn_296.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;


namespace ButterMtn_296.Data
{
    public class RecipeRepository : IRecipeRepository
    {
        public ApplicationDbContext context;

        //constructor
        public RecipeRepository(ApplicationDbContext c)
        {
            context = c;
        }

        public IQueryable<Recipe> Recipes
        {
            get
            {
                // Get all the Forum objects in the forum DbSet
                // and include the forum user that created the Forum
                return context.Recipes
                    .Include(recipe => recipe.RecipeUser)
                    .Include(recipe => recipe.Ingredients)
                    .Include(recipe => recipe.Logs)
                    .ThenInclude(log => log.LogUser);
            }

        }

        public IQueryable<Ingredient> Ingredients
        {
            get
            {
                return context.Ingredients;
            }

        }

        public async Task<int> AddRecipeAsync(Recipe recipe)
        {

            context.Recipes.Add(recipe);
            int result = await context.SaveChangesAsync();
            return result;

        }

        public async Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            context.Recipes.Remove(recipe);
            int result = await context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateRecipeAsync(Recipe recipe)
        {
            context.Recipes.Update(recipe);
            var result = await context.SaveChangesAsync();
            return result;
        }

        #region logentry

        public int AddLogEntry(LogEntry log)
        {
            Recipe recipe = GetRecipeOnly(log.RecipeID);
            recipe.Logs.Add(log);
            int result = context.SaveChanges();
            return result;

        }

        public int DeleteLogEntry(LogEntry log)
        {
            Recipe recipe = GetRecipeOnly(log.RecipeID);
            recipe.Logs.Remove(log);
            context.Logs.Remove(log);
            int result = context.SaveChanges();
            return result;
        }

        public int UpdateLogEntry(LogEntry newLog)
        {
            //fix
            Recipe recipe = GetRecipeOnly(newLog.RecipeID);
            LogEntry oldLog = context.Logs.Where(l => l.LogEntryID == newLog.LogEntryID).FirstOrDefault();
            recipe.Logs.Remove(oldLog);
            context.SaveChanges();
            context.Logs.Add(newLog);
            int result = context.SaveChanges();
            return result;
        }
        public Recipe GetRecipeOnly(int id)
        {
            Recipe recipe = context.Recipes.Where(r => r.RecipeID == id).FirstOrDefault();
            return recipe;
        }
        public LogEntry GetLogOnly(int id)
        {
            LogEntry log = context.Logs.Where(l => l.LogEntryID == id).FirstOrDefault();
            return log;
        }
        #endregion

        #region filtering
        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await context.Recipes
                .Include(recipe => recipe.RecipeUser)
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Logs)
                .ThenInclude(log => log.LogUser)
                .Where(recipe => recipe.RecipeID == id).FirstOrDefaultAsync();
        }

        public Recipe GetRecipeById(int id)
        {
            return context.Recipes
                .Include(recipe => recipe.RecipeUser)
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Logs)
                .ThenInclude(log => log.LogUser)
                .Where(recipe => recipe.RecipeID == id).FirstOrDefault();
        }


        public List<string> GetDistinctIngredients()
        {
            var query = context.Ingredients
                .AsEnumerable()
                .GroupBy(i => i.Name)
                .ToList();
            List<string> list = new List<string>();
            foreach (IGrouping<string, Ingredient> i in query)
            {
                string name = i.Key;

                if (!list.Contains(name))
                {
                    list.Add(name);
                }
            }
            return list;
            /*
           var blogs = context.Blogs
               .AsEnumerable()
               .Where(blog => StandardizeUrl(blog.Url).Contains("dotnet"))
               .ToList();

           foreach (IGrouping<string, Ingredient> ingredientGroup in query)
           {
               Console.WriteLine("Name: {0}", ingredientGroup.Key);
               foreach (Ingredient i in ingredientGroup)
               {
                   Console.WriteLine("\t" + i.Name);
               }
           }*/
        }
        /*
        public bool IngredientEquals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            else
            {
                Ingredient other = (Ingredient)obj;
                return
                    other.Value == Value &&
                    other.Suit == Suit;
            }
        }
        public override int GetHashCode()
        {
            return 13 + 7 * value.GetHashCode() +
                7 * suit.GetHashCode();
        }
        */
        public List<int>? GetRecipesByIngredientStep1(string name)
        {
            List<int> idList = new List<int>();
            List<Ingredient> ingredients = new List<Ingredient>();

            var allIngredients = context.Ingredients;

            foreach (Ingredient i in allIngredients)
            {
                //see if it contains the search
                if (i.Name == name)
                {
                    //get recipe by id
                    int id = i.RecipeID;
                    idList.Add(id);

                }
            }
            return idList;
        }


        public async Task<List<Recipe>> GetRecipesByUserAsync(AppUser user)
        {
            var allRecipes = context.Recipes;
            List<Recipe> recipes = await allRecipes.Where(r => r.RecipeUser == user).ToListAsync();
            return recipes;
        }


        public List<Recipe>? FilterRecipesByKeyword(string word)
        {
            var allRecipes = context.Recipes;
            List<Recipe> recipes = new List<Recipe>();
            return recipes;
        }

        public List<Recipe>? FilterRecipesByDate(DateTime startdate, DateTime enddate)

            {
            var allRecipes = context.Recipes;
            List<Recipe> recipes =  allRecipes.Where(r => r.RecipeDate >= startdate && r.RecipeDate <= enddate).ToList();
            return recipes;
        }
        }
        #endregion
    }

