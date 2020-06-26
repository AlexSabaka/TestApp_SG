using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ShoppingCartViewModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<IEnumerable<ProductItemViewModel>> _items;
        public IEnumerable<ProductItemViewModel> Items => _items.Value;


        private ObservableAsPropertyHelper<int> _total;
        public int Total => _total.Value;


        private ObservableAsPropertyHelper<int> _itemsCount;
        public int ItemsCount => _itemsCount.Value;


        public ReactiveCommand<ProductItemViewModel, Unit> AddItem { get; }

        public ReactiveCommand<ProductItemViewModel, Unit> DelItem { get; }

        public ShoppingCartViewModel()
        {
            var orderService = Locator.Current.GetService<IOrderService>();

            Observable
                .FromAsync(() => orderService.Cart("", 1))
                .Select(x => x.Select(m => new ProductItemViewModel(m)))
                .ToProperty(this, x => x.Items, out _items);

            this.WhenAnyValue(x => x.Items)
                .Select(x => x.Count())
                .ToProperty(this, x => x.ItemsCount, out _itemsCount);

            this.WhenAnyValue(x => x.Items)
                .Select(x => x.Sum(p => p.Price))
                .ToProperty(this, x => x.Total, out _total);

            AddItem = ReactiveCommand.CreateFromObservable<ProductItemViewModel, Unit>(p => p.BuyCommand.Execute());

            DelItem = ReactiveCommand.CreateFromObservable<ProductItemViewModel, Unit>(p => p.RemoveCommand.Execute());
        }
    }
}
