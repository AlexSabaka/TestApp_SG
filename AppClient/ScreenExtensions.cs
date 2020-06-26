using System;
using System.Reactive;

using ReactiveUI;

namespace AppClient
{
    public static class ScreenExtensions
    {
        public static IObservable<IRoutableViewModel> NavigateToView(this IScreen s, IRoutableViewModel viewModel)
            => s.Router.Navigate.Execute(viewModel);

        public static IObservable<IRoutableViewModel> NavigateToView(this IRoutableViewModel r, IRoutableViewModel viewModel)
            => r.HostScreen.Router.Navigate.Execute(viewModel);

        public static IObservable<Unit> NavigateBack(this IScreen s)
            => s.Router.NavigateBack.Execute();

        public static IObservable<Unit> NavigateBack(this IRoutableViewModel r)
            => r.HostScreen.Router.NavigateBack.Execute();
    }
}
