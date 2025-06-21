using BudgetApp.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.DataAccess;

public static class DbInit
{
    public static void Initialize(BudgetContext context)
    {
        context.Database.Migrate();
    }
}