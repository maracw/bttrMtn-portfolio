using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ButterMtn_296.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ButterMountain2.Data;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ButterMtn_296.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository _recipeRepository;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private readonly ApplicationDbContext context;

        public RecipeController(IRecipeRepository recipeRepository, UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
        {
            _recipeRepository = recipeRepository;
            userManager = userMngr;
            signInManager = signInMngr;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = await _recipeRepository.Recipes.ToListAsync();
            return View(recipes);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Recipe recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            return View(recipe);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Details(int id, double ratio)
        {
            Recipe recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            foreach (Ingredient i in recipe.Ingredients)
            {
                i.Amount = i.Amount * ratio;
            }
            return View(recipe);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FilterRecipes()
        {
            FilterVM vm = new FilterVM();
            vm.Recipes = await _recipeRepository.Recipes.ToListAsync();
            //vm.Ingredients = await _recipeRepository.Ingredients.ToListAsync();
            vm.IngredientNames=_recipeRepository.GetDistinctIngredients();
            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult FilterRecipes(string ingredientName)
        {
            FilterVM vm = new FilterVM();
            vm.IngredientNames = _recipeRepository.GetDistinctIngredients();

            List<int> listForPart2=new List<int>();
            listForPart2= _recipeRepository.GetRecipesByIngredientStep1(ingredientName);

            List<Recipe> repicesFromInt = new List<Recipe>();

            foreach(int i in listForPart2)
            {
                Recipe r = _recipeRepository.GetRecipeById(i);
                //adding it directly to vm is bad
                repicesFromInt.Add(r);
            }
            vm.Recipes = repicesFromInt;
            return View(vm);
        }

        public IActionResult FilterRecipesKeyWord(string start, string end, string search)
        {
            FilterVM vm = new FilterVM();
            vm.Recipes = _recipeRepository.Recipes.ToList();
            //vm.Ingredients = await _recipeRepository.Ingredients.ToListAsync();
            vm.IngredientNames = _recipeRepository.GetDistinctIngredients();
            var startdate = new DateTime();
            var enddate = new DateTime();
            
            //do dates first
            if (!String.IsNullOrEmpty(start) || !String.IsNullOrEmpty(end))
            {
                if (!String.IsNullOrEmpty(start) && !String.IsNullOrEmpty(end))
                {
                    startdate = DateTime.Parse(start).Date;
                    enddate = DateTime.Parse(end).Date;
                    vm.Recipes = _recipeRepository.FilterRecipesByDate(startdate, enddate);
                }
                else if (!String.IsNullOrEmpty(start))
                {
                    startdate = DateTime.Parse(start);
                    enddate = DateTime.Now;
                    vm.Recipes = _recipeRepository.FilterRecipesByDate(startdate, enddate);

                }
                else
                {
                    startdate = DateTime.Parse("12/31/00");
                    enddate = DateTime.Parse(end);
                    vm.Recipes = _recipeRepository.FilterRecipesByDate(startdate, enddate);

                }
            }
            else if(!String.IsNullOrEmpty(search))
            {
                vm.Recipes = _recipeRepository.FilterRecipesByKeyword(search);
            }
            else
            { }
            
            return View(vm);
        }

        #region AddEditDeleteRecipe
        [Authorize]
        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }
        //user must be logged in to create a new recipe
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeVM vm)
        {
            Recipe recipe = vm.Recipe;
            AppUser user = await userManager.FindByNameAsync(vm.vmUserName);
            recipe.RecipeUser = user;

            foreach (Ingredient i in vm.Ingredients)
            {
                if (i.Name != null)
                {

                    recipe.Ingredients.Add(i);
                }
            }
            try
            {
                int result = await _recipeRepository.AddRecipeAsync(recipe);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
       
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditRecipe(int id)
        {
            Recipe recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            //find current logged in user
            string currentUserName = User.Identity.Name;
            AppUser currentUser = await userManager.FindByNameAsync(currentUserName);
            //if recipe creator is logged in OR the user is an admin
            if (recipe.RecipeUser == currentUser || User.IsInRole("admin"))
            {
                return View(recipe);
            }
            //if loggedin user is not the recipe creator
            else
            {
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditRecipe(Recipe recipe)
        {
            /*todo*/
            return View(recipe);
        }
        //only admins can delete recipes
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            Recipe recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            return View(recipe);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int RecipeID)
        {
            Recipe recipe = await _recipeRepository.GetRecipeByIdAsync(RecipeID);
            int result = await _recipeRepository.DeleteRecipeAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Logs
        //user must be logged in to make a log
        //get user identity and add to log first
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddLog(int id)
        {
            LogEntry log = new LogEntry();
            log.RecipeID = id;
            string currentUserName = User.Identity.Name;
            AppUser currentUser = await userManager.FindByNameAsync(currentUserName);
            log.LogUser = currentUser;
            return View(log);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddLog(LogEntry log)
        {
            log.LogDate = DateTime.Now;
            int result = _recipeRepository.AddLogEntry(log);
            //method updates log and recipe so succes = 2 rows affected

            if (result == 2)
                return RedirectToAction("index");
            else
                return RedirectToAction("RecipeError");
        }


        //only admins can edit and delete logs
        /*currently this adds a new log
         consider making the views use a list of logs
        so I can keep the old log in position 0, and the new log in position 1
        then I can remove the list[0] log and add the list [1] log*/
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditLog(LogEntry log)
        {
            
            int result = _recipeRepository.UpdateLogEntry(log);
            if (result >0)
                return RedirectToAction("ManageLogEntries", "User");
            else
                return RedirectToAction("RecipeError");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditLog(int id)
        {
            LogEntry log = _recipeRepository.GetLogOnly(id);
            return View(log);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteLog(int id)
        {
            LogEntry log = _recipeRepository.GetLogOnly(id);
            return View(log);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteLog(LogEntry log)
        {
            int result = _recipeRepository.DeleteLogEntry(log);
            if (result > 0)
                return RedirectToAction("ManageLogEntries", "User");
            else
                return RedirectToAction("RecipeError");
        }
        #endregion
        public ViewResult RecipeError()
        {
            return View();
        }
    }
}

