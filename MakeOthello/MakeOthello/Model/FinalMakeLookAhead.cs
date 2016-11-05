using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class FinalMakeLookAhead : ILookAhead
    {
        private IOthello othello;

        public Point LookAhead(IOthello Othello, int turn)
        {
            othello = Othello;
            List<int> scoreList = new List<int>();
            var possiblePoints = othello.GetPossiblePoints(othello.Turn);
            for (int i = 0; i < possiblePoints.Count; i++)
            {
                scoreList.Add(0);
                Othello.Put(possiblePoints[i]);
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
                        scoreList[i] = -(othello.GetDiscNumber(1) - othello.GetDiscNumber(-1));
                        break;
                }
                othello.Back();
                //if (scoreList[i] > 0 && othello.Turn == 1 || scoreList[i] < 0 && othello.Turn == -1)
                //{
                //    break;
                //}
            }
            if (othello.Turn == 1)
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] == scoreList.Max())
                    {
                        return possiblePoints[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] == scoreList.Min())
                    {
                        return possiblePoints[i];
                    }
                }
            }
            return possiblePoints[0];
        }

        private int algorithm(int alfa, int beta)
        {
            if (othello.Turn == 1) //白
            {
                var _possiblePoints = othello.GetPossiblePoints(1);

                for (int i = 0; i < _possiblePoints.Count; i++)
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
                            alfa = othello.GetDiscNumber(-1) - othello.GetDiscNumber(1);
                            break;
                    }
                    othello.Back();
                    if (alfa >= beta)
                    {
                        return beta;
                    }
                    //if (alfa > 0)
                    //{
                    //    return alfa;
                    //}
                }
                return alfa;
            }
            else //黒
            {
                var possiblePoints = othello.GetPossiblePoints(-1);
                for (int i = 0; i < possiblePoints.Count; i++)
                {
                    othello.Put(possiblePoints[i]);
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
                            beta = othello.GetDiscNumber(-1) - othello.GetDiscNumber(1);
                            break;
                    }
                    othello.Back();
                    if (alfa >= beta)
                    {
                        return alfa;
                    }
                    //if (beta < 0)
                    //{
                    //    return beta;
                    //}
                }
                return beta;
            }
        }
    }
}
