namespace XF4Controls.Models
{
    public enum MenuItemType
    {
        CheckBox,
        Carousel,
        RefreshView
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
