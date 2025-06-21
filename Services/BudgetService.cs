
using BudgetApp.DataAccess;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Services;

    public class BudgetService
    {
        private readonly BudgetContext _context;

        public BudgetService(BudgetContext context)
        {
            _context = context;
        }
        

        public async Task AddTransactionAsync(string description, decimal amount, string category)
        {
            if (string.IsNullOrWhiteSpace(description)) 
                description = "No Description";

            if (string.IsNullOrWhiteSpace(category)) 
                category = "Uncategorized";

            var transaction = new Transaction
            {
                Description = description,
                Amount = amount,
                Category = category
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.OrderBy(t => t.Date).ToListAsync();
        }

        public async Task<decimal> GetCurrentBalanceAsync()
        {
            return await _context.Transactions.SumAsync(t=> t.Amount);
        }
        
        public async Task  UpdateTransactionAsync(int id, string newDescription, decimal newAmount, string newCategory)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                transaction.Description = newDescription;
                transaction.Amount = newAmount;
                transaction.Category = newCategory;
                transaction.Date = DateTime.Now;

               await _context.SaveChangesAsync();
               Console.WriteLine("Transaction updated successfully!");
            }
            else
            {
                Console.WriteLine("Transaction not found");
            }
        }


        public async Task DeleteTransactionAsync(int id)
        {
            
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
               await  _context.SaveChangesAsync();
                Console.WriteLine($"Deleted transaction with ID {id}");
            }
            else
            {
                Console.WriteLine("Transaction not found");
            }
        }
        
        public async Task<List<Transaction>> FilterByCategory(string category)
        {
            return await _context.Transactions
                .Where(t => t.Category.ToLower() == category.ToLower())
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }
