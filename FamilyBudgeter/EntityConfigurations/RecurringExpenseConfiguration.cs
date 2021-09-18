namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;

	public class RecurringExpenseConfiguration : EntityTypeConfiguration<RecurringExpense>
	{
		public RecurringExpenseConfiguration()
		{
			ToTable("RecurringExpense");

			Property(e => e.PaymentTo)
			.IsRequired()
			.HasMaxLength(64)
			.IsUnicode(false);

			Property(e => e.EstimatedAmount)
			.HasColumnType("money")
			.HasPrecision(19, 4);

			Property(e => e.EffectiveDate)
			.HasColumnType("date");

			Property(e => e.ExpirationDate)
			.HasColumnType("date");

			HasMany(e => e.ExpenseTransactions)
			.WithOptional(e => e.RecurringExpense)
			.HasForeignKey(e => e.ExpenseId);
		}
	}
}
