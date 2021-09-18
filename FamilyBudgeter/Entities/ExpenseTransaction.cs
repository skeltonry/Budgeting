namespace FamilyBudgeterWPF
{
	using System;

	public partial class ExpenseTransaction
    {
        public Guid Guid { get; set; }

        public int? ExpenseId { get; set; }

        public bool IsCleared { get; set; }

        public string Comments { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public virtual RecurringExpense RecurringExpense { get; set; }
    }
}