
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAccessibility.Services;

namespace XamarinAccessibility
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccessibleDragAndDrop : ContentPage
    {
        private readonly IAccessibilityService accessibilityService;

        public AccessibleDragAndDrop()
        {
            accessibilityService = DependencyService.Get<IAccessibilityService>();
            InitializeComponent();
        }

        private void OnCartDrop(object sender, DropEventArgs e)
        {
            accessibilityService.PlayAudio("Mono añadido al carrito");
        }

        private void OnWishListDrop(object sender, DropEventArgs e)
        {
            accessibilityService.PlayAudio("Mono añadido a la lista de deseos");
        }

        private void btAddToCart_Clicked(object sender, System.EventArgs e)
        {
            imCart.Source = ImageSource.FromFile("monkey.png");
            AutomationProperties.SetHelpText(imCart, "Carrito con un elemento");
            accessibilityService.PlayAudio("Mono añadido al carrito");
        }

        private void btAddToWishList_Clicked(object sender, System.EventArgs e)
        {
            imWishList.Source = ImageSource.FromFile("monkey.png");
            AutomationProperties.SetHelpText(imCart, "Lista de deseos con un elemento");
            accessibilityService.PlayAudio("Mono añadido a la lista de deseos");
        }
    }
}