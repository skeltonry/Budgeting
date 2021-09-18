namespace FamilyBudgeterWPF
{
	using System;

	public interface IUnitOfWork : IDisposable
	{
		IAccountRepository Accounts { get; }

		IExpenseTransactionRepository ExpenseTransactions { get; }

		IIncomeTransactionRepository IncomeTransactions { get; }

		IRecurringExpenseRepository RecurringExpenses { get; }

		IRecurringIncomeRepository RecurringIncomes { get; }

		IUserRepository Users { get; }

		int Commit();
	}
}