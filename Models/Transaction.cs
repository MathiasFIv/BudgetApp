namespace BudgetApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Category { get; set; } = string.Empty;
    
    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd} | {Category,-10} | {Description,-20} | {Amount,8:C}";
    }
}