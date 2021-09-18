namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using System.Linq;

	/// <summary>
	/// Interaction logic for AddRecurringExpensePopup.xaml
	/// </summary>
	public partial class AddRecurringExpensePopup : Window
	{
		#region Properties

		private FamilyBudgeterContext DbContext { get; set; }

		#endregion

		#region Constructors

		public AddRecurringExpensePopup(FamilyBudgeterContext dbContext)
		{
			this.InitializeComponent();
			this.DbContext = dbContext;
		}

		#endregion

		#region Methods

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.comboBoxAccount.ItemsSource = this.DbContext.Accounts.Where(ex => ex.Type == AccountType.Expense).OrderBy(ex => ex.Name).ToList();
			this.comboBoxAccount.DisplayMemberPath = "Name";
			this.comboBoxAccount.SelectedValuePath = "Name";

			List<PayFrequencyModel> payFrequencies = new List<PayFrequencyModel>();
			payFrequencies.Add(new PayFrequencyModel() { Frequency = PayFrequency.Weekly, Name = PayFrequency.Weekly.ToString() });
			payFrequencies.Add(new PayFrequencyModel() { Frequency = PayFrequency.BiWeekly, Name = PayFrequency.BiWeekly.ToString() });
			payFrequencies.Add(new PayFrequencyModel() { Frequency = PayFrequency.Monthly, Name = PayFrequency.Monthly.ToString() });
			payFrequencies.Add(new PayFrequencyModel() { Frequency = PayFrequency.Quarterly, Name = PayFrequency.Quarterly.ToString() });
			payFrequencies.Add(new PayFrequencyModel() { Frequency = PayFrequency.Yearly, Name = PayFrequency.Yearly.ToString() });

			this.comboBoxPayFrequency.ItemsSource = payFrequencies;
			this.comboBoxPayFrequency.DisplayMemberPath = "Name";
			this.comboBoxPayFrequency.SelectedValuePath = "Name";
		}

		private void OnButtonCreateExpenseClick(object sender, RoutedEventArgs e)
		{
			RecurringExpense recurringExpense = this.DbContext.RecurringExpenses.Create();

			Account account = (Account)this.comboBoxAccount.SelectedItem;
			PayFrequencyModel payFrequency = (PayFrequencyModel)this.comboBoxPayFrequency.SelectedItem;

			recurringExpense.AccountId = account.Id;
			recurringExpense.PaymentTo = this.textBoxPaymentTo.Text;
			recurringExpense.PayFrequency = payFrequency.Frequency;

			decimal estimatedAmount;
			if (Decimal.TryParse(this.textBoxEstimatedAmount.Text, out estimatedAmount))
			{
				recurringExpense.EstimatedAmount = estimatedAmount;
			}

			if (payFrequency.Frequency == PayFrequency.Monthly)
			{
				int dayOfMonth;
				if (Int32.TryParse(this.textBoxDayOfMonth.Text, out dayOfMonth))
				{
					recurringExpense.DayOfMonth = dayOfMonth;
				}
			}

			recurringExpense.EffectiveDate = this.datePickerEffectiveDate.SelectedDate.Value;

			this.DbContext.RecurringExpenses.Add(recurringExpense);
			this.DbContext.SaveChanges();

			this.Close();
		}

		#endregion
	}
}
