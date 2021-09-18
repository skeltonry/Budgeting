namespace FamilyBudgeterWPF
{
	public class RecurringExpenseRepository : Repository<RecurringExpense>, IRecurringExpenseRepository
	{
		public RecurringExpenseRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}