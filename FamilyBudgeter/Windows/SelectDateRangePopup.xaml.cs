namespace FamilyBudgeterWPF
{
	using System;
	using System.Windows;

	/// <summary>
	/// Interaction logic for SelectDateRangePopup.xaml
	/// </summary>
	public partial class SelectDateRangePopup : Window
	{
		#region Properties

		public DateTime BeginDate { get; set; }

		public DateTime EndDate { get; set; }

		#endregion

		#region Constructors

		public SelectDateRangePopup()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Methods

		private void OnButtonOkClick(object sender, RoutedEventArgs e)
		{
			this.BeginDate = this.datePickerBeginDate.SelectedDate.Value;
			this.EndDate = this.datePickerEndDate.SelectedDate.Value;

			this.DialogResult = true;

			this.Close();
		}

		#endregion
	}
}