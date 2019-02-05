namespace XFRxUpdate.Features.Main
{
    using DynamicData;
    using DynamicData.Binding;
    using DynamicData.PLinq;
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
        private readonly ReactiveCommand<Unit, Unit> refreshCommand;
        private string searchText;
        private bool sortDescending;

        public MainViewModel()
        {
            this.refreshCommand = ReactiveCommand.Create(PerformRefresh);

            IObservable<Func<Plane, bool>> filter = this.WhenAnyValue(vm => vm.SearhText)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(BuildFilter);

            IObservable<SortExpressionComparer<Plane>> sort = this.WhenAnyValue(vm => vm.SortDescending)
                .Select(sd => 
                {
                    if (sd)
                        return SortExpressionComparer<Plane>.Descending(l => l.Name);
                    return SortExpressionComparer<Plane>.Ascending(l => l.Name);
                });

            this.planes.Connect()
                .Filter(filter)
                .Sort(sort)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.bindingData)
                .DisposeMany()
                .Subscribe();
        }

        public SourceList<Plane> Planes
        {
            get => this.planes;
            set => this.RaiseAndSetIfChanged(ref this.planes, value);
        }

        public bool SortDescending
        {
            get => this.sortDescending;
            set => this.RaiseAndSetIfChanged(ref this.sortDescending, value);
        }

        public ReadOnlyObservableCollection<Plane> BindingData
        {
            get => this.bindingData;
            set => this.RaiseAndSetIfChanged(ref this.bindingData, value);
        }

        public string SearhText
        {
            get => this.searchText;
            set => this.RaiseAndSetIfChanged(ref this.searchText, value);
        }

        public ReactiveCommand<Unit, Unit> RefreshCommand => this.refreshCommand;


        private void PerformRefresh()
        {
            Plane plane = new Plane
            {
                Id = 1,
                Name = "Bronco"
            };
            Planes.Add(plane);
            plane = new Plane
            {
                Id = 2,
                Name = "Mustang"
            };
            Planes.Add(plane);
            plane = new Plane
            {
                Id = 3,
                Name = "SpitFire"
            };
            Planes.Add(plane);
            plane = new Plane
            {
                Id = 3,
                Name = "Messerschmitt"
            };
            Planes.Add(plane);
        }

        private static Func<Plane, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _ => true;
            return t => t.Name.ToLower().Contains(searchText.ToLower());
        }
    }
}
