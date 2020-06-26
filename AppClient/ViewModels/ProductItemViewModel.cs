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
    public class ProductItemViewModel : ReactiveObject
    {
        private ProductItem _item;

        private ReactiveCommand<Unit, Unit> _removeFromCartCommand;
        public ReactiveCommand<Unit, Unit> RemoveCommand
        {
            get => _removeFromCartCommand;
            set => this.RaiseAndSetIfChanged(ref _removeFromCartCommand, value);
        }

        private ReactiveCommand<Unit, Unit> _buyCommand;
        public ReactiveCommand<Unit, Unit> BuyCommand
        {
            get => _buyCommand;
            set => this.RaiseAndSetIfChanged(ref _buyCommand, value);
        }

        public string Name => _item.Name;

        public string Description => _item.Description;

        public int Price => _item.Price;


        public ProductItemViewModel(ProductItem item)
        {
            _item = item;

            var orderService = Locator.Current.GetService<IOrderService>();

            RemoveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await orderService.RemoveProductFromCart("", _item.Id);
            });

            BuyCommand = ReactiveCommand.CreateFromTask(async () =>
            {
               await orderService.PutProductToCart("", _item.Id);
            });

            //AddToCartCommand = ReactiveCommand.CreateFrom
        }
    }
}
