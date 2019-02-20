namespace XFRxUpdate.Models
{
    using ReactiveUI;
    using System;

    public class Plane : ReactiveObject, IEquatable<Plane>
    {
        private int id;
        private string name;

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

        public bool Equals(Plane other)
        {
            return this.name == other.name;
        }
    }
}
