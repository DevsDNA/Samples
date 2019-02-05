namespace TheAppOfTheDoctor.Features.Doctor
{
    using Microsoft.AppCenter.Analytics;
    using Xamarin.Forms;


    public class DoctorDescriptionViewModel : BindableObject
    {
        private readonly IDoctorApiProvider doctorApiProvider;
        private string textDescriptionDoctor;
        private bool isLoading;

        public DoctorDescriptionViewModel()
        {
            this.doctorApiProvider = DependencyService.Get<IDoctorApiProvider>();
            IsLoading = true;
        }




        public string TextDescriptionDoctor
        {
            get => this.textDescriptionDoctor;
            set
            {
                this.textDescriptionDoctor = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set
            {
                this.isLoading = value;
                OnPropertyChanged();
            }
        }


        public void OnAppearing()
        {
            SetTextAboutDoctor();
        }


        private async void SetTextAboutDoctor()
        {
            TextDescriptionDoctor = await this.doctorApiProvider.GetDescriptionFromServer();
            Analytics.TrackEvent("GotInformation");
            IsLoading = false;
        }
    }
}
