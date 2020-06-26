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
    public partial class ScanAndBagView : ReactiveUserControl<ScanAndBagViewModel>
    {
        public ScanAndBagView()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.BindCommand(ViewModel, x => x.PickListCommand, x => x.PickListButton).DisposeWith(disps);
            });
        }
    }
}
