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
    public class ActionCommandViewModel : ReactiveObject
    {

        public ReactiveCommand<Unit, Unit> Command { get; }

        public string Name { get; }


        public ActionCommandViewModel(string name, Action action)
            : this(name, ReactiveCommand.Create(action))
        {
        }
        public ActionCommandViewModel(string name, ReactiveCommand<Unit, Unit> action)
        {
            Name = name;
            Command = action;
        }
    }
}
