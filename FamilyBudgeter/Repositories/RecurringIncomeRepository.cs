namespace FamilyBudgeterWPF
{
	public class RecurringIncomeRepository : Repository<RecurringIncome>, IRecurringIncomeRepository
	{
		public RecurringIncomeRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}