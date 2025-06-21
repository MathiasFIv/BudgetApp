using System;
using BudgetApp.Services;

namespace BudgetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var budgetService = new BudgetService();

            while (true)
            {
                // Show menu options
                Console.WriteLine("=== Budget Tracker ===");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Transactions");
                Console.WriteLine("3. Show Balance");
                Console.WriteLine("4. Update Transaction");
                Console.WriteLine("5. Delete Transaction");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid Input!");
                    continue;
                }

                // Decide what to do based on input
                switch (input)
                {
                    case "1":
                        AddTransaction(budgetService);
                        break;
                    case "2":
                        ViewTransactions(budgetService);
                        break;
                    case "3":
                        ShowBalance(budgetService);
                        break;
                    case "4":
                        UpdateTransaction(budgetService);
                        break;
                    case "5":
                        DeleteTransaction(budgetService);
                        break;
                    case "6":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }

                Console.WriteLine(); // Blank line for spacing
            }
        }

        static void AddTransaction(BudgetService service)
        {
            Console.Write("Enter Description: ");
            string? descriptionInput = Console.ReadLine();
            string description = string.IsNullOrEmpty(descriptionInput) ? "No Description" : descriptionInput;

            Console.Write("Amount: ");
            string? amountInput = Console.ReadLine();
            if (!decimal.TryParse(amountInput, out decimal amount))
            {
                Console.WriteLine("Invalid amount. Transaction cancelled.");
                return;
            }

            Console.Write("Category: ");
            string? categoryInput = Console.ReadLine();
            string category = string.IsNullOrEmpty(categoryInput) ? "Uncategorized" : categoryInput;

            service.AddTransaction(description, amount, category);
            Console.WriteLine("Transaction added successfully.");
        }

        static void ViewTransactions(BudgetService service)
        {
            var transactions = service.GetAllTransactions();

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions recorded yet...");
                return;
            }

            Console.WriteLine("Transactions:");
            foreach (var t in transactions)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Date:yyyy-MM-dd HH:mm} | {t.Description} | {t.Category} | {t.Amount:C}");
            }
        }

        static void ShowBalance(BudgetService service)
        {
            decimal balance = service.GetCurrentBalance();
            Console.WriteLine($"Current Balance: {balance:C}");
        }

        static void UpdateTransaction(BudgetService service)
        {
            Console.Write("Enter the Transaction ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID - Transaction not found!");
                return;
            }

            Console.Write("New Description: ");
            string? newDescription = Console.ReadLine();

            Console.Write("New Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal newAmount))
            {
                Console.WriteLine("Invalid amount. Transaction cancelled.");
                return;
            }

            Console.Write("New Category: ");
            string? newCategory = Console.ReadLine();

            service.UpdateTransaction(id, newDescription, newAmount, newCategory);
        }

        static void DeleteTransaction(BudgetService service)
        {
            Console.Write("Enter the Transaction ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID - Transaction not found!");
                return;
            }

            service.DeleteTransaction(id);
        }
    }
}