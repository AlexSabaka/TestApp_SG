using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;
using ReactiveUI;
using Splat;

namespace AppClient
{
    public class ScanAndBagViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> PickListCommand { get; }

        public ReactiveCommand<Unit, Unit> ScanItem { get; }

        public ScanAndBagViewModel(IScreen screen)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            (HostScreen as MainViewModel).Transaction = new TransactionViewModel { };

            PickListCommand = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return HostScreen.Router.Navigate.Execute(new PickListViewModel(HostScreen));
                });

            ScanItem = ReactiveCommand.Create(() => { });
        }
    }
}
