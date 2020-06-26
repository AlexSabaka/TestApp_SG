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


        public ReactiveCommand<Unit, IRoutableViewModel> AuthCommand { get; }

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

        public AuthViewModel(IScreen screen, IRoutableViewModel onSuccessViewModel)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            _userService = Locator.Current.GetService<IUserService>();

            var onError = new DataNeededModel
            {
                Title = "XXX",
                Inputs = new List<DataNeededInputModel>
                {
                    new DataNeededInputModel { Type = DataNeededInputType.Block, Text= "Unexpected error during log in." },
                    new DataNeededInputModel { Type = DataNeededInputType.Block, Text= "Please try again..." },
                    new DataNeededInputModel { Type = DataNeededInputType.Button, Text= "OK", Data= "OK" },
                }
            };

            AuthCommand = ReactiveCommand.CreateFromObservable<Unit, IRoutableViewModel>(
                 _ => Observable
                        .FromAsync(() => _userService.Auth(new AuthModel { Name = UserName, Password = Password }))
                        .Where(x => x != null)
                        .SelectMany(_ => this.NavigateToView(onSuccessViewModel))
                        .Catch(this.NavigateToView(new DataNeededViewModel(HostScreen, onError, _ => this.NavigateBack()))),
                Observable.Concat(this.WhenAnyValue(x => x.UserName),
                                  this.WhenAnyValue(x => x.Password))
                          .Select(x => !string.IsNullOrEmpty(x)));
        }
    }
}
