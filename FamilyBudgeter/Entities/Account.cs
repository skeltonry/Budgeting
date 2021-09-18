namespace FamilyBudgeterWPF
{
	using System.Collections.Generic;

	public partial class Account
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Account()
		{
			RecurringExpenses = new HashSet<RecurringExpense>();
			RecurringIncomes = new HashSet<RecurringIncome>();
		}

		public int Id { get; set; }

		public int UserId { get; set; }

		public string Name { get; set; }

		public AccountType Type { get; set; }

		public decimal? Balance { get; set; }

		public int? AccountDebtTypeId { get; set; }

		public virtual AccountDebtType DebtType { get; set; }

		public virtual User User { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<RecurringExpense> RecurringExpenses { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<RecurringIncome> RecurringIncomes { get; set; }
	}
}
