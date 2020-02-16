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
using ReactiveUI;

namespace AppClient
{
    /// <summary>
    /// Interaction logic for ProductItemView.xaml
    /// </summary>
    public partial class ProductItemView : ReactiveUserControl<ProductItemViewModel>
    {
        public ProductItemView()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.BindCommand(ViewModel, x => x.AddToCartCommand, x => x.AddToCartButton).DisposeWith(disps);
                this.BindCommand(ViewModel, x => x.RemoveFromCartCommand, x => x.RemoveFromCartButton).DisposeWith(disps);
            });
        }
    }
}
