using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;
using ReactiveUI;
using Splat;

namespace AppClient
{
    public class AssistanceViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private ObservableCollection<ActionCommandViewModel> _assistanceFunctions;
        public ObservableCollection<ActionCommandViewModel> AssistanceFunctions
        {
            get => _assistanceFunctions;
            set => this.RaiseAndSetIfChanged(ref _assistanceFunctions, value);
        }

        public AssistanceViewModel(IScreen screen)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            AssistanceFunctions = new ObservableCollection<ActionCommandViewModel>
            {
                new ActionCommandViewModel(name: "Back", action: () => this.NavigateBack()),
                new ActionCommandViewModel(
                    name: "MakeDataNeeded",
                    action: () => this.NavigateToView(
                        new DataNeededViewModel(
                            screen: HostScreen,
                            onReplay: rm =>
                            {
                                this.NavigateBack();
                                MessageBox.Show($"DataNeededReply -- '{string.Join(", ", rm.Data)}'");
                            },
                            model: new DataNeededModel 
                            { 
                                Title = "XXX",
                                Inputs = new List<DataNeededInputModel>
                                {
                                    new DataNeededInputModel { Type = DataNeededInputType.Block, Text= "Authentication" },
                                    new DataNeededInputModel { Type = DataNeededInputType.Text, Text= "Username" },
                                    new DataNeededInputModel { Type = DataNeededInputType.Text, Text= "Password" },
                                    new DataNeededInputModel { Type = DataNeededInputType.Button, Text= "OK", Data= "OK" },
                                    new DataNeededInputModel { Type = DataNeededInputType.Button, Text= "Cancel", Data= "Cancel" },
                                }
                            }))),
                //TODO: ...
            };
        }
    }
}
