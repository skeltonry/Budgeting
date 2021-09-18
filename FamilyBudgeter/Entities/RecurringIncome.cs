namespace FamilyBudgeterWPF
{
	using System;
	using System.Collections.Generic;

	public partial class RecurringIncome
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RecurringIncome()
		{
			IncomeTransactions = new HashSet<IncomeTransaction>();
		}

		public int Id { get; set; }

		public int AccountId { get; set; }

		public string PaymentFrom { get; set; }

		public decimal EstimatedAmount { get; set; }

		public PayFrequency PayFrequency { get; set; }

		public int? DayOfMonth { get; set; }

		public DateTime EffectiveDate { get; set; }

		public DateTime? ExpirationDate { get; set; }

		public virtual Account Account { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<IncomeTransaction> IncomeTransactions { get; set; }
	}
}
