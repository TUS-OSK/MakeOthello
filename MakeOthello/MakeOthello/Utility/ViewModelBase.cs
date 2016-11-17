using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MakeOthello.Utility
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Frame Frame
        {
            get
            {
                Frame rootFrame = Window.Current.Content as Frame;
                return rootFrame;
            }
        }

        public CoreDispatcher Dispatcher
        {
            get { return CoreApplication.MainView.CoreWindow.Dispatcher; }
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

        protected async void Navigate(Type page, object param = null)
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
