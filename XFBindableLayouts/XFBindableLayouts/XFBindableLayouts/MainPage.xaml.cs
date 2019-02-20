using System.Collections.Generic;
using Xamarin.Forms;

namespace XFBindableLayouts
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Users = new List<UserModel>();
            for (int i = 0; i < 15; i++)
            {
                Users.Add(new UserModel() { Name = $"User #{i}" });
            }

            BindingContext = this;
        }

        public List<UserModel> Users
        {
            get;
            set;
        }
    }

    public class UserModel
    {
        public string Name { get; set; }
    }
}
