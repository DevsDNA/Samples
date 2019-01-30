namespace TheAppOfTheDoctor.Features.Doctor
{
    using Xamarin.Forms;


    public partial class DoctorDescriptionView : ContentPage
    {
        public DoctorDescriptionView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as DoctorDescriptionViewModel)?.OnAppearing();
        }
    }
}