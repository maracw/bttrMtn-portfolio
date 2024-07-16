using System;
using ButterMtn_296.Controllers;
using ButterMtn_296.Data;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ButterMtn_266Tests
{
	public class RecipeControllerTests
    {
        private readonly ApplicationDbContext context;
        private readonly RecipeController controller;
        private readonly RecipeRepository repo;
        private readonly Recipe recipe1;
        private readonly Recipe recipe2;
        
    public RecipeControllerTests()
    {
        context = SetupTestRepo.CreateContext();
        repo = SetupTestRepo.CreateRepo(context);
        recipe2 = SetupTestRepo.CreateRecipe2();
        controller = new RecipeController(repo, null, null);
    }
        //test if RecipeIndex controller method works
        [Fact]
        public void RecipeIndexTest()
        {
            // arrange
            //add two recipes
            Recipe recipe1 = SetupTestRepo.CreateRecipe();
            context.Add(recipe1);
            Recipe recipe2 = recipe2 = SetupTestRepo.CreateRecipe2();
            context.Add(recipe2);
            context.SaveChanges();
         
            // act
            var result= controller.Index().Result as ViewResult;

            // assert
            var viewrecipes = result.Model as List<Recipe>;
           
            Assert.NotNull(viewrecipes);

        }

        //test if RecipeDetails controller method works
        [Fact]
        public void RecipeDetailsTest()
        {
            // arrange
            //add two recipes
            Recipe recipe1 = SetupTestRepo.CreateRecipe();
            context.Add(recipe1);
            Recipe recipe2 = recipe2 = SetupTestRepo.CreateRecipe2();
            context.Add(recipe2);
            context.SaveChanges();

            // act
            var result = controller.Details(1).Result as ViewResult;

            // assert
            var viewrecipes = result.Model as Recipe;

            Assert.NotNull(viewrecipes);

        }


        [Fact]
        public void RecipeFilterTest()
        {
            // arrange
            //add two recipes
            Recipe recipe1 = SetupTestRepo.CreateRecipe();
            context.Add(recipe1);
            Recipe recipe2 = recipe2 = SetupTestRepo.CreateRecipe2();
            context.Add(recipe2);
            context.SaveChanges();
            string name = "sugar";
            string name2 = "eggs";
            // act
        
            //var filtereView = controller.FilterRecipes(name).ExecuteResultAsync as ViewResult; 
            //List<Recipe> filteredRecipes = filtereView.Model as List<Recipe>;

            //assert
            //Assert.NotNull(filteredRecipes);


        }

        //test if AddRecipe controller method works
        //needs user manager
        //to do find way to add usermanager
        /*
        [Fact]
        public void AddRecipeTest()
        {
            // arrange
            Recipe recipe1 = SetupTestRepo.CreateRecipe();
            RecipeVM vm = new RecipeVM();
            vm.Recipe = recipe1;
            vm.Ingredients = repo.Ingredients.ToList();
            // act
            var viewResult = controller.AddRecipe(vm).Result as ViewResult;

            // assert
        }*/

        /*Delete and Edit need user manager to work*/
    }
}

