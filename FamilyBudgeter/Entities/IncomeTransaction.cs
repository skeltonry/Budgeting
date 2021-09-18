namespace FamilyBudgeterWPF
{
	using System;

	public partial class IncomeTransaction
    {
        public Guid Guid { get; set; }

        public int? IncomeId { get; set; }

        public bool IsCleared { get; set; }

        public string Comments { get; set; }

        public DateTime ReceiveDate { get; set; }

        public decimal ReceiveAmount { get; set; }

        public virtual RecurringIncome RecurringIncome { get; set; }
    }
}