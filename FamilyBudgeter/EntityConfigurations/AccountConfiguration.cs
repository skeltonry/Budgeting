namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;

	public class AccountConfiguration : EntityTypeConfiguration<Account>
	{
		public AccountConfiguration()
		{
			ToTable("Account");

			Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(64)
			.IsUnicode(false);

			Property(e => e.Balance)
			.HasColumnType("money")
			.HasPrecision(19, 4);

			HasMany(e => e.RecurringExpenses)
			.WithRequired(e => e.Account)
			.WillCascadeOnDelete(false);

			HasMany(e => e.RecurringIncomes)
			.WithRequired(e => e.Account)
			.WillCascadeOnDelete(false);
		}
	}
}