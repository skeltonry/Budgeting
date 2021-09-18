namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using System.Linq;

	/// <summary>
	/// Interaction logic for AddIncomeTransactionPopup.xaml
	/// </summary>
	public partial class AddIncomeTransactionPopup : Window
	{
		#region Properties

		FamilyBudgeterContext DbContext;

		#endregion

		#region Constructors

		public AddIncomeTransactionPopup(FamilyBudgeterContext dbContext)
		{
			this.InitializeComponent();
			this.DbContext = dbContext;
		}

		#endregion

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.comboBoxIsCleared.Items.Add("No");
			this.comboBoxIsCleared.Items.Add("Yes");

			this.comboBoxRecurringIncome.ItemsSource = this.DbContext.RecurringIncomes.OrderBy(i => i.PaymentFrom).ToList();
			this.comboBoxRecurringIncome.DisplayMemberPath = "PaymentFrom";
			this.comboBoxRecurringIncome.SelectedValuePath = "PaymentFrom";
		}

		private void OnButtonAddTransactionClick(object sender, RoutedEventArgs e)
		{
			IncomeTransaction incomeTransaction = this.DbContext.IncomeTransactions.Create();

			RecurringIncome recurringIncome = this.comboBoxRecurringIncome.SelectedItem as RecurringIncome;
			if (recurringIncome != null)
			{
				incomeTransaction.IncomeId = recurringIncome.Id;
			}

			if (!String.IsNullOrWhiteSpace(this.textBoxComments.Text))
			{
				incomeTransaction.Comments = this.textBoxComments.Text;
			}

			incomeTransaction.IsCleared = this.comboBoxIsCleared.Text.ToUpper() == "YES" ? true : false;
			incomeTransaction.ReceiveDate = this.datePickerReceiveDate.SelectedDate.Value;

			decimal receiveAmount;
			if (Decimal.TryParse(this.textBoxReceiveAmount.Text, out receiveAmount))
			{
				incomeTransaction.ReceiveAmount = receiveAmount;
			}

			this.DbContext.IncomeTransactions.Add(incomeTransaction);
			this.DbContext.SaveChanges();

			this.Close();
		}
	}
}