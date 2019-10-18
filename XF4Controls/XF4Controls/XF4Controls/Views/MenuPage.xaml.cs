using System.Collections.Generic;
using Xamarin.Forms;
using XF4Controls.Models;

namespace XF4Controls.Views
{
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.CheckBox, Title="CheckBox" },
                new HomeMenuItem {Id = MenuItemType.Carousel, Title="Carousel" },
                new HomeMenuItem {Id = MenuItemType.RefreshView, Title="RefreshView" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}