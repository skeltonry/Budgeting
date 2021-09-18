using System.Collections.Generic;

namespace FamilyBudgeterWPF
{
	public class ExpenseTransactionRepository : Repository<ExpenseTransaction>, IExpenseTransactionRepository
	{
		public ExpenseTransactionRepository(FamilyBudgeterContext context)
			: base(context)
		{
		}

		public FamilyBudgeterContext FamilyBudgeterContext
		{
			get { return Context as FamilyBudgeterContext; }
		}
	}
}