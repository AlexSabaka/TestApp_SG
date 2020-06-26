using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Windows;

using AppApi.Client;

using Autofac;
using Autofac.Core;

using ReactiveUI;

using Splat;

namespace AppClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly string ApiHost = "http://localhost:49690/api";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetDI();
        }

        private void SetDI()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            //Locator.CurrentMutable.RegisterLazySingleton(() => RestService.For<IUserService>(ApiHost), typeof(IUserService));
            //Locator.CurrentMutable.RegisterLazySingleton(() => RestService.For<IOrderService>(ApiHost), typeof(IOrderService));
            //Locator.CurrentMutable.RegisterLazySingleton(() => RestService.For<IItemsService>(ApiHost), typeof(IItemsService));

            Locator.CurrentMutable.RegisterLazySingleton(() => new TestUserService(), typeof(IUserService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new TestOrderService(), typeof(IOrderService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new TestItemsService(), typeof(IItemsService));

            Locator.CurrentMutable.RegisterConstant(new MainViewModel(), typeof(IScreen));

        }
    }
}
