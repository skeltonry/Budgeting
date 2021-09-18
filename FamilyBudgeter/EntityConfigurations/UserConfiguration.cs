namespace FamilyBudgeterWPF
{
	using System.Data.Entity.ModelConfiguration;

	public class UserConfiguration : EntityTypeConfiguration<User>
	{
		public UserConfiguration()
		{
			ToTable("User");

			Property(e => e.FirstName)
			.IsRequired()
			.IsUnicode(false)
			.HasMaxLength(64);

			Property(e => e.LastName)
			.IsRequired()
			.IsUnicode(false)
			.HasMaxLength(64);

			HasMany(e => e.Accounts)
			.WithRequired(e => e.User)
			.WillCascadeOnDelete(false);
		}
	}
}