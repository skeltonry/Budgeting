namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Windows;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Properties

		private FamilyBudgeterContext UsersContext { get; set; }

		private FamilyBudgeterContext AccountsContext { get; set; }

		private FamilyBudgeterContext RecurringIncomesContext { get; set; }

		private FamilyBudgeterContext RecurringExpensesContext { get; set; }

		private FamilyBudgeterContext IncomeTransactionsContext { get; set; }

		private FamilyBudgeterContext ExpenseTransactionsContext { get; set; }

		#endregion

		#region Constructors

		public MainWindow()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Methods

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.LoadUsers();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.UsersContext?.Dispose();
			this.AccountsContext?.Dispose();
			this.RecurringIncomesContext?.Dispose();
			this.RecurringExpensesContext?.Dispose();
			this.IncomeTransactionsContext?.Dispose();
			this.ExpenseTransactionsContext?.Dispose();
		}

		private void OnButtonSaveUserChangesClick(object sender, RoutedEventArgs e)
		{
			this.UsersContext.SaveChanges();
			MessageBox.Show(this, "Changes have been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void OnButtonAddAccountClick(object sender, RoutedEventArgs e)
		{
			AddAccountPopup accountPopup = new AddAccountPopup(this.AccountsContext);
			accountPopup.Show();
		}

		private void OnButtonSaveAccountChangesClick(object sender, RoutedEventArgs e)
		{
			this.AccountsContext.SaveChanges();
			MessageBox.Show(this, "Changes have been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void OnButtonAddRecurringIncomeClick(object sender, RoutedEventArgs e)
		{
			AddRecurringIncomePopup recurringIncomePopup = new AddRecurringIncomePopup(this.RecurringIncomesContext);
			recurringIncomePopup.Show();
		}

		private void OnButtonSaveRecurringIncomeChangesClick(object sender, RoutedEventArgs e)
		{
			this.RecurringIncomesContext.SaveChanges();
			MessageBox.Show(this, "Changes have been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void OnButtonAddRecurringExpenseClick(object sender, RoutedEventArgs e)
		{
			AddRecurringExpensePopup recurringExpensePopup = new AddRecurringExpensePopup(this.RecurringExpensesContext);
			recurringExpensePopup.Show();
		}

		private void OnButtonSaveRecurringExpenseChangesClick(object sender, RoutedEventArgs e)
		{
			this.RecurringExpensesContext.SaveChanges();
			MessageBox.Show(this, "Changes have been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void OnButtonShowDefaultIncomeTransactionsClick(object sender, RoutedEventArgs e)
		{
			this.IncomeTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource incomeTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("incomeTransactionViewSource")));
			this.IncomeTransactionsContext.IncomeTransactions.Where(it => it.ReceiveDate >= DateTime.Now || !it.IsCleared).OrderBy(it => it.ReceiveDate).Load();
			incomeTransactionViewSource.Source = this.IncomeTransactionsContext.IncomeTransactions.Local;
		}

		private void OnButtonShowOpenIncomeTransactionsClick(object sender, RoutedEventArgs e)
		{
			this.IncomeTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource incomeTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("incomeTransactionViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.IncomeTransactionsContext.IncomeTransactions.Where(it => !it.IsCleared).OrderBy(it => it.ReceiveDate).Load();
			incomeTransactionViewSource.Source = this.IncomeTransactionsContext.IncomeTransactions.Local;
		}

		private void OnButtonShowAllIncomeTransactionsClick(object sender, RoutedEventArgs e)
		{
			this.IncomeTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource incomeTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("incomeTransactionViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.IncomeTransactionsContext.IncomeTransactions.OrderBy(it => it.ReceiveDate).Load();
			incomeTransactionViewSource.Source = this.IncomeTransactionsContext.IncomeTransactions.Local;
		}

		private void OnButtonAddIncomeTransactionClick(object sender, RoutedEventArgs e)
		{
			AddIncomeTransactionPopup incomeTransactionPopup = new AddIncomeTransactionPopup(this.IncomeTransactionsContext);
			incomeTransactionPopup.Show();
		}

		private void OnButtonSaveIncomeTransactionsChangesClick(object sender, RoutedEventArgs e)
		{
			this.IncomeTransactionsContext.SaveChanges();
			MessageBox.Show(this, "Changes have been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void OnButtonAutoGenIncomeTransactionsClick(object sender, RoutedEventArgs e)
		{
			DateTime beginDate = DateTime.MinValue;
			DateTime endDate = DateTime.MinValue;

			SelectDateRangePopup selectDateRangePopup = new SelectDateRangePopup();
			bool? dialogReturned = selectDateRangePopup.ShowDialog();

			if (dialogReturned.HasValue && dialogReturned.Value == true)
			{
				beginDate = selectDateRangePopup.BeginDate;
				endDate = selectDateRangePopup.EndDate;
			}
			else
			{
				MessageBox.Show(this, "No transactions were generated.", "Action Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			UnitOfWork recurringTransactionsUnitOfWork = new UnitOfWork(new FamilyBudgeterContext());

			TransactionHelper transactionHelper = new TransactionHelper();
			List<IncomeTransaction> transactionsToGenerate = transactionHelper.GenerateIncomeTransactions(
				beginDate,
				endDate,
				recurringTransactionsUnitOfWork.RecurringIncomes.GetAll().ToList<RecurringIncome>(),
				this.IncomeTransactionsContext.IncomeTransactions.ToList<IncomeTransaction>(),
				false);

			this.IncomeTransactionsContext.IncomeTransactions.AddRange(transactionsToGenerate);
			this.IncomeTransactionsContext.SaveChanges();
		}

		private void OnButtonShowDefaultExpenseTransactionsClick(object sender, RoutedEventArgs e)
		{
			this.ExpenseTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource expenseTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("expenseTransactionViewSource")));
			this.ExpenseTransactionsContext.ExpenseTransactions.Where(ex => ex.PaymentDate >= DateTime.Now || !ex.IsCleared).OrderBy(ex => ex.PaymentDate).Load();
			expenseTransactionViewSource.Source = this.ExpenseTransactionsContext.ExpenseTransactions.Local;
		}

		private void OnButtonShowOpenExpenseTransactionsClick(object sender, RoutedEventArgs e)
		{
			this.ExpenseTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource expenseTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("expenseTransactionViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.ExpenseTransactionsContext.ExpenseTransactions.Where(ex => !ex.IsCleared).OrderBy(ex => ex.PaymentDate).Load();
			expenseTransactionViewSource.Source = this.ExpenseTransactionsContext.ExpenseTransactions.Local;
		}

		private void OnButtonShowAllExpenseTransactionsClick(object sender, RoutedEventArgs e)
		{
			this.ExpenseTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource expenseTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("expenseTransactionViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.ExpenseTransactionsContext.ExpenseTransactions.OrderBy(ex => ex.PaymentDate).Load();
			expenseTransactionViewSource.Source = this.ExpenseTransactionsContext.ExpenseTransactions.Local;
		}

		private void OnButtonAddExpenseTransactionClick(object sender, RoutedEventArgs e)
		{
			AddExpenseTransactionPopup expenseTransactionPopup = new AddExpenseTransactionPopup(this.ExpenseTransactionsContext);
			expenseTransactionPopup.Show();
		}

		private void OnButtonSaveExpenseTransactionsChangesClick(object sender, RoutedEventArgs e)
		{
			this.ExpenseTransactionsContext.SaveChanges();
			MessageBox.Show(this, "Changes have been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void OnButtonAutoGenExpenseTransactionsClick(object sender, RoutedEventArgs e)
		{
			DateTime beginDate = DateTime.MinValue;
			DateTime endDate = DateTime.MinValue;

			SelectDateRangePopup selectDateRangePopup = new SelectDateRangePopup();
			bool? dialogReturned = selectDateRangePopup.ShowDialog();

			if (dialogReturned.HasValue && dialogReturned.Value == true)
			{
				beginDate = selectDateRangePopup.BeginDate;
				endDate = selectDateRangePopup.EndDate;
			}
			else
			{
				MessageBox.Show(this, "No transactions were generated.", "Action Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			UnitOfWork recurringTransactionsUnitOfWork = new UnitOfWork(new FamilyBudgeterContext());

			TransactionHelper transactionHelper = new TransactionHelper();
			List<ExpenseTransaction> transactionsToGenerate = transactionHelper.GenerateExpenseTransactions(
				beginDate,
				endDate,
				recurringTransactionsUnitOfWork.RecurringExpenses.GetAll().ToList<RecurringExpense>(),
				this.ExpenseTransactionsContext.ExpenseTransactions.ToList<ExpenseTransaction>(),
				false);

			this.ExpenseTransactionsContext.ExpenseTransactions.AddRange(transactionsToGenerate);
			this.ExpenseTransactionsContext.SaveChanges();
		}

		private void OnButtonCalculateCashFlowClick(object sender, RoutedEventArgs e)
		{
			decimal currentCash = Convert.ToDecimal(this.textBoxCurrentCash.Text);
			DateTime dateToCheck = this.datePickerSelectedDate.SelectedDate.Value;

			decimal totalIncome = 0;
			decimal totalExpense = 0;

			var unitOfWork = new UnitOfWork(new FamilyBudgeterContext());

			// Loop through all income transactions
			foreach (IncomeTransaction incomeTransaction in unitOfWork.IncomeTransactions.GetAll())
			{
				// Skip transactions that have cleared
				if (incomeTransaction.IsCleared)
				{
					continue;
				}

				if (incomeTransaction.ReceiveDate <= dateToCheck)
				{
					totalIncome += incomeTransaction.ReceiveAmount;
				}
			}

			// Loop through all expense transactions
			foreach (ExpenseTransaction expenseTransaction in unitOfWork.ExpenseTransactions.GetAll())
			{
				// Skip transactions that have cleared
				if (expenseTransaction.IsCleared)
				{
					continue;
				}

				if (expenseTransaction.PaymentDate <= dateToCheck)
				{
					totalExpense += expenseTransaction.PaymentAmount;
				}
			}

			decimal remainingCash = (currentCash + totalIncome) - totalExpense;
			this.textBoxRemainingCash.Text = remainingCash.ToString();
		}

		private void OnTabControlSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (e.Source == this.tabControl)
			{
				if (this.tabItemUsers.IsSelected)
				{
					this.LoadUsers();
				}
				else if (this.tabItemAccounts.IsSelected)
				{
					this.LoadAccounts();
				}
				else if (this.tabItemRecurringIncome.IsSelected)
				{
					this.LoadRecurringIncomes();
				}
				else if (this.tabItemRecurringExpenses.IsSelected)
				{
					this.LoadRecurringExpenses();
				}
				else if (this.tabItemIncomeTransactions.IsSelected)
				{
					this.LoadIncomeTransactions();
				}
				else if (this.tabItemExpenseTransactions.IsSelected)
				{
					this.LoadExpenseTransactions();
				}
				else if (this.tabItemTotalDebt.IsSelected)
				{
					this.LoadTotalDebtItems();
				}
			}
		}

		private void LoadUsers()
		{
			this.UsersContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.UsersContext.Users.Load();
			userViewSource.Source = this.UsersContext.Users.Local;
		}

		private void LoadAccounts()
		{
			this.AccountsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource accountViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("accountViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.AccountsContext.Accounts.OrderBy(a => a.Name).Load();
			accountViewSource.Source = this.AccountsContext.Accounts.Local;
		}

		private void LoadRecurringIncomes()
		{
			this.RecurringIncomesContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource recurringIncomeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("recurringIncomeViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.RecurringIncomesContext.RecurringIncomes.OrderBy(ri => ri.Account.Name).Load();
			recurringIncomeViewSource.Source = this.RecurringIncomesContext.RecurringIncomes.Local;
		}

		private void LoadRecurringExpenses()
		{
			this.RecurringExpensesContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource recurringExpenseViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("recurringExpenseViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.RecurringExpensesContext.RecurringExpenses.OrderBy(re => re.Account.Name).Load();
			recurringExpenseViewSource.Source = this.RecurringExpensesContext.RecurringExpenses.Local;
		}

		private void LoadIncomeTransactions()
		{
			this.IncomeTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource incomeTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("incomeTransactionViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.IncomeTransactionsContext.IncomeTransactions.Where(it => it.ReceiveDate >= DateTime.Now || !it.IsCleared).OrderBy(it => it.ReceiveDate).Load();
			incomeTransactionViewSource.Source = this.IncomeTransactionsContext.IncomeTransactions.Local;
		}

		private void LoadExpenseTransactions()
		{
			this.ExpenseTransactionsContext = new FamilyBudgeterContext();
			System.Windows.Data.CollectionViewSource expenseTransactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("expenseTransactionViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			this.ExpenseTransactionsContext.ExpenseTransactions.Where(ex => ex.PaymentDate >= DateTime.Now || !ex.IsCleared).OrderBy(ex => ex.PaymentDate).Load();
			expenseTransactionViewSource.Source = this.ExpenseTransactionsContext.ExpenseTransactions.Local;
		}

		private void LoadTotalDebtItems()
		{
			System.Windows.Data.CollectionViewSource totalDebtModelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("totalDebtModelViewSource")));

			var unitOfWork = new UnitOfWork(new FamilyBudgeterContext());

			List<Account> debtAccounts = unitOfWork.Accounts.Find(x => x.DebtType != null).ToList();

			var accountDebtModels = new List<TotalDebtModel>();
			foreach (Account account in debtAccounts.OrderBy(x => x.UserId))
			{
				accountDebtModels.Add(
					new TotalDebtModel()
					{
						UserName = $"{account.User.FirstName} {account.User.LastName}",
						Description = account.Name,
						DebtType = account.DebtType.Name,
						Amount = account.Balance
					});
			}

			var groupedUsersDebtType = accountDebtModels.GroupBy(a => new
			{
				a.UserName,
				a.DebtType
			})
			.Select(ga => new TotalDebtModel()
			{
				UserName = ga.Key.UserName,
				Description = "TOTALS",
				DebtType = ga.Key.DebtType.ToUpper(),
				Amount = ga.Sum(a => a.Amount)
			});

			var groupedUsers = accountDebtModels.GroupBy(x => x.UserName)
				.Select(g => new TotalDebtModel()
				{
					UserName = g.Key,
					Description = "TOTALS",
					DebtType = "ALL",
					Amount = g.Sum(x => x.Amount)
				});

			var groupingTotals = new List<TotalDebtModel>();

			foreach (var totalDebtModel in groupedUsersDebtType)
			{
				groupingTotals.Add(totalDebtModel);
			}

			foreach (var totalDebtModel in groupedUsers)
			{
				groupingTotals.Add(totalDebtModel);
			}

			foreach (var totalDebtModel in groupingTotals)
			{
				accountDebtModels.Add(totalDebtModel);
			}

			totalDebtModelViewSource.Source = accountDebtModels;
		}

		#endregion
	}
}