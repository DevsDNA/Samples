namespace DragAndDrop
{
    using System.Diagnostics;
    using Xamarin.Forms;


    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            var label = (Label)((Element)sender).Parent;
            Debug.WriteLine($"DragGestureRecognizer_DragStarting [{label.Text}]");

            e.Data.Properties["Label"] = label;
        }

        // Disable drag and drop event for use viewmodel logic.
        private void DragGestureRecognizer_DragStarting_Collection(object sender, DragStartingEventArgs e)
        {
        }

        // Disable drag and drop event for use viewmodel logic.
        private void DropGestureRecognizer_Drop_Collection(object sender, DropEventArgs e)
        {
            e.Handled = true;
        }
    }
}