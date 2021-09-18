namespace FamilyBudgeterWPF
{
	using System;
	using System.Windows;
	using System.Linq;

	/// <summary>
	/// Interaction logic for AddExpenseTransactionPopup.xaml
	/// </summary>
	public partial class AddExpenseTransactionPopup : Window
	{
		#region Properties

		private FamilyBudgeterContext DbContext;

		#endregion

		public AddExpenseTransactionPopup(FamilyBudgeterContext dbContext)
		{
			this.InitializeComponent();
			this.DbContext = dbContext;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.comboBoxIsCleared.Items.Add("No");
			this.comboBoxIsCleared.Items.Add("Yes");

			this.comboBoxRecurringExpense.ItemsSource = this.DbContext.RecurringExpenses.OrderBy(ex => ex.PaymentTo).ToList();
			this.comboBoxRecurringExpense.DisplayMemberPath = "PaymentTo";
			this.comboBoxRecurringExpense.SelectedValuePath = "PaymentTo";
		}

		private void OnButtonAddTransactionClick(object sender, RoutedEventArgs e)
		{
			ExpenseTransaction expenseTransaction = this.DbContext.ExpenseTransactions.Create();

			RecurringExpense recurringExpense = this.comboBoxRecurringExpense.SelectedItem as RecurringExpense;
			if (recurringExpense != null)
			{
				expenseTransaction.ExpenseId = recurringExpense.Id;
			}

			if (!String.IsNullOrWhiteSpace(this.textBoxComments.Text))
			{
				expenseTransaction.Comments = this.textBoxComments.Text;
			}

			expenseTransaction.IsCleared = this.comboBoxIsCleared.Text.ToUpper() == "YES" ? true : false;
			expenseTransaction.PaymentDate = this.datePickerPaymentDate.SelectedDate.Value;

			decimal receiveAmount;
			if (Decimal.TryParse(this.textBoxPaymentAmount.Text, out receiveAmount))
			{
				expenseTransaction.PaymentAmount = receiveAmount;
			}

			this.DbContext.ExpenseTransactions.Add(expenseTransaction);
			this.DbContext.SaveChanges();

			this.Close();
		}
	}
}