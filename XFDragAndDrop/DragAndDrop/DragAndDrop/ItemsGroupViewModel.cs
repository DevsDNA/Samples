namespace DragAndDrop
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ItemsGroupViewModel : ObservableCollection<ItemViewModel>
    {

        public ItemsGroupViewModel(string name, IEnumerable<ItemViewModel> items) : base(items)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}