using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using MakeOthello.Utility;
using MakeOthello.View;

namespace MakeOthello.ViewModel
{
    class SelectPageViewModel : ViewModelBase
    {
        private SolidColorBrush _level1Background;
        private SolidColorBrush _level2Background;
        private SolidColorBrush _level3Background;
        private SolidColorBrush _discBackground;
        public ICommand GoNextCommand { get; set; }
        public ICommand ChangeColorCommand { get; set; }
        public ICommand SetLevelCommand { get; set; }
        private int CpuLevel;
        private int DiscColor;

        public SolidColorBrush Level1Background
        {
            get { return _level1Background; }
            set
            {
                _level1Background = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush Level2Background
        {
            get { return _level2Background; }
            set
            {
                _level2Background = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush Level3Background
        {
            get { return _level3Background; }
            set
            {
                _level3Background = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush DiskBackground
        {
            get { return _discBackground; }
            set
            {
                _discBackground = value;
                OnPropertyChanged();
            }
        }


        private Color _buttonColor = Colors.DarkSeaGreen;
        public SelectPageViewModel(Frame frame)
        {
            
            Level1Background = new SolidColorBrush(Colors.White);
            Level2Background = new SolidColorBrush(_buttonColor);
            Level3Background = new SolidColorBrush(_buttonColor);
            CpuLevel = 1;
            DiskBackground =new SolidColorBrush(Colors.Black);
            DiscColor = -1;
            
            this.Frame = frame;
            SetLevelCommand = new SimpleCommand(param =>
            {
                switch (param.ToString())
                {
                    case "1":
                        Level1Background = new SolidColorBrush(Colors.White);
                        Level2Background = new SolidColorBrush(_buttonColor);
                        Level3Background = new SolidColorBrush(_buttonColor);
                        CpuLevel = 1;
                        break;
                    case "2":
                        Level2Background = new SolidColorBrush(Colors.White);
                        Level1Background = new SolidColorBrush(_buttonColor);
                        Level3Background = new SolidColorBrush(_buttonColor);
                        CpuLevel = 2;
                        break;
                    case "3":
                        Level3Background = new SolidColorBrush(Colors.White);
                        Level2Background = new SolidColorBrush(_buttonColor);
                        Level1Background = new SolidColorBrush(_buttonColor);
                        CpuLevel = 3;
                        break;
                }
            });
            ChangeColorCommand=new SimpleCommand(param =>
                {
                    if (param.ToString()=="0")
                    {
                        if (DiscColor==-1)
                        {
                            DiskBackground=new SolidColorBrush(Colors.White);
                            DiscColor = 1;
                        }
                        else
                        {
                            DiskBackground=new SolidColorBrush(Colors.Black);
                            DiscColor = -1;
                        }
                            
                    }
                });

            GoNextCommand = new SimpleCommand(param =>
            {
                var vm = new BoardViewModel(Frame,DiscColor,CpuLevel);
                vm.Dispatcher = Dispatcher;
                this.Frame.Navigate(typeof(GamePage), vm);
            });
        }
    }
}
