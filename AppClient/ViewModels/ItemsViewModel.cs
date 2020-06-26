using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;
using ReactiveUI;
using Splat;

namespace AppClient
{
    public class ItemsViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IItemsService _itemService;

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private ReactiveCommand<Unit, Unit> _updateCommand;
        public ReactiveCommand<Unit, Unit> UpdateCommand
        {
            get => _updateCommand;
            set => this.RaiseAndSetIfChanged(ref _updateCommand, value);
        }

        private IEnumerable<ProductItem> _items;
        public IEnumerable<ProductItem> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public ItemsViewModel(IScreen screen)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            _itemService = Locator.Current.GetService<IItemsService>();

        }
    }
}
