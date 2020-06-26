using ReactiveUI;
using System.Reactive;

namespace AppClient
{
    public class TransactionViewModel : ReactiveObject
    {
        private ShoppingCartViewModel _shoppingCart;
        public ShoppingCartViewModel ShoppingCart
        {
            get => _shoppingCart;
            set => this.RaiseAndSetIfChanged(ref _shoppingCart, value);
        }

        public ReactiveCommand<ProductItemViewModel, Unit> AddItem => ShoppingCart.AddItem;

        public TransactionViewModel()
        {
            ShoppingCart = new ShoppingCartViewModel();
        }
    }
}