using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MakeOthello.Model;
using MakeOthello.Utility;
using MakeOthello.View;

namespace MakeOthello.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        private string _DiscNumberLeft;
        private string _DiscNumberRight;
        private int cpuLevel;
        private ICommand _BackCommand;

        public double Height { get; set; }
        public double Width { get; set; }
        public IOthello Othello { get; set; }
        public OthelloAiBase Ai { get; private set; }
        public DiscViewModel[] DiscDataList { get; private set; }
        public PopUpControleViewModel PopUpData { get; private set; }
        public PopUpControleViewModel WinPopUpData { get; private set; }
        public PopUpControleViewModel LosePopUpData { get; private set; }
        public string PlayerRight { get; private set; }
        public string PlayerLeft { get; private set; }
        public ICommand BackCommand
        {
            get { return _BackCommand; }
            set
            {
                _BackCommand = value;
                OnPropertyChanged();
            }
        }

        public string DiscNumberLeft
        {
            get { return _DiscNumberLeft; }
            set
            {
                _DiscNumberLeft = value;
                OnPropertyChanged();
            }
        }

        public string DiscNumberRight
        {
            get { return _DiscNumberRight; }
            set
            {
                _DiscNumberRight = value;
                OnPropertyChanged();
            }
        }


        public new Windows.UI.Core.CoreDispatcher Dispatcher
        {
            get { return base.Dispatcher; }
            set
            {
                base.Dispatcher = value;
                PopUpData.Dispatcher = value;
                LosePopUpData.Dispatcher = value;
                WinPopUpData.Dispatcher = value;
            }
        }

        public BoardViewModel(Frame frame, int playercolor = -1, int cpu = -1) : base(frame)
        {
            double min = Math.Min(frame.Width, frame.Height);
            if (min < 720)
            {
                Height = min * 600 / 720;
                Width = min * 600 / 720;
            }
            else
            {
                Height = 600;
                Width = 600;
            }
            cpuLevel = cpu;
            Othello = new Othello();
            Othello.Start();
            Othello.PassEvent += (othello, pass) =>
            {
                PopUpData.Visibility = Visibility.Visible;
            };
            WinPopUpData = new PopUpControleViewModel();
            LosePopUpData = new PopUpControleViewModel();
            PopUpData = new PopUpControleViewModel();

            if (cpu == -1)
            {
                PlayerLeft = ":1P";
                PlayerRight = ":2P";
            }
            else
            {
                PlayerRight = ":CPU Lv." + cpu;
                PlayerLeft = ":You";
            }

            if (cpu == -1)
            {
                DiscNumberLeft = Othello.GetDiscNumber(playercolor).ToString();
                DiscNumberRight = Othello.GetDiscNumber(-1 * playercolor).ToString();
            }
            else
            {
                DiscNumberLeft = Othello.GetDiscNumber(-1).ToString();
                DiscNumberRight = Othello.GetDiscNumber(1).ToString();
            }

            BackCommand = new SimpleCommand(o =>
              {
                  Othello.Back();
                  Othello.Back();
                  Update();
              });

            PopUpData.OkCommand = new SimpleCommand(async o =>
            {
                PopUpData.Visibility = Visibility.Collapsed;
                Othello.Pass();
                var points = Update();
                await AiPutAsync(points);

            });
            LosePopUpData.QuitCommand = new SimpleCommand(o =>
              {
                  Navigate(typeof(MainPage));
              });
            WinPopUpData.QuitCommand = new SimpleCommand(o =>
            {
                Navigate(typeof(MainPage));
            });
            DiscDataList = new DiscViewModel[64];
            Initcpu(cpu);

            Othello.EndEvent += (othello, resulut) =>
            {

                if (playercolor == resulut)
                {
                    LosePopUpData.Visibility = Visibility.Visible;
                }
                else
                {
                    WinPopUpData.Visibility = Visibility.Visible;

                }
            };
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

            if (cpuLevel == -1)
            {
                DiscNumberLeft = Othello.GetDiscNumber(Othello.Turn).ToString();
                DiscNumberRight = Othello.GetDiscNumber(-1 * Othello.Turn).ToString();
            }
            else
            {
                DiscNumberLeft = Othello.GetDiscNumber(-1).ToString();
                DiscNumberRight = Othello.GetDiscNumber(1).ToString();
            }
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

        private void Initcpu(int cpu)
        {
            Ai = new SampleOthelloAi();
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
            Update();
        }

        private async Task AiPutAsync(List<Point> points)
        {
            // TODO ここでプレーヤーの入力を無効に
            await Ai.PutAsync(Othello, points);
            Update();

            // TODO ここでプレーヤーの入力を有効に
        }
    }
}
