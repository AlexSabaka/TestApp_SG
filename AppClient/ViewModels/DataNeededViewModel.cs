using AppApi.Models;

using ReactiveUI;

using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppClient
{
    public class DataNeededInputTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is DataNeededInputViewModel model)
            {
                return element.FindResource($"{model.InputType}InputStyle") as DataTemplate;

                //return
                //    element.FindResource("myTaskTemplate") as DataTemplate;
            }

            return null;
        }
    }

    public class DataNeededInputViewModel : ReactiveObject
    {
        public DataNeededInputType InputType { get; }

        private int _maxLength;
        public int MaxLength
        {
            get => _maxLength;
            set => this.RaiseAndSetIfChanged(ref _maxLength, value);
        }

        private string _data = string.Empty;
        public string Data
        {
            get => _data;
            set => this.RaiseAndSetIfChanged(ref _data, value);
        }

        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public ReactiveCommand<string, Unit> Command { get; }

        public DataNeededInputViewModel(DataNeededInputModel inputModel, ReactiveCommand<string, Unit> command)
        {
            InputType = inputModel.Type;
            MaxLength = inputModel.MaxLength;
            Data = inputModel.Data;
            Text = inputModel.Text;
            Command = command;
        }
    }

    public class DataNeededViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private IEnumerable<DataNeededInputViewModel> _inputs;
        public IEnumerable<DataNeededInputViewModel> Inputs
        {
            get => _inputs;
            set => this.RaiseAndSetIfChanged(ref _inputs, value);
        }

        public ReactiveCommand<string, Unit> Command { get; }

        public string Title { get; }

        public DataNeededViewModel(IScreen screen, DataNeededModel model = null, Action<DataNeededReplyModel> onReplay = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            Command = ReactiveCommand.Create<string, Unit>(buttonData => {
                var x = _inputs.Select(vm => vm.Data).ToList();
                onReplay?.Invoke(new DataNeededReplyModel { Type = model?.Type, Data = x });
                return Unit.Default;
            });

            _inputs = model?.Inputs.Select(x => new DataNeededInputViewModel(x, Command));
            Title = model?.Title ?? "";
        }
    }
}