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
    public partial class DataNeededView : ReactiveUserControl<DataNeededViewModel>
    {
        public DataNeededView()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.OneWayBind(ViewModel, x => x.Inputs, x => x.Layout.ItemsSource)
                    .DisposeWith(disps);

                this.OneWayBind(ViewModel, x => x.Title, x => x.TitleText.Text)
                    .DisposeWith(disps);
            });
        }
    }
}
