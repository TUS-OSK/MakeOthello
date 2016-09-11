using System.Collections.Generic;

namespace MakeOthello.Model
{
    public delegate void OthelloEndEventHandler(IOthello othello, int result);

    public delegate void OthelloPassEventHandler(IOthello othello, int pass);

    public interface IOthello
    {
        int[,] Board { get; }
        int Turn { get; }
        int Count { get; }
        List<Point> GetPossiblePoints(int disc);
        int GetDiscNumber(int disc);
        void Start();
        bool Put(Point point);
        void Pass();
        bool Back();       
        event OthelloEndEventHandler EndEvent;
        event OthelloPassEventHandler PassEvent;
    }
}
