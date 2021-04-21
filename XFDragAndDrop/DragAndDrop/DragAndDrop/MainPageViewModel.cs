namespace DragAndDrop
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public class MainPageViewModel : BindableObject
    {
        private ObservableCollection<ItemViewModel> items;
        private ObservableCollection<ItemsGroupViewModel> groupedItems;

        public MainPageViewModel()
        {
            groupedItems = new ObservableCollection<ItemsGroupViewModel>();
            items = new ObservableCollection<ItemViewModel>();

            ItemDraggedCommand = new Command<ItemViewModel>(OnItemDragged);
            ItemDraggedOverCommand = new Command<ItemViewModel>(OnItemDraggedOver);
            ItemDragLeaveCommand = new Command<ItemViewModel>(OnItemDragLeave);
            ItemDroppedCommand = new Command<ItemViewModel>(i => OnItemDropped(i));

            ResetItemsState();
        }



        public ObservableCollection<ItemViewModel> Items
        {
            get { return items; }
            set { items= value; OnPropertyChanged(); }
        }

        public ObservableCollection<ItemsGroupViewModel> GroupedItems
        {
            get { return groupedItems; }
            set { groupedItems= value; OnPropertyChanged(); }
        }

        public ICommand ItemDraggedCommand { get; }

        public ICommand ItemDraggedOverCommand { get; }

        public ICommand ItemDragLeaveCommand { get; }

        public ICommand ItemDroppedCommand { get; }

        // Start dragging.
        private void OnItemDragged(ItemViewModel item)
        {
            Debug.WriteLine($"OnItemDragged: {item?.Title}");
            Items.ForEach(i => i.IsBeingDragged = item == i);
        }

        // Stop dragging over other element.
        private void OnItemDragLeave(ItemViewModel item)
        {
            Debug.WriteLine($"OnItemDragLeave: {item?.Title}");
            Items.ForEach(i => i.IsBeingDraggedOver = false);
        }

        // Drag over element.
        private void OnItemDraggedOver(ItemViewModel item)
        {
            Debug.WriteLine($"OnItemDraggedOver: {item?.Title}");
            ItemViewModel itemBeingDragged = items.FirstOrDefault(i => i.IsBeingDragged);
            Items.ForEach(i => i.IsBeingDraggedOver = item == i && item != itemBeingDragged);
        }

        // Stop dragging, param is item over dragging.
        private void OnItemDropped(ItemViewModel item)
        {
            ItemViewModel itemToMove = items.First(i => i.IsBeingDragged);
            ItemViewModel itemToInsertBefore = item;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            ItemsGroupViewModel categoryToMoveFrom = GroupedItems.First(g => g.Contains(itemToMove));
            categoryToMoveFrom.Remove(itemToMove);

            // Wait for remove animation to be completed
            // https://github.com/xamarin/Xamarin.Forms/issues/13791
            // await Task.Delay(1000);

            ItemsGroupViewModel categoryToMoveTo = GroupedItems.First(g => g.Contains(itemToInsertBefore));
            int insertAtIndex = categoryToMoveTo.IndexOf(itemToInsertBefore);
            itemToMove.Category = categoryToMoveFrom.Name;
            categoryToMoveTo.Insert(insertAtIndex, itemToMove);
            itemToMove.IsBeingDragged = false;
            itemToInsertBefore.IsBeingDraggedOver = false;
            Debug.WriteLine($"OnItemDropped: [{itemToMove?.Title}] => [{itemToInsertBefore?.Title}], target index = [{insertAtIndex}]");
            
            PrintItemsState();
        }

        private void ResetItemsState()
        {
            Items.Clear();
            Items.Add(new ItemViewModel { Category = "Category 1", Title = "Item 1" });
            Items.Add(new ItemViewModel { Category = "Category 1", Title = "Item 2" });
            Items.Add(new ItemViewModel { Category = "Category 2", Title = "Item 3" });
            Items.Add(new ItemViewModel { Category = "Category 2", Title = "Item 4" });
            Items.Add(new ItemViewModel { Category = "Category 2", Title = "Item 5" });
            Items.Add(new ItemViewModel { Category = "Category 2", Title = "Item 6" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 7" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 8" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 9" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 10" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 11" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 12" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 13" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 14" });
            Items.Add(new ItemViewModel { Category = "Category 3", Title = "Item 15" });

            GroupedItems = new ObservableCollection<ItemsGroupViewModel>(Items.GroupBy(i => i.Category).Select(g => new ItemsGroupViewModel(g.Key, g)));
        }

        private void PrintItemsState()
        {
            Debug.WriteLine($"Items {Items.Count}, state:");
            for (int i = 0; i < Items.Count; i++)
            {
                Debug.WriteLine($"\t{i}: Group: {Items[i].Category} | Item: {Items[i].Title}");
            }
        }
    }
}