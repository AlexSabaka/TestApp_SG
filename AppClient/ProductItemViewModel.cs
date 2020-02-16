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
        private readonly IOrderService _orderService;

        private ProductItem _item;

        private CurrentUserInfo _currentUser;

        private ReactiveCommand<Unit, Unit> _addToCartCommand;
        public ReactiveCommand<Unit, Unit> AddToCartCommand
        {
            get => _addToCartCommand;
            set => this.RaiseAndSetIfChanged(ref _addToCartCommand, value);
        }

        private ReactiveCommand<Unit, Unit> _removeFromCartCommand;
        public ReactiveCommand<Unit, Unit> RemoveFromCartCommand
        {
            get => _removeFromCartCommand;
            set => this.RaiseAndSetIfChanged(ref _removeFromCartCommand, value);
        }

        public string Name => _item.Name;

        public string Description => _item.Description;

        public decimal Price => _item.Price;
        
        public bool Available => _item.Available;


        public ProductItemViewModel(ProductItem item)
        {
            _item = item;
            _currentUser = Locator.Current.GetService<CurrentUserInfo>();
           _orderService = Locator.Current.GetService<IOrderService>();

            AddToCartCommand = ReactiveCommand.Create(() => {
                try
                {
                    _orderService.PutProductToCart(_currentUser.Token, _item.Id);
                }
                catch (Exception ex)
                {
                    //todo
                }
            },
            this.WhenAnyValue(x => x._currentUser.IsLogin));

            RemoveFromCartCommand = ReactiveCommand.Create(() => {
                try
                {
                    _orderService.RemoveProductFromCart(_currentUser.Token, _item.Id);
                }
                catch (Exception ex)
                {
                    //todo
                }
            },
            this.WhenAnyValue(x => x._currentUser.IsLogin));
        }
    }
}
