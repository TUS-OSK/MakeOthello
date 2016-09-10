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
    class BoardViewModel:ViewModelBase
    {
        public IOthello Othello { get; set; }

        public ICommand DiscTapedCommand { get; set; }

        public BoardViewModel()
        {
            DiscTapedCommand  =new SimpleCommand(DiscTaped);
            DiscCondition =new DiscCondition[64];


        }

        private void Update()
        {
            
        }

        public DiscCondition[] DiscCondition { get; private set; }

        private void DiscTaped(object obj)
        {
            var number = (int) obj-1;
            Othello.Put(new Point(number/8,number%8));
            var possible=Othello.GetPossiblePoint(Othello.Turn);

        }

        private static Point ConvertPoint(int i)
        {
           return new Point(i/8,i%8);
        }

        private static int ConvertInt(Point point)
        {
            return point.x*8 + point.y;
        }

    }
}
