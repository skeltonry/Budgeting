namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using System.Linq;

	/// <summary>
	/// Interaction logic for AddAccountPopup.xaml
	/// </summary>
	public partial class AddAccountPopup : Window
	{
		private FamilyBudgeterContext DbContext { get; set; }

		public AddAccountPopup(FamilyBudgeterContext dbContext)
		{
			InitializeComponent();
			this.DbContext = dbContext;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			List<UserModel> users = new List<UserModel>();
			foreach (User user in this.DbContext.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList())
			{
				users.Add(new UserModel() { Id = user.Id, FullName = String.Concat(user.FirstName, " ", user.LastName) });
			}

			this.comboBoxUser.ItemsSource = users;
			this.comboBoxUser.DisplayMemberPath = "FullName";
			this.comboBoxUser.SelectedValuePath = "FullName";

			var unitOfWork = new UnitOfWork(this.DbContext);
			List<AccountDebtType> accountDebtTypes = unitOfWork.AccountDebtTypes.GetAll().ToList<AccountDebtType>();

			this.comboBoxDebtType.ItemsSource = accountDebtTypes;
			this.comboBoxDebtType.DisplayMemberPath = "Name";
			this.comboBoxUser.SelectedValuePath = "Name";

			List<AccountTypeModel> accountTypes = new List<AccountTypeModel>();
			accountTypes.Add(new AccountTypeModel() { Type = AccountType.Income, Name = AccountType.Income.ToString() });
			accountTypes.Add(new AccountTypeModel() { Type = AccountType.Expense, Name = AccountType.Expense.ToString() });

			this.comboBoxType.ItemsSource = accountTypes;
			this.comboBoxType.DisplayMemberPath = "Name";
			this.comboBoxType.SelectedValuePath = "Name";
		}

		private void OnButtonCreateAccountClick(object sender, RoutedEventArgs e)
		{
			Account account = this.DbContext.Accounts.Create();

			UserModel user = (UserModel)this.comboBoxUser.SelectedItem;
			AccountTypeModel accountType = (AccountTypeModel)this.comboBoxType.SelectedItem;
			AccountDebtType accountDebtType = (AccountDebtType)this.comboBoxDebtType.SelectedItem;

			account.UserId = user.Id;
			account.Name = this.textBoxName.Text;
			account.Type = accountType.Type;

			if (accountDebtType != null)
			{
				account.AccountDebtTypeId = accountDebtType.Id;
			}

			decimal balance;
			if (Decimal.TryParse(this.textBoxBalance.Text, out balance))
			{
				account.Balance = balance;
			}

			this.DbContext.Accounts.Add(account);
			this.DbContext.SaveChanges();

			this.Close();
		}
	}
}