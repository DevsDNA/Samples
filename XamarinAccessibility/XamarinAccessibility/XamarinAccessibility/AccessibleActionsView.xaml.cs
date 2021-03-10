using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAccessibility.Services;

namespace XamarinAccessibility
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccessibleActionsView : ContentPage
    {
        private readonly IAccessibilityService accessibilityService;

        public AccessibleActionsView()
        {
            accessibilityService = DependencyService.Get<IAccessibilityService>();
            InitializeComponent();
        }

        private async void BtnUndefinedAction_Clicked(object sender, EventArgs e)
        {
            accessibilityService.PlayAudio("Iniciando proceso");
            BtnUndefinedAction.IsVisible = false;
            AiUndefinedAction.IsRunning = true;
            AiUndefinedAction.IsVisible = true;
            
            await Task.Delay(3000);
            accessibilityService.PlayAudio("Proceso finalizado");

            BtnUndefinedAction.IsVisible = true;
            AiUndefinedAction.IsRunning = false;
            AiUndefinedAction.IsVisible = false;
        }

        private async void BtnDefinedAction_Clicked(object sender, EventArgs e)
        {
            accessibilityService.PlayAudio("Iniciando proceso");
            BtnDefinedAction.IsVisible = false;
            PbDefinedAction.IsVisible = true;
            PbDefinedAction.Progress = 0;
            await Task.Delay(3000);

            for (int i = 0; i < 5; i++)
            {
                double progress = 0.25 * i;
                PbDefinedAction.Progress = progress;
                accessibilityService.PlayAudio($"{progress * 100} porciento completado");
                await Task.Delay(3000);
            }
            
            accessibilityService.PlayAudio("Proceso finalizado");
            
            BtnDefinedAction.IsVisible = true;
            PbDefinedAction.IsVisible = false;
            PbDefinedAction.Progress = 0;
        }
    }
}