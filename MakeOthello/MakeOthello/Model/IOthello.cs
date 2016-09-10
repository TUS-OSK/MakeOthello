using System.Collections.Generic;

namespace MakeOthello.Model
{
     public class Point
     {
         public Point(int x,int y)
         {
             this.x = x;
             this.y = y;
         }
         public int x;
         public int y;
     }
    public delegate void OthelloEndEventHandler(IOthello othello, int result);

    public delegate void OthlloPassEventHandler(IOthello othello, int pass);

    public interface IOthello
    {
     int[][] Board { get; }
        void GameStart();
        int Turn { get; }
        List<Point> GetPossiblePoint(int disc);
        bool Put(Point point);
        int GetDiscNumber(int disc);
        event OthelloEndEventHandler EndEvent;
        bool Back();
    }  
}
