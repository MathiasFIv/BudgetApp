using System.Text.Json;
using BudgetApp.Models;

namespace BudgetApp.Services
{
    public class BudgetService
    {
        private readonly List<Transaction> _transactions = new();

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

            _transactions.Add(transaction);
        }

        public List<Transaction> GetAllTransactions() => _transactions;

        public decimal GetCurrentBalance() => _transactions.Sum(t => t.Amount);

        public void SaveToFile(string filepath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_transactions, options);
            File.WriteAllText(filepath, jsonString);
        }

        public void LoadFromFile(string filepath)
        {
            if (!File.Exists(filepath))
                return;

            string jsonString = File.ReadAllText(filepath);
            var loadedTransactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
            if (loadedTransactions != null)
                
                _transactions.Clear();
                _transactions.AddRange(loadedTransactions);
        }
    }
}