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
        public OthelloAiBase Ai { get; private set; }

        public BoardViewModel()
        {
            Othello = new Othello();
            Othello.Start();

            Ai = new SampleOthelloAi();

            DiscDataList = new DiscViewModel[64];
            for (var i = 0; i < DiscDataList.Length; i++)
            {
                var discdata = new DiscViewModel(i);
                discdata.DiscTapedCommand = new SimpleCommand((async o =>
                {
                    if (!Othello.Put(ConvertPoint(discdata.Number)))
                        return;
                    Update();

                    // TODO ここでプレーヤーの入力を無効に
                    await Ai.PutAsync(Othello);
                    Update();

                    // TODO ここでプレーヤーの入力を有効に
                }));
                DiscDataList[i] = discdata;
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
