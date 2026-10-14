using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        public List<Expense> expList = new List<Expense>(){
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
            refreshIds();
            return expList;
        }

        public void addExpense(Expense exp)
        {
            expList.Add(exp);
        }

        private void refreshIds()
        {
            int index = 1;
            foreach(Expense exp in expList)
            {
                exp.Id = index++;
            }
        }

        public void updateExpense(Expense exp)
        {
            var index = expList.FindIndex(e => e.Id == exp.Id);

            if (index != -1)
            {
                expList[index] = exp;
            }
        }

        public void deleteExpense(Expense exp)
        {
            expList.Remove(exp);
        }
    }
}
