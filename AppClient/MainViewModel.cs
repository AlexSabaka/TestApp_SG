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
    public class MainViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; private set; }

        public MainViewModel()
        {
            Router = new RoutingState();

            Router.Navigate.Execute(new AuthViewModel());
        }
    }
}
