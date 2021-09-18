namespace FamilyBudgeterWPF
{
	using System.Data.Entity;

	public partial class FamilyBudgeterContext : DbContext
	{
		public FamilyBudgeterContext()
			: base("name=FamilyBudgeterContext")
		{
		}

		public virtual DbSet<Account> Accounts { get; set; }
		public virtual DbSet<ExpenseTransaction> ExpenseTransactions { get; set; }
		public virtual DbSet<IncomeTransaction> IncomeTransactions { get; set; }
		public virtual DbSet<RecurringExpense> RecurringExpenses { get; set; }
		public virtual DbSet<RecurringIncome> RecurringIncomes { get; set; }
		public virtual DbSet<AccountDebtType> AccountDebtTypes { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new AccountConfiguration());
			modelBuilder.Configurations.Add(new AccountDebtTypeConfiguration());
			modelBuilder.Configurations.Add(new ExpenseTransactionConfiguration());
			modelBuilder.Configurations.Add(new IncomeTransactionConfiguration());
			modelBuilder.Configurations.Add(new RecurringExpenseConfiguration());
			modelBuilder.Configurations.Add(new RecurringIncomeConfiguration());
			modelBuilder.Configurations.Add(new UserConfiguration());
		}
	}
}