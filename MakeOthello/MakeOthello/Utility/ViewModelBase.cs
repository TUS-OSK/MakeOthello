using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;

namespace MakeOthello.Utility
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public Windows.UI.Core.CoreDispatcher Dispatcher { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public Frame Frame { get; set; }

        public ViewModelBase(Frame frame = null)
        {
            Frame = frame;
        }

        /// <summary>
        /// プロパティの変更通知イベントを発生させる
        /// </summary>
        /// <param name="name"></param>
        protected async virtual void OnPropertyChanged([CallerMemberName]string name = null)
        {
            if (Dispatcher != null)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
                });
            }
            else
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        protected async void Navigate(Type page,object param = null)
        {
            if (Dispatcher != null)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    Frame.Navigate(page, param);
                });
            }
            else
            {
                Frame.Navigate(page, param);
            }
        }
    }
}
