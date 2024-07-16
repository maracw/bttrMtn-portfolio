using System;
using ButterMtn_296.Data;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Components.Routing;
using Xunit.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSupport.EfHelpers;
using Microsoft.EntityFrameworkCore;

namespace ButterMtn_266Tests
{

    internal class SetupTestRepo
    {

        // Create a DbContext for the SQLite in-memory database
        public static ApplicationDbContext CreateContext()
        {
            var options = SqliteInMemory.CreateOptions<ApplicationDbContext>();
            return new ApplicationDbContext(options);
        }

        public static RecipeRepository CreateRepo(ApplicationDbContext context)
        {
            // Create a ReviewRepository instance using a SQLite in-memory db
            context.Database.EnsureCreated();
            return new RecipeRepository(context);
        }

        public static Recipe CreateRecipe()
        {
            AppUser figgy = new AppUser();
            AppUser mara = new AppUser();

            Recipe recipe = new Recipe
            {
                Title = "Almond Bee Sting Cake Topping",
                RecipeDate = DateTime.Parse("11/25/21"),
                Difficulty = 3,
                Category = "Cake,Fancy",
                RecipeUser = mara,
                ImageURL = "/images/beesting1.jpg",
                Instructions = "Melt the butter in a sturdy saucepan;\n" +
                    "Add the sugar, honey, cream and salt;\nCook on medium heat for 3-5 minutes. " +
                    "It should get bubbly and foamy;\nAdd the almonds and stir;\nEither let cool in the pan slightly, " +
                    "or dump the mix onto a cookie sheet lined with parchment paper. Use a spatula to flatten it " +
                    "into a rough circle shape (that’s about the same size as your cake pan);\n"
            };

            List<Ingredient> ingredients = new List<Ingredient>
                {
                    new Ingredient {Amount=3, Units="tbsp", Name="sugar"},
                    new Ingredient {Amount=3, Units="tbsp", Name="honey"},
                    new Ingredient {Amount=2, Units="tbsp", Name="cream"},
                    new Ingredient {Amount=4, Units="tbsp", Name="butter"},
                    new Ingredient {Amount=1, Units="cup", Name="sliced almonds"},
                    new Ingredient {Amount=0, Units="pinch", Name="salt"}
                };

            //add ingredients to recipe
            foreach (Ingredient i in ingredients)
            {
                recipe.Ingredients.Add(i);
            }

            //make a logEntry

            LogEntry log = new LogEntry
            {
                Headline = "Lemon Version",
                Description = "I added the zest of one lemon right after taking it off the stove. Absolutely delicious!",
                Rating = 5,
                LogUser = figgy,
                LogDate = DateTime.Parse("12/1/21")
            };
            //add logentry to recipe

            recipe.Logs.Add(log);

            return recipe;
        }

        public static Recipe CreateRecipe2()
        {
            AppUser figgy = new AppUser();
            AppUser mara = new AppUser();
            Recipe recipe2 = new Recipe
            {
                Title = "Almond Bee Sting Cake",
                RecipeDate = DateTime.Parse("11/25/21"),
                Difficulty = 2,
                RecipeUser = mara,
                ImageURL = "/images/beesting1.jpg",
                Instructions = "Grease a springform pan and line the bottom with a circle of parchment paper;" +
                "\nPreheat the oven to 350;\nMake the topping and let cool while you make the cake batter;" +
                "\nIn a big bowl, combine the dry ingredients -flour, almond flour, leavening, and salt;\n" +
                "In another bowl, whisk the wet ingredients together - eggs, sugar, yogurt, milk, melted butter, " +
                "zest, and extract. It will be pretty thick;\nAdd the wet mix to the bowl with the dry ingredients. " +
                "Mix until it’s smooth. It will be fairly stiff;\nDump the mix into the prepped pan, top with the almond topping. " +
                "If you spread it into a circle, carefully peel the gooey topping off and set it on top of the cake. " +
                "If you let it cool in the pan, take a big spoonful and flatten it slightly (I use my hands) and then lay " +
                "it on top of the cake. Repeat with other big flat spoonfuls until you have used up the topping;" +
                "\nPut the pan on a cookie sheet to catch any caramel drips, bake for 35 minutes;" +
                "\nWhen you take it out, run a butter knife around the edge to loosen the cake. " +
                "Then, let it cool for 10 min in the pan. Then, unmold the cake and cool it on a rack;"
            };

            List<Ingredient> ingredients2 = new List<Ingredient>
                {
                    new Ingredient {Amount=.66, Units="cup", Name="sugar"},
                    new Ingredient {Amount=.25, Units="cup", Name="melted butter"},
                    new Ingredient {Amount=.33, Units="cup", Name="plain yogurt"},
                    new Ingredient {Amount=2, Units="whole", Name="eggs"},
                    new Ingredient {Amount=1, Units="tbsp", Name="lemon or orange zest (one piece of fruit zested will be enough"},
                    new Ingredient {Amount=.5, Units="tsp", Name="vanilla extract" },
                    new Ingredient {Amount=1.66, Units="cup", Name="almond flour" },
                    new Ingredient {Amount=.5, Units="cup", Name="AP flour or Gluten Free AP flour"},
                    new Ingredient {Amount=.5, Units="tsp", Name="salt"},
                    new Ingredient {Amount=2, Units="tsp", Name="baking powder"},
                    new Ingredient {Amount=.25, Units="tsp", Name="baking soda"},
                    new Ingredient {Amount=1, Units="batch", Name="Bess Sting Cake Topping"},
                };

            //add ingredients to recipe
            foreach (Ingredient i in ingredients2)
            {
                recipe2.Ingredients.Add(i);
            }

            //make a logEntry

            LogEntry log2 = new LogEntry
            {
                Headline = "No flour - all almond flour",
                Description = "I wanted to try a GF version, but I didn't have any GF flour on hand. " +
                "I replaced the 1/2 cup of flour with more almond flour. It was okay, a bit crumbly. Not good enought to repeat",
                Rating = 2,
                LogUser = mara,
                LogDate = DateTime.Parse("12/30/21")
            };
            //add logentry to recipe

            recipe2.Logs.Add(log2);
            return recipe2;
        }
    }
}

