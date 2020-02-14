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
    public partial class AuthView : ReactiveUserControl<AuthViewModel>
    {
        public AuthView()
        {
            InitializeComponent();

            this.WhenActivated(disps =>
            {
                this.Bind(ViewModel, x => x.ErrorDescription, x => x.ErrorTextBlock.Text).DisposeWith(disps);

                this.Bind(ViewModel, x => x.UserName, x => x.UserNameBox.Text).DisposeWith(disps);
                this.Bind(ViewModel, x => x.Password, x => x.PasswordBox.Text).DisposeWith(disps);

                this.BindCommand(ViewModel, x => x.AuthCommand, x => x.LogInButton).DisposeWith(disps);
                this.BindCommand(ViewModel, x => x.RegisterCommand, x => x.RegisterButton).DisposeWith(disps);
            });
        }
    }
}
