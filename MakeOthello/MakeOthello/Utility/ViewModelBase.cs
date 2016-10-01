using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MakeOthello.Utility
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public Windows.UI.Core.CoreDispatcher Dispatcher { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// �v���p�e�B�̕ύX�ʒm�C�x���g�𔭐�������
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
    }
}
