using Microsoft.EntityFrameworkCore;
using BudgetApp.Models;

namespace BudgetApp.DataAccess
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}