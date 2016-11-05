using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Gaming.UI;

namespace MakeOthello.Model
{
    class MakeLookAhead : ILookAhead
    {
        private IOthello game;
        private int[,] board = new int[8,8];
        public int AheadNumber { get; set; }
        public Point LookAhead(IOthello Othello, int turn)
        {
            game = Othello;
            AheadNumber = turn;
            List<int> scoreList = new List<int>();
            var ableList = game.GetPossiblePoints(game.Turn);
            for (int i = 0; i < ableList.Count; i++)
            { 
                scoreList.Add(0);
                game.Put(ableList[i]);
                switch (game.Condition)
                {
                   case OthelloCondition.Wait:
                        scoreList[i] = algorithm(AheadNumber - 1, -10000, 10000);
                        break;
                    case OthelloCondition.Pass:
                        game.Pass();
                        scoreList[i] = algorithm(AheadNumber - 1, -10000, 10000);
                        game.Back();
                        break;
                    case OthelloCondition.End:
                        scoreList[i] = -(game.GetDiscNumber(1) - game.GetDiscNumber(-1))* 100;
                        break;
                }
                game.Back();
            }

            if (game.Turn == 1)
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] == scoreList.Max())
                    {
                        return ableList[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] == scoreList.Min())
                    {
                        return ableList[i];
                    }
                }
            }
            return ableList[0];
        }

        private int algorithm(int depth, int alfa, int beta)
        {
            if (depth == 0)
            {
                return judge();
            }

            if (game.Turn == 1)
            {
                var list = game.GetPossiblePoints(1);
                for (int i = 0; i < list.Count; i++)
                {
                    game.Put(list[i]);
                    switch (game.Condition)
                    {
                        case OthelloCondition.Wait:
                            alfa = Math.Max(alfa, algorithm(depth - 1, alfa, beta));
                            break;
                        case OthelloCondition.Pass:
                            game.Pass();
                            // player -1のターン
                            alfa = Math.Max(alfa, algorithm(depth - 1, alfa, beta));
                            game.Back();
                            break;

                        case OthelloCondition.End:
                            alfa = -(game.GetDiscNumber(1) - game.GetDiscNumber(-1)) * 100;
                            break;
                    }
                    game.Back();
                    if (alfa >= beta)
                    {
                        return beta;
                    }
                }
                return alfa;
            }

            else
            {
                var list = game.GetPossiblePoints(-1);
                for (int i = 0; i < list.Count; i++)
                {
                    game.Put(list[i]);
                    switch (game.Condition)
                    {
                        case OthelloCondition.Wait:
                            beta = Math.Min(beta, algorithm(depth - 1, alfa, beta));
                            break;
                        case OthelloCondition.Pass:
                            game.Pass();
                            beta = Math.Min(beta, algorithm(depth - 1, alfa, beta));
                            game.Back();
                            break;
                        case OthelloCondition.End:
                            beta = -(game.GetDiscNumber(1) - game.GetDiscNumber(-1)) * 100;
                            break;
                    }
                    game.Back();
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
            int score;
            score = judgeQ(game.Board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board[i, 7 - j] = game.Board[i, j];
                }
            score += judgeQ(board);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board[7 - i, j] = game.Board[i, j];
                }
            score += judgeQ(board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board[7 - i, 7 - j] = game.Board[i, j];
                }
            score += judgeQ(board);
            score -= game.GetPossiblePoints(-1).Count;
            score += game.GetPossiblePoints(1).Count;
            score += game.GetDiscNumber(-1) / 2;
            score -= game.GetDiscNumber(1) / 2;
            return score;
        }

        private int judgeQ(int[,] board)
        {
            int score = 0;
            if (board[0, 0] == 1)
            {
                score -= 100;
            }
            else if (board[0, 0] == -1)
            {
                score += 100;
            }
            else if (board[1, 1] == -1 && board[1, 0] == -1 && board[0, 1] == -1)
            {
                score -= 100;
            }
            else if (board[1, 1] == 1 && board[1, 0] == 1 && board[0, 1] == 1)
            {
                score += 100;
            }
            return score;
        }
    }
}
