namespace ReactiveExtensionExamples.Base
{
    using ReactiveUI;
    using ReactiveUI.XamForms;
    using System;
    using System.Reactive.Disposables;
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

    public class BaseReactiveContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseReactiveViewModel
    {
        protected CompositeDisposable disposables;
        public BaseReactiveContentPage()
        {
            ViewModel = InstanceViewModel();
            BindingContext = ViewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.WhenActivated(disposables => CreateBindingsAndSubscribes(disposables));
        }

        protected virtual void CreateBindingsAndSubscribes(CompositeDisposable disposables)
        {
            this.disposables = disposables;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.disposables.Clear();
        }

        private TViewModel InstanceViewModel()
        {
            return (TViewModel)Activator.CreateInstance(typeof(TViewModel));
        }
    }
}
