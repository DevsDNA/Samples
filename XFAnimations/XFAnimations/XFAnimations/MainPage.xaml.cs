namespace XFAnimations
{
    using System;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void StopAnimaton_Clicked(object sender, EventArgs e)
        {
            this.AbortAnimation("animations");
        }

        private async void StartAnimaton_Clicked(object sender, EventArgs e)
        {
            //ViewExtensions
            Animation parentAnimation = new Animation();

            Animation rotateLoginButton = new Animation(v => LoginButton.Rotation = v, LoginButton.Rotation, 180, Easing.CubicInOut);
            Animation opacityLoginButton = new Animation(v => LoginButton.Opacity = v, LoginButton.Opacity,0, Easing.CubicInOut);
            Animation translateLoginButton = new Animation(v => LoginButton.TranslationY = v, LoginButton.TranslationY, 200, Easing.CubicInOut);

            Animation translateEntryMail= new Animation(v => EntryEmail.TranslationY = v, EntryEmail.TranslationY, 200, Easing.CubicInOut);
            Animation translateEntryPass = new Animation(v => EntryPassword.TranslationY = v, EntryPassword.TranslationY, 200, Easing.CubicInOut);


            parentAnimation.Add(0, 1, rotateLoginButton);
            parentAnimation.Add(0.5, 1, opacityLoginButton);
            parentAnimation.Add(0, 1, translateLoginButton);

            parentAnimation.Add(0, 1, translateEntryMail);
            parentAnimation.Add(0, 1, translateEntryPass);

            parentAnimation.Commit(this, "animations", 16, 2000, Easing.CubicInOut, (v, b) => { }, () => false);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            LoginButton.Rotation = 0;
            LoginButton.TranslationY = 0;
            LoginButton.Opacity = 1;
            EntryEmail.TranslationY = 0;
            EntryPassword.TranslationY = 0;
        }
    }
}
