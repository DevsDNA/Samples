namespace Triggers
{
    using Xamarin.Forms;

    public class ExpandButtonTriggerAction : TriggerAction<Button>
    {
        protected override async void Invoke(Button sender)
        {
            await sender.ScaleTo(0.95, 50, Easing.CubicOut);
            await sender.ScaleTo(1, 50, Easing.CubicIn);
        }
    }
}
