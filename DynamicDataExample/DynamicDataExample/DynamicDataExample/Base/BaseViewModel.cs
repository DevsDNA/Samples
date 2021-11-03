namespace DynamicDataExample.Base
{
    using ReactiveUI;
    using System.Reactive.Disposables;
    using System.Threading.Tasks;

    public abstract class BaseViewModel : ReactiveObject
    {
        protected CompositeDisposable disposables;

        public BaseViewModel()
        {
            CreateCommands();
        }

        public virtual Task OnAppearingAsync()
        {
            disposables = disposables ?? new CompositeDisposable();
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearingAsync()
        {
            disposables?.Dispose();
            disposables = null;
            return Task.CompletedTask;
        }

        protected virtual void CreateCommands()
        {
        }
    }
}
