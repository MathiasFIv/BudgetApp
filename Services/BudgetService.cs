
using BudgetApp.DataAccess;
using BudgetApp.Models;

namespace BudgetApp.Services;

    public class BudgetService
    {
        private readonly BudgetContext _context = new();
        

        public void AddTransaction(string description, decimal amount, string category)
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
            _context.SaveChanges();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.OrderByDescending(t => t.Date).ToList();
        }

        public decimal GetCurrentBalance() => _context.Transactions.Sum(t => t.Amount);


        public void UpdateTransaction(int id, string newDescription, decimal newAmount, string newCategory)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction != null)
            {
                transaction.Description = newDescription;
                transaction.Amount = newAmount;
                transaction.Category = newCategory;
                transaction.Date = DateTime.Now;

                _context.SaveChanges();
                Console.WriteLine(
                    $"Updated {transaction.Description} to {transaction.Amount} to {transaction.Category}");
            }
            else
            {
                Console.WriteLine("Transaction not found");
            }
        }


        public void DeleteTransaction(int id)
        {
            
            var transaction = _context.Transactions.Find(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
                Console.WriteLine($"Deleted transaction with ID {id}");
            }
            else
            {
                Console.WriteLine("Transaction not found");
            }
        }
        
        public List<Transaction> FilterByCategory(string category)
        {
            return _context.Transactions
                .Where(t => t.Category.ToLower() == category.ToLower())
                .OrderByDescending(t => t.Date)
                .ToList();
        }
    }
