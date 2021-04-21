using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XFMacOS
{
    public partial class CollectionViewPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public CollectionViewPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyCollectionView.ItemsSource = Items;
        }

    }
}
