namespace FamilyBudgeterWPF
{
	public class AccountRepository : Repository<Account>, IAccountRepository
	{
		public AccountRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}