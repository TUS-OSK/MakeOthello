using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class MakeLookAhead : ILookAhead
    {
        

        private List<Point> _possiblePoints;
        private Othello othello;

        

        public int AheadNumber { get; set; } 
        public MakeLookAhead(int aheadnum)
        {
            AheadNumber = aheadnum;
        }
        public Point LookAhead(IOthello Othello, int turn)
        {
            var blackScore = othello.GetDiscNumber(1);
            var whiteScore = othello.GetDiscNumber(-1);

            this.othello = othello;
            //this.bosrd=new Board();
            List<int> scoreList = new List<int>();
            for(int i=0; i < _possiblePoints.Count; i++)
            {
                scoreList.Add(0);
                Othello.Put(_possiblePoints[i]);
                switch (othello.Condition)
                {
                    case OthelloCondition.Wait:
                        break;
                    case OthelloCondition.Pass:
                        break;
                    case OthelloCondition.End:
                        break;
                }
            }


            throw new NotImplementedException();
        }
        private int algorithm(int depth, int alfa, int beta)
        {
            var blackScore = othello.GetDiscNumber(1);
            var whiteScore = othello.GetDiscNumber(-1);

            if (depth == 0)
            {
                return judge();
            }
            if (othello.Turn == 1)
            {
                for(int i = 0; i < _possiblePoints.Count; i++)
                {
                    othello.Put(_possiblePoints[i]);
                    switch (othello.Condition)
                    {
                        case OthelloCondition.Wait:
                            alfa = Math.Max(alfa, algorithm(depth - 1, alfa, beta));
                            break;
                        case OthelloCondition.Pass:
                            othello.Pass();
                            //player -1のターン
                            alfa = Math.Max(alfa, algorithm(depth - 1, alfa, beta));
                            othello.Back();
                            break;
                        case OthelloCondition.End:
                            alfa = (blackScore - whiteScore) * 100;
                            break;
                    }
                    othello.Back();
                    if (alfa >= beta)
                    {
                        return beta;
                    }
                }
                return alfa;
            }
            else
            {
                for(int i = 0; i < _possiblePoints.Count; i++)
                {
                    othello.Put(_possiblePoints[i]);
                    switch (othello.Condition)
                    {
                        case OthelloCondition.Wait:
                            beta = Math.Min(beta, algorithm(depth - 1, alfa, beta));
                            break;
                        case OthelloCondition.Pass:
                            othello.Pass();
                            beta = Math.Min(beta, algorithm(depth - 1, alfa, beta));
                            othello.Back();
                            break;
                        case OthelloCondition.End:
                            beta = (blackScore - whiteScore) * 100;
                            break;
                    }
                    othello.Back();
                    if (alfa >= beta)
                    {
                        return alfa;
                    }
                }
                return beta;
            }
        }
        private int judge()
        {
            var blackScore = othello.GetDiscNumber(1);
            var whiteScore = othello.GetDiscNumber(-1);

            int score;
            //score = judgeQ(othello.Board);
            for(int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                    {

                    }
            //score += judgeQ(board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    {

                    }
            //score += judgeQ(board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    {

                    }
            //score += judgeQ(board);


            //score -= blackScore / 2;
            //score += whiteScore / 2;
            return score;
        }
        //private int judgeQ(Board board)
        //{

        //}
    }
}
