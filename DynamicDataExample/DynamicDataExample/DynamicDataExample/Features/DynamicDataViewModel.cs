namespace DynamicDataExample.Features
{
    using DynamicData;
    using DynamicData.Binding;
    using DynamicDataExample.Base;
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public class DynamicDataViewModel : BaseViewModel
    {
        private bool initiated;
        private bool isExecutingLoadMoreList;
        private SourceList<SourceItem> itemSourceList;
        private ReadOnlyObservableCollection<ItemViewModel> itemList;

        public DynamicDataViewModel()
        {
            itemSourceList = new SourceList<SourceItem>();
            isExecutingLoadMoreList = false;
            initiated = false;
            SortByList = new List<string> { "Name", "Age" };
            SortBy = "Name";
            SearchText = string.Empty;

            ConnectItemList();
        }


        [Reactive] public string SearchText { get; set; }
        [Reactive] public string SortBy { get; set; }
        [Reactive] public List<string> SortByList { get; private set; }

        public ReadOnlyObservableCollection<ItemViewModel> ItemList => itemList;

        public ReactiveCommand<Unit, Unit> LoadItemListCommand { get; private set; }
        public extern bool IsLoadingItemList { [ObservableAsProperty]get; }

        public ReactiveCommand<Unit, Unit> LoadMoreItemListCommand { get; private set; }
        public extern bool IsLoadingMoreItemList { [ObservableAsProperty]get; }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();

            disposables.Add(LoadItemListCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(LoadItemListCommand.IsExecuting.ToPropertyEx(this, x => x.IsLoadingItemList));

            disposables.Add(LoadMoreItemListCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(LoadMoreItemListCommand.IsExecuting.ToPropertyEx(this, x => x.IsLoadingMoreItemList));

            if (!initiated)
            {
                initiated = true;
                await LoadItemListCommand.Execute();
            }
        }

        protected override void CreateCommands()
        {
            base.CreateCommands();

            LoadItemListCommand = ReactiveCommand.CreateFromTask(LoadItemListAsync);
            LoadMoreItemListCommand = ReactiveCommand.CreateFromTask(LoadMoreItemListAsync);
        }

        private void ConnectItemList()
        {
            IObservable<Func<ItemViewModel, bool>> filter = this.WhenAnyValue(x => x.SearchText)
                .Select(BuildFilter);

            IObservable<SortExpressionComparer<ItemViewModel>> sort = this.WhenAnyValue(x => x.SortBy)
                .Select(BuildSort);

            itemSourceList.Connect()
                .Transform(TransformItem)
                .Filter(filter)
                .Sort(sort)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out itemList)
                .DisposeMany()
                .Subscribe();
        }

        private SortExpressionComparer<ItemViewModel> BuildSort(string sortBy)
        {
            switch (sortBy.ToLowerInvariant())
            {
                case "age":
                    return SortExpressionComparer<ItemViewModel>.Ascending(x => x.Age);
                case "name":
                default:
                    return SortExpressionComparer<ItemViewModel>.Ascending(x => x.Name);
            }
        }

        private Func<ItemViewModel, bool> BuildFilter(string filter)
        {
            return item =>
            {
                return item.Name.ToLowerInvariant().Contains(filter.ToLowerInvariant()) ||
                       item.Age.ToString().ToLowerInvariant().Contains(filter.ToLowerInvariant());
            };
        }

        private ItemViewModel TransformItem(SourceItem item)
        {
            return new ItemViewModel
            {
                Name = $"{item.Name} {item.SurName}",
                Age = GetYears(item.Birthdate)
            };
        }

        private int GetYears(DateTime date)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - date;
            return (zeroTime + span).Year - 1;
        }

        private Task LoadItemListAsync()
        {
            var random = new Random();
            itemSourceList.Clear();
            List<SourceItem> tmp = new List<SourceItem>();
            for (int i = 0; i < 100; i++)
            {
                tmp.Add(new SourceItem
                {
                    Name = $"Name {GetLastChars(i)}",
                    SurName = $"Surname {GetLastChars(i)}",
                    Birthdate = new DateTime(random.Next(1950, 2020), random.Next(1, 12), random.Next(1, 28))
                });
            }

            itemSourceList.AddRange(tmp);
            return Task.CompletedTask;
        }

        private string GetLastChars(int value)
        {
            string formatedValue = "000" + value.ToString();
            return formatedValue.Substring(formatedValue.Length - 3);
        }

        
        private Task LoadMoreItemListAsync()
        {
            if (itemSourceList.Count > 300)
                return Task.CompletedTask;

            if (isExecutingLoadMoreList)
                return Task.CompletedTask;

            try
            {
                isExecutingLoadMoreList = true;
                var random = new Random();
                int startIndex = itemSourceList.Count;
                int endIndex = itemSourceList.Count + 100;
                List<SourceItem> tmp = new List<SourceItem>();
                for (int i = startIndex; i < endIndex; i++)
                {
                    tmp.Add(new SourceItem
                    {
                        Name = $"Name {GetLastChars(i)}",
                        SurName = $"Surname {GetLastChars(i)}",
                        Birthdate = new DateTime(random.Next(1950, 2020), random.Next(1, 12), random.Next(1, 28))
                    });
                }

                itemSourceList.AddRange(tmp);
                return Task.CompletedTask;

            }
            finally { isExecutingLoadMoreList = false; }
        }

        private void LogError(Exception ex)
        {
            Debug.WriteLine($"Error {ex?.Message}");
        }
    }
}
