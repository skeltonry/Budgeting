namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;
	public class AccountDebtTypeConfiguration : EntityTypeConfiguration<AccountDebtType>
	{
		public AccountDebtTypeConfiguration()
		{
			ToTable("AccountDebtType");

			Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(64)
			.IsUnicode(false);
		}
	}
}
