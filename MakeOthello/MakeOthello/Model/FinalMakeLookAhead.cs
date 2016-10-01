using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class FinalMakeLookAhead : ILookAhead
    {
        public FinalMakeLookAhead()
        { }
        private IOthello othello;
        private List<Point> _possiblePoints;

        public Point LookAhead(IOthello Othello, int turn)
        {
            var blackScore = othello.GetDiscNumber(1);
            var whiteScore = othello.GetDiscNumber(-1);
            this.othello = Othello;
            List<int> scoreList = new List<int>();
            for(int i = 0; i < _possiblePoints.Count; i++)
            {
                scoreList.Add(0);
                Othello.Put(_possiblePoints[i]);
                switch (Othello.Condition)
                {
                    case OthelloCondition.Wait:
                        scoreList[i] = algorithm(-10000, 10000);
                        break;
                    case OthelloCondition.Pass:
                        othello.Pass();
                        scoreList[i] = algorithm(-10000, 10000);
                        othello.Back();
                        break;
                    case OthelloCondition.End:
                        scoreList[i] = (blackScore-whiteScore) * 100;
                        break;
                }
                othello.Back();
                if (scoreList[i] > 0 && othello.Turn == 1 || scoreList[i] < 0 && othello.Turn == -1)
                {
                    break;
                }
            }
            throw new NotImplementedException();
        }

        private int algorithm(int alfa, int beta)
        {
            var blackScore = othello.GetDiscNumber(1);
            var whiteScore = othello.GetDiscNumber(-1);

            if (othello.Turn == 1)
            {
                for(int i = 0; i < _possiblePoints.Count; i++)
                {
                    othello.Put(_possiblePoints[i]);
                    switch (othello.Condition)
                    {
                        case OthelloCondition.Wait:
                            alfa = Math.Max(alfa, algorithm(alfa, beta));
                            break;
                        case OthelloCondition.Pass:
                            othello.Pass();
                            alfa = Math.Max(alfa, algorithm(alfa, beta));
                            othello.Back();
                            break;
                        case OthelloCondition.End:
                            alfa = blackScore - whiteScore;
                            break;
                    }
                    othello.Back();
                    if(alfa >= beta)
                    {
                        return beta;
                    }
                    if(alfa > 0)
                    {
                        return alfa;
                    }
                }
                return alfa;
            }
            else
            {
                for (int i = 0; i < _possiblePoints.Count; i++)
                {
                    othello.Put(_possiblePoints[i]);
                    switch (othello.Condition)
                    {
                        case OthelloCondition.Wait:
                            beta = Math.Min(beta, algorithm(alfa, beta));
                            break;
                        case OthelloCondition.Pass:
                            othello.Pass();
                            beta = Math.Min(beta, algorithm(alfa, beta));
                            othello.Back();
                            break;
                        case OthelloCondition.End:
                            beta = blackScore - whiteScore;
                            break;
                    }
                    othello.Back();
                    if (alfa >= beta)
                    {
                        return alfa;
                    }
                    if (beta < 0)
                    {
                        return beta;
                    }
                }
                return beta;
            }
        }
    }
}
