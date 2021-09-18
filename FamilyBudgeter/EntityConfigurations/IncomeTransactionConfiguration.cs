namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;

	public class IncomeTransactionConfiguration : EntityTypeConfiguration<IncomeTransaction>
	{
		public IncomeTransactionConfiguration()
		{
			ToTable("IncomeTransaction");

			HasKey(e => e.Guid);

			Property(e => e.Guid).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

			Property(e => e.Comments)
			.HasMaxLength(128)
			.IsUnicode(false);

			Property(e => e.ReceiveAmount)
			.HasColumnType("money")
			.HasPrecision(19, 4);

			Property(e => e.ReceiveDate)
			.HasColumnType("date");
		}
	}
}