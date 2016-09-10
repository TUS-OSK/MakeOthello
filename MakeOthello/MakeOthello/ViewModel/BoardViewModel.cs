using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using MakeOthello.Model;
using MakeOthello.View;
using OseroSample.Utillity;

namespace MakeOthello.ViewModel
{
    class BoardViewModel : ViewModelBase
    {
        public IOthello Othello { get; set; }

        

        public BoardViewModel()
        {
            Othello = new Othello();
            Othello.Start();
            
            DiscDataList = new DiscViewModel[64];
            for (var i = 0; i < DiscDataList.Length; i++)
            {
                DiscDataList[i] = new DiscViewModel();
                DiscDataList[i].DiscTapedCommand = new SimpleCommand(DiscTaped);
            }
            Update();
        }

        private void Update()
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
        }

        public DiscViewModel[] DiscDataList { get; private set; }

        private void DiscTaped(object obj)
        {
            var number = int.Parse(obj.ToString());
            Othello.Put(new Point(number / 8, number % 8));
            Update();
        }

        private static Point ConvertPoint(int i)
        {
            return new Point(i / 8, i % 8);
        }

        private static int ConvertInt(Point point)
        {
            return point.x * 8 + point.y;
        }

    }
}
