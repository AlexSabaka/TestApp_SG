﻿using System;
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

        private IObservable<ProductItem> _items;
        public IObservable<ProductItem> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public ItemsViewModel()
        {
            _itemService = Locator.Current.GetService<IItemsService>();

            UpdateCommand = ReactiveCommand.Create(() =>
            {
                try
                {
                    var items = _itemService.Get();
                    items.Wait();

                    Items = items.Result.ToObservable();
                }
                catch (Exception ex)
                {
                    //todo handle exception and display message
                }
            });
        }
    }
}