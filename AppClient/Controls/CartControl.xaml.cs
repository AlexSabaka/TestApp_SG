using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppClient.Controls
{
    /// <summary>
    /// Interaction logic for CartControl.xaml
    /// </summary>
    public partial class CartControl : ReactiveUserControl<ShoppingCartViewModel>
    {
        public CartControl()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.OneWayBind(ViewModel, x => x.Total, x => x.Total.Text)
                    .DisposeWith(disps);

                this.OneWayBind(ViewModel, x => x.Items, x => x.CartItems.ItemsSource)
                    .DisposeWith(disps);
            });
        }
    }
}
