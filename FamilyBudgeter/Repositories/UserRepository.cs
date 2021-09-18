namespace FamilyBudgeterWPF
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}