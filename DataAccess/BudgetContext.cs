using Microsoft.EntityFrameworkCore;
using BudgetApp.Models;
using System;
using System.IO;

namespace BudgetApp.DataAccess
{
    public class BudgetContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "budget.db");
            Console.WriteLine($"Using database at: {dbPath}");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}