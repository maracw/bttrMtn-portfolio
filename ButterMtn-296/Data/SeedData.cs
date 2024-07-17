using System;
using System.Diagnostics.Metrics;
using System.IO;
using ButterMountain2.Data;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Org.BouncyCastle.Asn1.Utilities;

namespace ButterMtn_296.Data
{
    public class SeedData
    {

        public static void Seed(ApplicationDbContext context, IServiceProvider provider)
        {
            if (!context.Recipes.Any())
            {
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                AppUser mara = userManager.FindByNameAsync("Mara").Result;
                AppUser figgy = userManager.FindByNameAsync("Figgy").Result;
                #region recipe1
                Recipe recipe = new Recipe
                {
                    Title = "Almond Bee Sting Cake Topping",
                    RecipeDate = DateTime.Parse("11/25/21"),
                    Difficulty = 3,
                    Category = "Cake,Fancy",
                    RecipeUser = mara,
                    ImageURL = "/images/beesting-cake.jpg",
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

                //add the recipe to the context
                context.Recipes.Add(recipe);
                //save changes
                context.SaveChanges();
                #endregion
                #region Recipe2
                Recipe recipe2 = new Recipe
                {
                    Title = "Almond Bee Sting Cake",
                    RecipeDate = DateTime.Parse("11/25/21"),
                    Difficulty = 2,
                    RecipeUser = mara,
                    ImageURL = "/images/beesting-cake.jpg",
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
                    new Ingredient {Amount=1, Units="batch", Name="Bee Sting Cake Topping"},
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

                //add the recipe to the context
                context.Recipes.Add(recipe2);
                //save changes
                context.SaveChanges();
                #endregion

                #region Recipe3
                Recipe recipe3 = new Recipe
                {
                    Title = "Gingerbread Orange Loaf",
                    RecipeDate = DateTime.Parse("12/25/22"),
                    Difficulty = 2,
                    RecipeUser = mara,
                    ImageURL = "/images/beesting1.jpg",
                    Instructions = "Grease nad flour two medium sized loaf pans;" +
                    "\nPreheat the oven to 350;\n" +
                    "\nIn a big bowl, combine the all the dry ingredients (all the spices too);\n" +
                    "In another bowl, whisk the wet ingredients together - eggs, sugar, molasses, beer, oil, zest;" +
                    "\nAdd the wet mix to the bowl with the dry ingredients. " +
                    "Mix until it’s smooth. Then add the chopped ginger and orange bits;" +
                    "\nDump the mix into the prepped pans and bake for 55min;"
                };

                List<Ingredient> ingredients3 = new List<Ingredient>
                {
                    new Ingredient {Amount=.66, Units="cup", Name="molasses"},
                    new Ingredient {Amount=1.5, Units="cup", Name="sugar"},
                    new Ingredient {Amount=.33, Units="cup", Name="bread flour"},
                    new Ingredient {Amount=2, Units="cup", Name="AP flour"},
                    new Ingredient {Amount=1, Units="tbsp", Name="orange zest (one piece of fruit zested will be enough"},
                    new Ingredient {Amount=.25, Units="tsp", Name="ground cloves"},
                     new Ingredient {Amount=.25, Units="tsp", Name="ground allspice"},
                      new Ingredient {Amount=1.5, Units="tsp", Name="ground cinnamon"},
                       new Ingredient {Amount=.5, Units="tsp", Name="ground cardamom"},
                        new Ingredient {Amount=.5, Units="whole", Name="nutmeg"},
                         new Ingredient {Amount=2, Units="tbsp", Name="ground ginger"},
                    new Ingredient {Amount=.5, Units="tsp", Name="salt"},
                    new Ingredient {Amount= 1, Units="tsp", Name="baking powder"},
                    new Ingredient {Amount=.5, Units="tsp", Name="baking soda"},
                    new Ingredient {Amount=2, Units="whole", Name="eggs"},
                    new Ingredient {Amount=.75, Units="cup", Name="vegetable oil"},
                    new Ingredient {Amount=1, Units="cup", Name="dark beer (old from the fridge)"},
                    new Ingredient {Amount=.75, Units="cup", Name="chopped candied ginger"},
                     new Ingredient {Amount=.5, Units="cup", Name="chopped candied manadarin slices from Trader Joes"},
                };

                //add ingredients to recipe
                foreach (Ingredient i in ingredients3)
                {
                    recipe3.Ingredients.Add(i);
                }

                //add the recipe to the context
                context.Recipes.Add(recipe3);
                //save changes
                context.SaveChanges();
                #endregion

                #region recipe4
                Recipe recipe4 = new Recipe
                {

                    Title = "Crime bread",
                    RecipeDate = DateTime.Parse("9/25/20"),
                    Difficulty = 1,
                    RecipeUser = mara,
                    ImageURL = "/images/crime-bread.jpg",
                    Instructions = "Preheat oven to 400° F;" +
                    "In a big bowl, combine flour,baking soda,and salt.Add the shredded cheese and mix with your hands;" +
                    "In a different bowl, mix the wet ingredients.Whisk together egg, melted butter, Lao Gan Ma, and the buttermilk. It should be reddish orange and thick!;" +
                    "Dump the wet ingredients into the dry ingredients. Mix with your hands. It needs to be sticky and wet.Add more buttermilk if you need to;" +
                    "Pat the sticky dough into a rough ball and place in a cast iron pan;Bake for 45 - 55 minutes"
                };

                List<Ingredient> ingredients4 = new List<Ingredient>
                {  new Ingredient {Amount=6, Units="oz", Name="shredded cheese"},
                new Ingredient {Amount=4, Units="cup", Name="AP Flour"},
                new Ingredient {Amount=.25, Units="cup", Name="melted butter"},
                new Ingredient {Amount=.5, Units="tsp", Name="salt"},
                    new Ingredient {Amount= 1, Units="tsp", Name="baking powder"},
                    new Ingredient {Amount=.5, Units="tsp", Name="baking soda"},
                    new Ingredient {Amount=1, Units="whole", Name="egg"},
                    new Ingredient {Amount= 1, Units="tsp", Name="baking powder"},
                    new Ingredient {Amount=1.5, Units="cup", Name="buttermilk"},
                    new Ingredient {Amount=.33, Units="cup", Name="Lao Gan Ma Spicy Chilli Crisp"},
                };

                foreach (Ingredient i in ingredients4)
                {
                    recipe4.Ingredients.Add(i);
                }

                context.Recipes.Add(recipe4);
                //save changes
                context.SaveChanges();

                #endregion
            }
        }
    }
}

