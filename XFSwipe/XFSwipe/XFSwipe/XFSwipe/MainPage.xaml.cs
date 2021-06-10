namespace XFSwipe
{
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            deleteSwipe.Command = new Command(() =>
            {
                DisplayAlert("borrar", "¿Está seguro de borrar este elemento?", "aceptar", "cancelar");
            });
            editSwipe.Command = new Command(() =>
            {
                lblText.IsVisible = !lblText.IsVisible;
                txtEdit.IsVisible = !txtEdit.IsVisible;
                editSwipe.Text = txtEdit.IsVisible ? "finalizar" : "editar";
            });
            favouriteSwipe.Command = new Command(() =>
            {
                shapeFavourite.IsVisible = !shapeFavourite.IsVisible;
                favouriteSwipe.Text = shapeFavourite.IsVisible ? "desmarcar favorito" : "marcar favorito";
            });
        }
    }
}
