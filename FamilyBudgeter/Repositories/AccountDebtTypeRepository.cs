namespace FamilyBudgeterWPF
{
	public class AccountDebtTypeRepository : Repository<AccountDebtType>, IAccountDebtTypeRepository
	{
		public AccountDebtTypeRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}
