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
    public partial class AssistanceView : ReactiveUserControl<AssistanceViewModel>
    {
        public AssistanceView()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.OneWayBind(ViewModel, x => x.AssistanceFunctions, x => x.AssistanceFunctions.ItemsSource)
                    .DisposeWith(disps);
            });
        }
    }
}
