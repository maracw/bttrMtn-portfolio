using System;
using ButterMtn_296;
using ButterMtn_296.Data;
using ButterMtn_296.Controllers;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace ButterMtn_266Tests
{
	public class RecipeRepoTests
	{
        private readonly ApplicationDbContext context;
        private readonly RecipeController controller;
        private readonly RecipeRepository repo;
        private readonly Recipe recipe1;
        private readonly Recipe recipe2;

        public RecipeRepoTests()
        {
            context = SetupTestRepo.CreateContext();
            repo = SetupTestRepo.CreateRepo(context);
            recipe1 = SetupTestRepo.CreateRecipe();
            recipe2 = SetupTestRepo.CreateRecipe2();
        }
        [Fact]
        // Verify that a book object can be stored in the database
        public void AddRecipeTest()
        {
            // Arrange: done in constructor
            //Act
            int result = repo.AddRecipeAsync(recipe1).Result;
            // Assert
            Assert.True(result > 0);
            Assert.Equal(1, context.Recipes.Count());
        }

        [Fact]
        // Verify that a Book and all the related data are loaded
        public void IQueryableRecipeTest()
        {
            // Arrange: store an object and it's related data in the database
            context.Recipes.Add(recipe1);
            context.SaveChanges();

            // Act: get the IQueryable object
            var testRecipe = repo.Recipes.First();

            // Assert: check that the IQueryable object and all it's related data are there
            Assert.NotNull(testRecipe);
            Assert.NotNull(testRecipe.RecipeUser);
            Assert.NotNull(testRecipe.Ingredients);
            Assert.NotNull(testRecipe.Logs);
            Assert.NotNull(testRecipe.Logs.First().LogUser);
        }

        [Fact]
        public void IQueryableIngredientsTest()
        {
            //arrrange
            context.Recipes.Add(recipe1);
            context.SaveChanges();
            //act
            int count = context.Ingredients.Count();
            //assert
            Assert.NotNull(context.Ingredients);
            Assert.Equal(6, count);
        }
        /*
        [Fact]
        public async void DeleteRecipeFromRepo()
        {   //arrange
            context.Recipes.Add(recipe1);
            context.Recipes.Add(recipe2);
            context.SaveChanges();
            //act - test if recipe is in repo
            int count1 = repo.Recipes.Count();
            

            //act - delete recipe and repeat count
            context.Recipes.Remove(recipe1);
            context.SaveChanges();
            int count2 = repo.Recipes.Count();

            //check the counts before and after delte are different
            Assert.NotEqual(count1, count2);
        }*/
    }
}

