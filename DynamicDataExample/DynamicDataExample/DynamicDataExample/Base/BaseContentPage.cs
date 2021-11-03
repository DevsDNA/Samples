namespace DynamicDataExample.Base
{
    using ReactiveUI;
    using ReactiveUI.XamForms;
    using System.Reactive.Disposables;
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.WhenActivated(d => ManageDisposables(d));
        }

        protected CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            FillViewModel();
            CreateBindings(disposables);
            ObserveValues(disposables);
            CreateEvents(disposables);
            return disposables;
        }

        protected virtual void FillViewModel()
        { }

        protected virtual void CreateBindings(CompositeDisposable disposables)
        { }

        protected virtual void ObserveValues(CompositeDisposable disposables)
        { }

        protected virtual void CreateEvents(CompositeDisposable disposables)
        { }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.OnAppearingAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.OnDisappearingAsync();
        }
    }
}
