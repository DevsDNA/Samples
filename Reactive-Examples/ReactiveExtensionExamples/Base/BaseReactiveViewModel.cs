namespace ReactiveExtensionExamples.Base
{
    using ReactiveUI;

    public class BaseReactiveViewModel : ReactiveObject, ISupportsActivation
    {
        public BaseReactiveViewModel()
        {
            InitializeCommands();
        }

        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();
        protected virtual void InitializeCommands() { }
        public ViewModelActivator Activator
        {
            get { return this.viewModelActivator; }
        }
    }
}
