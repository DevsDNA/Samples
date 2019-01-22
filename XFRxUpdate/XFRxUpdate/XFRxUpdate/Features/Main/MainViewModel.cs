namespace XFRxUpdate.Features.Main
{
    using DynamicData;
    using ReactiveUI;
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive;
    using System.Reactive.Linq;
    using XFRxUpdate.Models;

    public class MainViewModel : ReactiveObject
    {
        private ReadOnlyObservableCollection<Plane> bindingData;
        private SourceList<Plane> planes = new SourceList<Plane>();
        private ReactiveCommand<Unit, Unit> addnewCommand;

        public MainViewModel()
        {
            this.addnewCommand = ReactiveCommand.Create(PerformAdd);
            System.IObservable<IChangeSet<Plane>> planesConnection = this.planes.Connect();

            IDisposable myBindingOperation = planesConnection
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.bindingData)
                .Subscribe(cs =>
                {
                    this.RaisePropertyChanging(nameof(BindingData));
                });
        }

        public SourceList<Plane> Planes
        {
            get => this.planes;
            set => this.RaiseAndSetIfChanged(ref this.planes, value);
        }

        public ReadOnlyObservableCollection<Plane> BindingData
        {
            get => this.bindingData;
            set => this.RaiseAndSetIfChanged(ref this.bindingData, value);
        }

        public ReactiveCommand<Unit, Unit> AddNewCommand => this.addnewCommand;


        private void PerformAdd()
        {
            if (Planes.Count == 0)
            {
                Plane plane = new Plane
                {
                    Id = 1,
                    Name = "Bronco"
                };
                Planes.Add(plane);
            }
            else if (Planes.Count == 1)
            {
                Plane plane = new Plane
                {
                    Id = 2,
                    Name = "Mustang"
                };
                Planes.Add(plane);
            }
            else if (Planes.Count == 2)
            {
                Plane plane = new Plane
                {
                    Id = 3,
                    Name = "SpitFire"
                };
                Planes.Add(plane);
            }
        }
    }
}
