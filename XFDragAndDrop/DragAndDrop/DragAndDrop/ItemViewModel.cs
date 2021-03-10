namespace DragAndDrop
{
    using Xamarin.Forms;

    public class ItemViewModel : BindableObject
    {
        private bool isBeingDragged;
        private bool isBeingDraggedOver;
        private string category;
        private string title;

        public string Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(); }
        }

        public bool IsBeingDragged
        {
            get => isBeingDragged;
            set { isBeingDragged = value; OnPropertyChanged(); }
        }

        public bool IsBeingDraggedOver
        {
            get => isBeingDraggedOver;
            set { isBeingDraggedOver = value; OnPropertyChanged(); }
        }
    }
}