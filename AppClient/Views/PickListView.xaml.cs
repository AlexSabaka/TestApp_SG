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

namespace AppClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PickListView : ReactiveUserControl<PickListViewModel>
    {
        public PickListView()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.OneWayBind(ViewModel, x => x.QuickPickItems, x => x.QuickPickList.ItemsSource)
                    .DisposeWith(disps);

                this.Bind(ViewModel, x => x.SearchPhrase, x => x.SearchBox.Text)
                    .DisposeWith(disps);
            });
        }
    }
}
