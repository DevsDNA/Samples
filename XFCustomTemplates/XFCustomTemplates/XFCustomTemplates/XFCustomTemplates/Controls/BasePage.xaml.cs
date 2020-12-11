namespace XFCustomTemplates.Controls
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        public static readonly BindableProperty ToolBarColorProperty = BindableProperty.Create(nameof(ToolBarColor), typeof(Color), typeof(BasePage), Color.Blue);

        private ControlTemplate topToolBarTemplate;
        private ControlTemplate bottomToolBarTemplate;
        private bool topToolBar = true;        

        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            topToolBarTemplate = (ControlTemplate)Resources["TopToolBarTemplate"];
            bottomToolBarTemplate = (ControlTemplate)Resources["BottomToolBarTemplate"];
        }

        public Color ToolBarColor
        {
            get => (Color)GetValue(ToolBarColorProperty);
            set => SetValue(ToolBarColorProperty, value);
        }

        private void ChangeButton_Clicked(object sender, System.EventArgs e)
        {
            topToolBar = !topToolBar;
            ControlTemplate = (topToolBar) ? topToolBarTemplate : bottomToolBarTemplate;
        }

        private async void BackButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}