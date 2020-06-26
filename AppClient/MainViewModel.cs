using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;

using ReactiveUI;

using Splat;

namespace AppClient
{

    public class MainViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }


        private ObservableAsPropertyHelper<bool> _transactionEmpty;
        public bool TransactionEmpty => _transactionEmpty.Value;


        private TransactionViewModel _transaction;
        public TransactionViewModel Transaction
        {
            get => _transaction;
            set => this.RaiseAndSetIfChanged(ref _transaction, value);
        }


        private ObservableCollection<ActionCommandViewModel> _systemFuctions;
        public ObservableCollection<ActionCommandViewModel> SystemFunctions
        {
            get => _systemFuctions;
            set => this.RaiseAndSetIfChanged(ref _systemFuctions, value);
        }


        private string _language = "UA";
        public string Language
        {
            get => _language;
            set => this.RaiseAndSetIfChanged(ref _language, value);
        }


        private bool _assistanceLoged = false;
        public bool AssistanceLoged
        {
            get => _assistanceLoged;
            set => this.RaiseAndSetIfChanged(ref _assistanceLoged, value);
        }


        public MainViewModel()
        {
            Router = new RoutingStateWithMiddleware(OnRouting);

            SystemFunctions = new ObservableCollection<ActionCommandViewModel>
            {
                new ActionCommandViewModel(name: "Language", action: ChangeLanguage),
                new ActionCommandViewModel(name: "Assistance", action: () => this.NavigateToView(new AuthViewModel(this, new AssistanceViewModel(this)))),
                new ActionCommandViewModel(name: "Go Back", action: Router.NavigateBack),
            };

            this.WhenAnyValue(x => x.Transaction)
                .Select(x => x != null)
                .ToProperty(this, x => x.TransactionEmpty, out _transactionEmpty);

            Router.Navigate.Execute(new WelcomeViewModel(this));
        }


        private IRoutableViewModel OnRouting(IRoutableViewModel vm)
        {
            switch (vm)
            {
                default:
                    return vm;
            }
        }

        private void ChangeLanguage()
        {
            if (Language == "UA") Language = "EN";
            else Language = "UA";
        }
    }
}
