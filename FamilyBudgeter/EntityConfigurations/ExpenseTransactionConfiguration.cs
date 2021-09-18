namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;

	public class ExpenseTransactionConfiguration : EntityTypeConfiguration<ExpenseTransaction>
	{
		public ExpenseTransactionConfiguration()
		{
			ToTable("ExpenseTransaction");

			HasKey(e => e.Guid);

			Property(e => e.Guid).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

			Property(e => e.Comments)
			.HasMaxLength(128)
			.IsUnicode(false);

			Property(e => e.PaymentAmount)
			.HasColumnType("money")
			.HasPrecision(19, 4);

			Property(e => e.PaymentDate)
			.HasColumnType("date");
		}
	}
}