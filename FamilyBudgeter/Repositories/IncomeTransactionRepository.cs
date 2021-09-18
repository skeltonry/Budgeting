namespace FamilyBudgeterWPF
{
	public class IncomeTransactionRepository : Repository<IncomeTransaction>, IIncomeTransactionRepository
	{
		public IncomeTransactionRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}