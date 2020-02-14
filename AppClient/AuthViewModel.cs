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
    public class AuthViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IUserService _userService;

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private ReactiveCommand<Unit, IRoutableViewModel> _regCommand;
        public ReactiveCommand<Unit, IRoutableViewModel> RegisterCommand
        {
            get => _regCommand;
            set => this.RaiseAndSetIfChanged(ref _regCommand, value);
        }

        private ReactiveCommand<Unit, IRoutableViewModel> _authCommand;
        public ReactiveCommand<Unit, IRoutableViewModel> AuthCommand
        {
            get => _authCommand;
            set => this.RaiseAndSetIfChanged(ref _authCommand, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _errorDescription;
        public string ErrorDescription
        {
            get => _errorDescription;
            set => this.RaiseAndSetIfChanged(ref _errorDescription, value);
        }

        public AuthViewModel(IScreen screen = null, string errorText = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            _userService = Locator.Current.GetService<IUserService>();

            ErrorDescription = errorText;

            SetupCommands();
        }

        private void SetupCommands()
        {
            AuthCommand = ReactiveCommand.CreateFromObservable(
            () =>
            {
                AuthModel model = new AuthModel
                {
                    Name = UserName,
                    Password = Password,
                };

                try
                {
                    var auth = _userService.Auth(model);
                    auth.Wait();

                    if (auth.Result == null)
                        return HostScreen.Router.NavigateAndReset.Execute(new AuthViewModel(errorText: "User does not exists"));

                    return HostScreen.Router.Navigate.Execute(new ItemsViewModel());
                }
                catch (Exception ex)
                {
                        //todo handle exception and display message
                        return HostScreen.Router.NavigateAndReset.Execute(new AuthViewModel(errorText: ex.Message));
                }
            },
            Observable.Concat(this.WhenAnyValue(x => x.UserName),
                              this.WhenAnyValue(x => x.Password))
                      .Select(x => !string.IsNullOrEmpty(x)));


            RegisterCommand = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    AuthModel model = new AuthModel
                    {
                        Name = UserName,
                        Password = Password,
                    };

                    try
                    {
                        var auth = _userService.Register(model);
                        auth.Wait();

                        if (auth.Result == null)
                            return HostScreen.Router.NavigateAndReset.Execute(new AuthViewModel(errorText: "User already exists"));

                        return HostScreen.Router.Navigate.Execute(new ItemsViewModel());
                    }
                    catch (Exception ex)
                    {
                        //todo handle exception and display message
                        return HostScreen.Router.NavigateAndReset.Execute(new AuthViewModel(errorText: ex.Message));
                    }
                },
                Observable.Concat(this.WhenAnyValue(x => x.UserName),
                                  this.WhenAnyValue(x => x.Password))
                          .Select(x => !string.IsNullOrEmpty(x)));
        }
    }
}
