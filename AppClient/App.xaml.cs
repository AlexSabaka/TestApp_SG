using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;
using ReactiveUI;
using Refit;
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

            Locator.CurrentMutable.RegisterLazySingleton(() => new CurrentUserInfo());

            Locator.CurrentMutable.RegisterLazySingleton(() => RestService.For<IUserService>(ApiHost), typeof(IUserService));
            Locator.CurrentMutable.RegisterLazySingleton(() => RestService.For<IOrderService>(ApiHost), typeof(IOrderService));
            Locator.CurrentMutable.RegisterLazySingleton(() => RestService.For<IItemsService>(ApiHost), typeof(IItemsService));

            Locator.CurrentMutable.RegisterConstant(new MainViewModel(), typeof(IScreen));
        }
    }

    public class CurrentUserInfo
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public bool IsLogin => !string.IsNullOrEmpty(Token);
    }
}
