using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Printing.OptionDetails;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MakeOthello.Model;
using MakeOthello.Utility;
using MakeOthello.View;
using MakeOthello.View.Controls;
using MakeOthello.ViewModel.MessageBox;


namespace MakeOthello.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        private string _blackNumberText;
        private string _whiteNumberText;
        private int cpuLevel;
        private int playercolor;
        private ICommand _BackCommand;
        private ICommand _QuitCommand;
        private Visibility _waitingMaskVisibility;
        private MessageBoxViewModel _messageBoxData;

        public double Height { get; set; }
        public double Width { get; set; }
        public IOthello Othello { get; set; }
        public OthelloAiBase Ai { get; private set; }
        public DiscViewModel[] DiscDataList { get; private set; }

        public MessageBoxViewModel MessageBoxData
        {
            get
            {
                return _messageBoxData;
            }
            private set
            {
                OnPropertyChanged();
                _messageBoxData = value;
            }
        }

        public PassControlViewModel PassControlData { get; private set; }

        public Visibility WaitingMaskVisibility
        {
            get { return _waitingMaskVisibility; }
            private set
            {
                _waitingMaskVisibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand BackCommand
        {
            get { return _BackCommand; }
            set
            {
                _BackCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand QuitCommand
        {
            get { return _QuitCommand; }
            set
            {
                _QuitCommand = value;
                OnPropertyChanged();
            }
        }

        public string BlackPlayerText { get; private set; }
        public string WhitePlayerText { get; private set; }

        public string BlackNumberText
        {
            get { return _blackNumberText; }
            set
            {
                _blackNumberText = value;
                OnPropertyChanged();
            }
        }

        public string WhiteNumberText
        {
            get { return _whiteNumberText; }
            set
            {
                _whiteNumberText = value;
                OnPropertyChanged();
            }
        }


        public BoardViewModel(int player = -1, int cpu = 0)
        {
            WaitingMaskVisibility = Visibility.Collapsed;
            MessageBoxData = MessageBoxViewModel.Empty;
            PassControlData = new PassControlViewModel();
            DiscDataList = new DiscViewModel[64];


            double min = Math.Min(Frame.ActualHeight, Frame.ActualWidth);
            if (min < 600)
            {
                Height = min * 5 / 6;
                Width = min * 5 / 6;
            }
            else
            {
                Height = 500;
                Width = 500;
            }
            cpuLevel = cpu;
            playercolor = player;
            Othello = new Othello();
            Othello.Start();
            QuitCommand = new SimpleCommand(o =>
            {
                MessageBoxData = new ConfirmMessageBoxViewModel();
            });


            BlackNumberText = Othello.GetDiscNumber(-1).ToString();
            WhiteNumberText = Othello.GetDiscNumber(1).ToString();
            if (cpu == 0)
            {
                BlackPlayerText = "1P: ";
                WhitePlayerText = "2P: ";
                BackCommand = new SimpleCommand(o =>
                {
                    Othello.Back();
                    Update();
                });
                Othello.EndEvent += (othello, resulut) =>
                {
                    if (resulut == 0)
                    {
                        MessageBoxData = new EndMessageBoxViewModel(player, cpu, "Draw !");
                    }
                    else if (playercolor == resulut)
                    {
                        MessageBoxData = new EndMessageBoxViewModel(player, cpu, "2P Success");
                    }
                    else
                    {
                        MessageBoxData = new EndMessageBoxViewModel(player, cpu, "2P Success");
                    }
                };
                PassControlData.OkCommand = new SimpleCommand(o =>
                {
                    PassControlData.Visibility = Visibility.Collapsed;
                    Othello.Pass();
                    Update();
                });
                Othello.PassEvent += (othello, pass) =>
                {
                    PassControlData.Visibility = Visibility.Visible;
                };
                Initplayer();
            }
            else
            {
                if (player == 1)
                {
                    BlackPlayerText = "CPU Lv." + cpu + ":";
                    WhitePlayerText = "You:";
                }
                else
                {
                    WhitePlayerText = "CPU Lv." + cpu + ":";
                    BlackPlayerText = "You:";
                }
               
                BackCommand = new SimpleCommand(o =>
                {
                    PassControlData.Visibility = Visibility.Collapsed;
                    Othello.Back();
                    Othello.Back();
                    Update();
                });
                Othello.EndEvent += (othello, resulut) =>
                {
                    if (resulut == 0)
                    {
                        MessageBoxData = new EndMessageBoxViewModel(player, cpu, "Draw !");
                    }
                    else if (playercolor == resulut)
                    {
                        MessageBoxData = new LoseMessageBoxViewModel(player, cpu);
                    }
                    else
                    {
                        MessageBoxData = new WinMessageBoxViewModel(player, cpu);
                    }
                };
                PassControlData.OkCommand = new SimpleCommand(async o =>
                {
                    PassControlData.Visibility = Visibility.Collapsed;
                    Othello.Pass();
                    var points = Update();
                    await AiPutAsync(points);
                });
                Othello.PassEvent += (othello, pass) =>
                {
                    if (pass == player)
                        PassControlData.Visibility = Visibility.Visible;
                };
                Initcpu(cpu, playercolor);
            }
        }

        private List<Point> Update()
        {
            for (var i = 0; i < 64; i++)
            {
                var point = ConvertPoint(i);
                switch (Othello.Board[point.x, point.y])
                {
                    case -1:
                        DiscDataList[i].DiscCondition = DiscCondition.Black;
                        break;
                    case 1:
                        DiscDataList[i].DiscCondition = DiscCondition.White;
                        break;
                    case 0:
                        DiscDataList[i].DiscCondition = DiscCondition.Void;
                        break;
                }
            }
            var points = Othello.GetPossiblePoints(Othello.Turn);
            switch (Othello.Turn)
            {
                case -1:
                    foreach (var point in points)
                    {
                        DiscDataList[ConvertInt(point)].DiscCondition = DiscCondition.AbleBlack;
                    }
                    break;
                case 1:
                    foreach (var point in points)
                    {
                        DiscDataList[ConvertInt(point)].DiscCondition = DiscCondition.AbleWhite;
                    }
                    break;
            }
            BlackNumberText = Othello.GetDiscNumber(-1).ToString();
            WhiteNumberText = Othello.GetDiscNumber(1).ToString();
            return points;
        }

        private static Point ConvertPoint(int i)
        {
            return new Point(i / 8, i % 8);
        }

        private static int ConvertInt(Point point)
        {
            return point.x * 8 + point.y;
        }

        private async void Initcpu(int cpu, int playercolor)
        {
            Ai = new MakeOthelloAi(cpu);

            for (var i = 0; i < DiscDataList.Length; i++)
            {
                var discdata = new DiscViewModel(i, Height * 0.08);
                discdata.DiscTapedCommand = new SimpleCommand((async o =>
                {
                    if (!Othello.Put(ConvertPoint(discdata.Number)))
                        return;
                    var points = Update();
                    await AiPutAsync(points);
                }));
                DiscDataList[i] = discdata;
            }

            if (playercolor == 1)
            {
                var points = Update();
                await AiPutAsync(points);
            }
            else
            {
                Update();
            }
        }

        private void Initplayer()
        {
            for (var i = 0; i < DiscDataList.Length; i++)
            {
                var discdata = new DiscViewModel(i, Height * 0.08);
                discdata.DiscTapedCommand = new SimpleCommand((o =>
                {
                    if (!Othello.Put(ConvertPoint(discdata.Number)))
                        return;
                    Update();
                }));
                DiscDataList[i] = discdata;
            }
            Update();
        }

        private async Task AiPutAsync(List<Point> points)
        {
            WaitingMaskVisibility = Visibility.Visible;
            await Ai.PutAsync(Othello, points);
            Update();
            WaitingMaskVisibility = Visibility.Collapsed;
        }
    }
}
