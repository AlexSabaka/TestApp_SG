using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
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

using Splat;

namespace AppClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();

            this.WhenActivated(disps =>
            {
                this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedViewHost.Router)
                    .DisposeWith(disps);

                this.OneWayBind(ViewModel, x => x.TransactionEmpty, x => x.ShoppingCart.Visibility)
                    .DisposeWith(disps);

                //this.OneWayBind(ViewModel, x => x.ShoppingCartVisibility, x => x.ShoppingCart.Visibility)
                //    .DisposeWith(disps);

                this.OneWayBind(ViewModel, x => x.Transaction.ShoppingCart, x => x.ShoppingCart.ViewModel)
                    .DisposeWith(disps);

                //this.OneWayBind(ViewModel, x => x.Language, x => x.CurrentLanguage.Text)
                //    .DisposeWith(disps);

                this.OneWayBind(ViewModel, x => x.SystemFunctions, x => x.SystemFunctions.ItemsSource)
                    .DisposeWith(disps);

                //this.BindCommand(ViewModel, x => x.ChangeLanguageCommand, x => x.ChangeLanguageButton)
                //    .DisposeWith(disps);

                //this.BindCommand(ViewModel, x => x.AssistanceCommand, x => x.AssistanceButton)
                //    .DisposeWith(disps);

                //this.BindCommand(ViewModel, x => x.GoBackCommand, x => x.GoBackButton)
                //    .DisposeWith(disps);
            });
        }
    }
}
