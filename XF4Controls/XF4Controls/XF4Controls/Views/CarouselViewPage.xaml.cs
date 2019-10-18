using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XF4Controls.Views
{
    public partial class CarouselViewPage : ContentPage
    {
        public CarouselViewPage()
        {
            InitializeComponent();
            BindingContext = new CarouselViewPageViewModel();
        }
    }

    public class CarouselViewPageViewModel : BindableObject
    {
        private ObservableCollection<string> items;

        public CarouselViewPageViewModel()
        {
            LoadItems();
        }

        public ObservableCollection<string> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        private void LoadItems()
        {
            Items = new ObservableCollection<string>();
            for (int i = 1; i <= 3; i++)
                Items.Add($"Item {i}");
        }
    }
}