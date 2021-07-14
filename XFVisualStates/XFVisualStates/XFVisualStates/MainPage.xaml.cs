namespace XFVisualStates
{
	using System;
	using Xamarin.Forms;

	public partial class MainPage : ContentPage
	{
		private bool disabledButtons;

		public MainPage()
		{
			InitializeComponent();
			BtnDisableControls.Command = new Command(() => BtnDisableControlsCommandExecute());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			TxtCustomStates.TextChanged += Txt_TextChanged;
			TxtMergeStates.TextChanged += Txt_TextChanged;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			TxtCustomStates.TextChanged -= Txt_TextChanged;
			TxtMergeStates.TextChanged -= Txt_TextChanged;
		}

		private void Txt_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.NewTextValue))
				VisualStateManager.GoToState(sender as VisualElement, "WithoutText");
			else
				VisualStateManager.GoToState(sender as VisualElement, "WithText");
		}

		private void BtnDisableControlsCommandExecute()
		{
			disabledButtons = !disabledButtons;
			if (disabledButtons)
				BtnDisableControls.Text = "Enable Controls.";
			else
				BtnDisableControls.Text = "Disable controls.";


			TxtCommonStates.IsEnabled = !disabledButtons;
			TxtMergeStates.IsEnabled = !disabledButtons;
		}
	}
}
