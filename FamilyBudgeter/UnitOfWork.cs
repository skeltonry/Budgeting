namespace FamilyBudgeterWPF
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly FamilyBudgeterContext context;

		public UnitOfWork(FamilyBudgeterContext context)
		{
			this.context = context;
			Accounts = new AccountRepository(this.context);
			ExpenseTransactions = new ExpenseTransactionRepository(this.context);
			IncomeTransactions = new IncomeTransactionRepository(this.context);
			RecurringExpenses = new RecurringExpenseRepository(this.context);
			RecurringIncomes = new RecurringIncomeRepository(this.context);
			AccountDebtTypes = new AccountDebtTypeRepository(this.context);
			Users = new UserRepository(this.context);
		}

		public IAccountRepository Accounts { get; private set; }

		public IExpenseTransactionRepository ExpenseTransactions { get; set; }

		public IIncomeTransactionRepository IncomeTransactions { get; set; }

		public IRecurringExpenseRepository RecurringExpenses { get; set; }

		public IRecurringIncomeRepository RecurringIncomes { get; set; }

		public IAccountDebtTypeRepository AccountDebtTypes { get; set; }
		public IUserRepository Users { get; set; }

		public int Commit()
		{
			return this.context.SaveChanges();
		}

		public void Dispose()
		{
			this.context.Dispose();
		}
	}
}