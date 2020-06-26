using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;
using ReactiveUI;
using Splat;

namespace AppClient
{
    public class PickListViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IItemsService _itemService;

        public string UrlPathSegment { get; }

        public IScreen HostScreen { get; }


        private string _searchPhrase;
        public string SearchPhrase
        {
            get => _searchPhrase;
            set => this.RaiseAndSetIfChanged(ref _searchPhrase, value);
        }


        private readonly ObservableAsPropertyHelper<IEnumerable<ProductItem>> _quickPickItems;
        public IEnumerable<ProductItem> QuickPickItems => _quickPickItems.Value;


        public PickListViewModel(IScreen screen)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            _itemService = Locator.Current.GetService<IItemsService>();

            _quickPickItems = this
                .WhenAnyValue(x => x.SearchPhrase)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                //.Where(term => !string.IsNullOrWhiteSpace(term))
                .SelectMany(UpdateQuickPickItems)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.QuickPickItems);


            _quickPickItems.ThrownExceptions.Subscribe(error => { /* Handle errors here */ });


            //_isAvailable = this
            //    .WhenAnyValue(x => x.QuickPickItems)
            //    .Select(searchResults => searchResults != null)
            //    .ToProperty(this, x => x.IsAvailable);
        }


        private async Task<IEnumerable<ProductItem>> UpdateQuickPickItems(string term, CancellationToken token)
        {
            var all = await _itemService.Get();

            return await Task.FromResult(all.Where(pi => pi.Name.Contains(term)).ToList());
        }
    }
}
