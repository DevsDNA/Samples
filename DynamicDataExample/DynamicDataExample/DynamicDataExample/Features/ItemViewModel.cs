namespace DynamicDataExample.Features
{
    using DynamicDataExample.Base;
    using ReactiveUI.Fody.Helpers;

    public class ItemViewModel : BaseViewModel
    {

        [Reactive] public string Name { get; set; }
        [Reactive] public int Age { get; set; }
    }
}
