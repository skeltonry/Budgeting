namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;

	public partial class RecurringExpense
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RecurringExpense()
		{
			ExpenseTransactions = new HashSet<ExpenseTransaction>();
		}

		public int Id { get; set; }

		public int AccountId { get; set; }

		public string PaymentTo { get; set; }

		public decimal EstimatedAmount { get; set; }

		public PayFrequency PayFrequency { get; set; }

		public int? DayOfMonth { get; set; }

		public DateTime EffectiveDate { get; set; }

		public DateTime? ExpirationDate { get; set; }

		public virtual Account Account { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<ExpenseTransaction> ExpenseTransactions { get; set; }
	}
}
