namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using System.Linq;

	/// <summary>
	/// Interaction logic for AddRecurringIncomePopup.xaml
	/// </summary>
	public partial class AddRecurringIncomePopup : Window
	{
		private FamilyBudgeterContext DbContext { get; set; }

		public AddRecurringIncomePopup(FamilyBudgeterContext dbContext)
		{
			this.InitializeComponent();
			this.DbContext = dbContext;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.comboBoxAccount.ItemsSource = this.DbContext.Accounts.Where(i => i.Type == AccountType.Income).OrderBy(i => i.Name).ToList();
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

		private void OnButtonCreateIncomeClick(object sender, RoutedEventArgs e)
		{
			RecurringIncome recurringIncome = this.DbContext.RecurringIncomes.Create();

			Account account = (Account)this.comboBoxAccount.SelectedItem;
			PayFrequencyModel payFrequency = (PayFrequencyModel)this.comboBoxPayFrequency.SelectedItem;

			recurringIncome.AccountId = account.Id;
			recurringIncome.PaymentFrom = this.textBoxPaymentFrom.Text;
			recurringIncome.PayFrequency = payFrequency.Frequency;

			decimal estimatedAmount;
			if (Decimal.TryParse(this.textBoxEstimatedAmount.Text, out estimatedAmount))
			{
				recurringIncome.EstimatedAmount = estimatedAmount;
			}

			if (payFrequency.Frequency == PayFrequency.Monthly)
			{
				int dayOfMonth;
				if (Int32.TryParse(this.textBoxDayOfMonth.Text, out dayOfMonth))
				{
					recurringIncome.DayOfMonth = dayOfMonth;
				}
			}

			recurringIncome.EffectiveDate = this.datePickerEffectiveDate.SelectedDate.Value;

			this.DbContext.RecurringIncomes.Add(recurringIncome);
			this.DbContext.SaveChanges();

			this.Close();
		}
	}
}