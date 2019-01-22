namespace XFRxUpdate.Models
{
    using ReactiveUI;

    public class Plane : ReactiveObject
    {
        private int id;
        private string name;
        private string surname;

        public int Id
        {
            get => this.id;
            set => this.RaiseAndSetIfChanged(ref this.id, value);
        }

        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }
    }
}
