using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using MakeOthello.Utility;
using MakeOthello.View;

namespace MakeOthello.ViewModel
{
    class SelectPageViewModel : ViewModelBase
    {
        public ICommand GoNextCommand { get; set; }
        public ICommand ChangeColorCommand { get; set; }
        public ICommand SetLevelCommand { get; set; }

        public SelectPageViewModel(Frame frame)
        {
            this.Frame = frame;
            SetLevelCommand = new SimpleCommand(param =>
            {
                
            });
            GoNextCommand = new SimpleCommand(param =>
            {
                var vm = new BoardViewModel(Frame);
                vm.Dispatcher = Dispatcher;
                this.Frame.Navigate(typeof(GamePage), vm);
            });
        }
    }
}
