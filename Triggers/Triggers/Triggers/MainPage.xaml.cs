namespace Triggers
{
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private string name;
        private string surname;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            Name = string.Empty;
            Surname = string.Empty;

            InitializeComponent();
            BindingContext = this;
        }
    }
}
