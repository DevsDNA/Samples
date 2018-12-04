namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    using System;
    using System.Diagnostics;
    using System.Reactive;
    using System.Threading.Tasks;

    public class LoginViewModel : Base.BaseReactiveViewModel
    {
        private string user;
        private string password;
        private ObservableAsPropertyHelper<bool> isLoading;
        private ReactiveCommand<Unit, Unit> doLoginCommand;
        public LoginViewModel()
        {
        }

        public string User
        {
            get => this.user;
            set => this.RaiseAndSetIfChanged(ref this.user, value);
        }

        public string Password
        {
            get => this.password;
            set => this.RaiseAndSetIfChanged(ref this.password, value);
        }

        public bool IsLoading => this.isLoading?.Value ?? false;

        public ReactiveCommand<Unit, Unit> DoLoginCommand => this.doLoginCommand;

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            this.doLoginCommand = ReactiveCommand.CreateFromTask(PerformLoginAsync, CanDoLogin());
            this.doLoginCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out this.isLoading);
        }

        private IObservable<bool> CanDoLogin()
        {
            return this.WhenAnyValue(vm => vm.User, vm => vm.Password
                   , selector: (user, pass) =>
                   {
                       if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                           return false;
                       return true;
                   });
        }

        private async Task PerformLoginAsync()
        {
            Debug.WriteLine($"User = {User} / Pass {Password}");
            await Task.Delay(5000);
        }
    }
}
