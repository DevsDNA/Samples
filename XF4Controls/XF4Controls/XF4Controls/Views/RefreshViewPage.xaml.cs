using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XF4Controls.Views
{
    public partial class RefreshViewPage : ContentPage
    {
        public RefreshViewPage()
        {
            InitializeComponent();
            BindingContext = new RefreshViewPageViewModel();
        }
    }

    public class RefreshViewPageViewModel : BindableObject
    {
        private bool isRefreshing;
        private ObservableCollection<string> items;
        private int index = 3;

        public RefreshViewPageViewModel()
        {
            isRefreshing = false;
            items = new ObservableCollection<string>() { "Item 1", "Item 2" };
            RefreshCommand = new Command(Refresh);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
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

        public ICommand RefreshCommand { get; set; }

        private async void Refresh(object obj)
        {
            IsRefreshing = true;
            await Task.Delay(2000);

            Items.Add($"Item {index}");
            index++;
            Items.Add($"Item {index}");
            index++;
            IsRefreshing = false;
        }
    }
}