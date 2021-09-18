namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;

	public class RecurringIncomeConfiguration : EntityTypeConfiguration<RecurringIncome>
	{
		public RecurringIncomeConfiguration()
		{
			ToTable("RecurringIncome");

			Property(e => e.PaymentFrom)
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

			HasMany(e => e.IncomeTransactions)
			.WithOptional(e => e.RecurringIncome)
			.HasForeignKey(e => e.IncomeId);
		}
	}
}