namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    using System.Reactive.Disposables;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        protected override void CreateBindingsAndSubscribes(CompositeDisposable disposables)
        {
            base.CreateBindingsAndSubscribes(disposables);
            disposables.Add(this.Bind(ViewModel, vm => vm.User, v => v.userEntry.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.Password, v => v.userPassword.Text));

            this.BindCommand(ViewModel, vm => vm.DoLoginCommand, v => v.doLoginButon).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.IsLoading, v => v.progressActivity.IsVisible).DisposeWith(disposables);
        }
    }
}