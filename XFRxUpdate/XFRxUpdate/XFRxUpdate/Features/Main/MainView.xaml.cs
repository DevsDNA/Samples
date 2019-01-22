namespace XFRxUpdate.Features.Main
{
    using ReactiveUI;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.WhenActivated(d => 
            {
                this.BindCommand(ViewModel, vm => vm.AddNewCommand, v => v.AddButton);
                this.OneWayBind(ViewModel, vm => vm.BindingData, v => v.PlanesListView.ItemsSource);
            });
        }
    }
}