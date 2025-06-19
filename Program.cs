using System;
using System.Reflection.Metadata.Ecma335;
using BudgetApp.Services;

namespace BudgetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var budgetService = new BudgetService();
            string filePath = "transactions.json";
            
            budgetService.LoadFromFile(filePath);

            while (true)
            {
                // Show menu options
                Console.WriteLine("=== Budget Tracker ===");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Transactions");
                Console.WriteLine("3. Show Balance");
                Console.WriteLine("4. Exit");
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
                        budgetService.SaveToFile(filePath);
                        Console.WriteLine("Goodbye!");
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }

                Console.WriteLine(); // Blank line for spacing between actions
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
                Console.WriteLine("Invalid amount. Transaction Cancelled.");
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
            
            Console.WriteLine("Transactions");
            foreach (var t in transactions)
            {
                Console.WriteLine(t);
            }
        }

        static void ShowBalance(BudgetService service)
        {
            decimal balance = service.GetCurrentBalance();
            Console.WriteLine($"Current Balance: {balance:C}");
        }
    }
}