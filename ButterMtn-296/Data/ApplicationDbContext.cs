using System;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ButterMtn_296.Data
{
	public class ApplicationDbContext :IdentityDbContext
	{
        public ApplicationDbContext(

          DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient>Ingredients { get; set; }
        public DbSet<LogEntry> Logs { get; set; }
    }
}

