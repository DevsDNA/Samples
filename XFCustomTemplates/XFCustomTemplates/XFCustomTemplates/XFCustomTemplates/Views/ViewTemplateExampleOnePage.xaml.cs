namespace XFCustomTemplates.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTemplateExampleOnePage : ContentPage
    {
        private string infoTitle;
        private string infoDescription;

        public ViewTemplateExampleOnePage()
        {
            InitializeComponent();

            infoTitle = "Jorge";
            infoDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla elit dolor, convallis non interdum.";

            BindingContext = this;
        }

        public string InfoTitle
        {
            get => infoTitle;
            set
            {
                infoTitle = value;
                OnPropertyChanged();
            }
        }

        public string InfoDescription
        {
            get => infoDescription;
            set
            {
                infoDescription = value;
                OnPropertyChanged();
            }
        }
    }
}