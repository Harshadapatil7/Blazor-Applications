using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        public List<Expense> expList = new()
        {
            new Expense
            {
                Id = 1,
                Title = "Groceries",
                Amount = 850.50m,
                Date = DateTime.Today,
                Category = "Food",
                Description = "Weekly groceries"
            },
            new Expense
            {
                Id = 2,
                Title = "Bus Ticket",
                Amount = 120m,
                Date = DateTime.Today.AddDays(-1),
                Category = "Travel",
                Description = "Office commute"
            },
            new Expense
            {
                Id = 3,
                Title = "Electricity Bill",
                Amount = 1500m,
                Date = DateTime.Today.AddDays(-3),
                Category = "Bills",
                Description = "Monthly electricity bill"
            }
        };

        public List<Expense> GetExpenses()
        {
            return expList;
        }

        public Expense? GetExpenseById(int id)
        {
            return expList.FirstOrDefault(e => e.Id == id);
        }

        public List<Expense> SortByAmount(bool descending = false)
        {
            return descending
                ? expList.OrderByDescending(e => e.Amount).ToList()
                : expList.OrderBy(e => e.Amount).ToList();
        }

        public List<Expense> SortByDate(bool descending = true)
        {
            return descending
                ? expList.OrderByDescending(e => e.Date).ToList()
                : expList.OrderBy(e => e.Date).ToList();
        }

        public List<Expense> FilterByCategory(string category)
        {
            return string.IsNullOrWhiteSpace(category)
                ? GetExpenses()
                : expList.Where(e => e.Category.Equals(
                    category.Trim(),
                    StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Filter and sort expenses
        public List<Expense> GetFilteredExpenses(
            string category, string sortOption)
        {
            IEnumerable<Expense> query = string.IsNullOrWhiteSpace(category)
                ? GetExpenses()
                : FilterByCategory(category);

            query = sortOption switch
            {
                "amount-asc" => query.OrderBy(e => e.Amount),
                "amount-desc" => query.OrderByDescending(e => e.Amount),
                "date-asc" => query.OrderBy(e => e.Date),
                _ => query.OrderByDescending(e => e.Date)
            };

            return query.ToList();
        }

        public List<string> GetCategories()
        {
            return expList
                .Select(e => e.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c)
                .ToList();
        }

        public void AddExpense(Expense exp)
        {
            exp.Id = expList.Count == 0
                ? 1
                : expList.Max(e => e.Id) + 1;

            expList.Add(exp);
        }

        public void UpdateExpense(Expense exp)
        {
            int index = expList.FindIndex(e => e.Id == exp.Id);

            if (index != -1)
                expList[index] = exp;
        }

        public void DeleteExpense(int id)
        {
            var expense = GetExpenseById(id);

            if (expense != null)
                expList.Remove(expense);
        }
    }
}