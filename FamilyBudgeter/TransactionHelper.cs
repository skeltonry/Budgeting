namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;

	public class TransactionHelper
	{
		public List<ExpenseTransaction> GenerateExpenseTransactions(DateTime beginDate, DateTime endDate, List<RecurringExpense> recurringExpenses, List<ExpenseTransaction> expenseTransactions, bool skipBeginDate)
		{
			List<ExpenseTransaction> expenseTransactionsToGenerate = new List<ExpenseTransaction>();
			List<string> existingTransactions = new List<string>();

			expenseTransactions.ForEach(x =>
			{
				string itemKey = String.Format("{0}:{1}", x.ExpenseId, x.PaymentDate.ToShortDateString());
				existingTransactions.Add(itemKey);
			});

			foreach (RecurringExpense recurringExpense in recurringExpenses)
			{
				DateTime expirationDate = recurringExpense.ExpirationDate.HasValue ? recurringExpense.ExpirationDate.Value : DateTime.MaxValue;

				// An account starts on the date that it is effective and it ends at the very end of the date that it expires.
				if (expirationDate.Date <= beginDate.Date || recurringExpense.EffectiveDate.Date > endDate.Date)
				{
					continue;
				}

				DateTime dateToCheck = this.GetFirstDate(recurringExpense.PayFrequency, recurringExpense.EffectiveDate, recurringExpense.DayOfMonth);

				while (dateToCheck.Date <= endDate
					&& expirationDate >= dateToCheck.Date)
				{
					// if the date to check is equal to an income date then we skip over it because we assume the income has already been received.
					if ((dateToCheck.Date > beginDate.Date || (dateToCheck.Date >= beginDate.Date && !skipBeginDate))
						&& dateToCheck.Date <= endDate.Date
						&& recurringExpense.EffectiveDate.Date <= dateToCheck.Date)
					{
						string transactionKey = String.Format("{0}:{1}", recurringExpense.Id, dateToCheck.Date.ToShortDateString());

						if (!existingTransactions.Contains(transactionKey))
						{
							ExpenseTransaction expenseTransaction = new ExpenseTransaction()
							{
								ExpenseId = recurringExpense.Id,
								IsCleared = false,
								PaymentDate = dateToCheck.Date,
								PaymentAmount = recurringExpense.EstimatedAmount,
								Comments = "Auto-Generated",
							};

							expenseTransactionsToGenerate.Add(expenseTransaction);
						}
					}

					switch (recurringExpense.PayFrequency)
					{
						case PayFrequency.Weekly:
							{
								dateToCheck = dateToCheck.AddDays(7);
								break;
							}

						case PayFrequency.BiWeekly:
							{
								dateToCheck = dateToCheck.AddDays(14);
								break;
							}

						case PayFrequency.Monthly:
							{
								dateToCheck = dateToCheck.AddMonths(1);
								dateToCheck = new DateTime(dateToCheck.Year, dateToCheck.Month, recurringExpense.DayOfMonth.Value);
								break;
							}

						case PayFrequency.Quarterly:
							{
								dateToCheck = dateToCheck.AddMonths(3);
								break;
							}

						case PayFrequency.Yearly:
							{
								dateToCheck = dateToCheck.AddYears(1);
								break;
							}
					}
				}
			}

			return expenseTransactionsToGenerate;
		}

		public List<IncomeTransaction> GenerateIncomeTransactions(DateTime beginDate, DateTime endDate, List<RecurringIncome> recurringIncomes, List<IncomeTransaction> incomeTransactions, bool skipBeginDate)
		{
			List<IncomeTransaction> incomeTransactionsToGenerate = new List<IncomeTransaction>();
			List<string> existingTransactions = new List<string>();

			incomeTransactions.ForEach(x =>
				{
					string itemKey = String.Format("{0}:{1}", x.IncomeId, x.ReceiveDate.ToShortDateString());
					existingTransactions.Add(itemKey);
				});

			foreach (RecurringIncome recurringIncome in recurringIncomes)
			{
				DateTime expirationDate = recurringIncome.ExpirationDate.HasValue ? recurringIncome.ExpirationDate.Value : DateTime.MaxValue;

				// An account starts on the date that it is effective and it ends at the very end of the date that it expires.
				if (expirationDate.Date <= beginDate.Date || recurringIncome.EffectiveDate.Date > endDate.Date)
				{
					continue;
				}

				DateTime dateToCheck = this.GetFirstDate(recurringIncome.PayFrequency, recurringIncome.EffectiveDate, recurringIncome.DayOfMonth);

				while (dateToCheck.Date <= endDate
					&& expirationDate >= dateToCheck.Date)
				{
					// if the date to check is equal to an income date then we skip over it because we assume the income has already been received.
					if ((dateToCheck.Date > beginDate.Date || (dateToCheck.Date >= beginDate.Date && !skipBeginDate))
						&& dateToCheck.Date <= endDate.Date
						&& recurringIncome.EffectiveDate.Date <= dateToCheck.Date)
					{
						string transactionKey = String.Format("{0}:{1}", recurringIncome.Id, dateToCheck.Date.ToShortDateString());

						if (!existingTransactions.Contains(transactionKey))
						{
							IncomeTransaction incomeTransaction = new IncomeTransaction()
							{
								IncomeId = recurringIncome.Id,
								IsCleared = false,
								ReceiveDate = dateToCheck.Date,
								ReceiveAmount = recurringIncome.EstimatedAmount,
								Comments = "Auto-Generated",
							};

							incomeTransactionsToGenerate.Add(incomeTransaction);
						}
					}

					switch (recurringIncome.PayFrequency)
					{
						case PayFrequency.Weekly:
							{
								dateToCheck = dateToCheck.AddDays(7);
								break;
							}

						case PayFrequency.BiWeekly:
							{
								dateToCheck = dateToCheck.AddDays(14);
								break;
							}

						case PayFrequency.Monthly:
							{
								dateToCheck = dateToCheck.AddMonths(1);
								dateToCheck = new DateTime(dateToCheck.Year, dateToCheck.Month, recurringIncome.DayOfMonth.Value);
								break;
							}

						case PayFrequency.Quarterly:
							{
								dateToCheck = dateToCheck.AddMonths(3);
								break;
							}

						case PayFrequency.Yearly:
							{
								dateToCheck = dateToCheck.AddYears(1);
								break;
							}
					}
				}
			}

			return incomeTransactionsToGenerate;
		}

		public DateTime GetFirstDate(PayFrequency payFrequency, DateTime effectiveDate, int? dayOfMonth)
		{
			DateTime checkDate;

			switch (payFrequency)
			{
				case PayFrequency.Weekly:
					{
						return effectiveDate;
					}
				case PayFrequency.BiWeekly:
					{
						return effectiveDate;
					}
				case PayFrequency.Monthly:
					{
						checkDate = new DateTime(effectiveDate.Year, effectiveDate.Month, dayOfMonth.Value);

						if (effectiveDate > checkDate)
						{
							return checkDate.AddMonths(1);
						}

						return checkDate;
					}
				case PayFrequency.Yearly:
					{
						checkDate = new DateTime(effectiveDate.Year, effectiveDate.Month, dayOfMonth.Value);

						if (effectiveDate > checkDate)
						{
							return checkDate.AddYears(1);
						}

						return checkDate;
					}
				case PayFrequency.Quarterly:
					{
						int[] quartersInYear = { 3, 6, 9, 12 };

						for (int i = 0; i <= 3; i++)
						{
							int monthToCheck = quartersInYear[i];

							checkDate = new DateTime(effectiveDate.Year, monthToCheck, dayOfMonth.Value);

							if (checkDate >= effectiveDate)
							{
								return checkDate;
							}
						}

						break;
					}
			}

			return DateTime.MaxValue;
		}
	}
}