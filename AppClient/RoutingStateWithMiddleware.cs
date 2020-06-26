using System;
using System.Reactive.Linq;

using ReactiveUI;

namespace AppClient
{
    public class RoutingStateWithMiddleware : RoutingState
    {
        public RoutingStateWithMiddleware(Func<IRoutableViewModel, IRoutableViewModel> middleware = null) : base()
        {
            Navigate = ReactiveCommand.CreateFromObservable<IRoutableViewModel, IRoutableViewModel>(
                  vm =>
                  {
                      var nextVm = middleware?.Invoke(vm) ?? vm;

                      if (nextVm == null)
                      {
                          return this.CurrentViewModel;
                      }

                      this.NavigationStack.Add(nextVm);

                      return Observable.Return(nextVm);
                  },
                  outputScheduler: Scheduler);
        }
    }
}
