using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ButterMtn_296.Data;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ButterMountain2.Controllers
{

    //[Authorize]
    //[AllowAnonymous]
    [Authorize(Roles = "admin")]
    //[Area("Admin")]

    public class UserController : Controller
    {
        UserManager<AppUser> userManager;
        RoleManager<IdentityRole> roleManager;
        private IRecipeRepository _recipeRepository;
        public UserController(UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr, IRecipeRepository recipeRepository)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            _recipeRepository=recipeRepository;
        }

        public ViewResult  Index()
        {
            return View();
        }
        public async Task<IActionResult> ManageUsers()
        {
            List<AppUser> users = new List<AppUser>();

            users = userManager.Users.ToList();
          
            foreach (AppUser u in users)
            {
                //u.RoleNames = userManager.GetRolesAsync(u).Result;
                u.RoleNames = await userManager.GetRolesAsync(u);
            }

            UserVM model = new UserVM
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser orphan = await userManager.FindByNameAsync("OrphanAccount");
            AppUser user = await userManager.FindByIdAsync(id); if (user != null)
            {
                List<Recipe> orphanRecipes = new List<Recipe>();
                try
                {
                    var allRecipes = _recipeRepository.Recipes;
                    orphanRecipes = await allRecipes.Where(r => r.RecipeUser == user).ToListAsync();
                   
                    foreach (Recipe recipe in orphanRecipes)
                    {
                        recipe.RecipeUser = orphan;
                    }
                }
                catch
                {
                    throw;

                }
                //find all the user's recipes and and replace the user with Orphan account
               
                IdentityResult result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                { //if failed
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }
        // the Add() methods work like the Register() methods from 16-11 and 16-12
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role); return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManageRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = await _recipeRepository.Recipes.ToListAsync();
            return View(recipes);
        }


        public async Task<IActionResult> ManageLogEntries()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = await _recipeRepository.Recipes.ToListAsync();
            return View(recipes);
        }
    }
}

