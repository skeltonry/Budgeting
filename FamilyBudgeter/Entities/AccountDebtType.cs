namespace FamilyBudgeterWPF
{
	using System.Collections.Generic;
	public partial class AccountDebtType
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public AccountDebtType()
		{
			Accounts = new HashSet<Account>();
		}
		public int Id { get; set; }
		public string Name { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Account> Accounts { get; set; }
	}
}
