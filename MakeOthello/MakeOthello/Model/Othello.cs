using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class Othello : IOthello
    {
        private static Vector2[] dir =
        { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 0),
           new Vector2(0, -1), new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1)
        };

        public Othello()
        {
            _boardList.Add(new int[8,8]);
        }

        public event OthelloEndEventHandler EndEvent;
        public event OthelloPassEventHandler PassEvent;

        private List<int[,]> _boardList = new List<int[,]>();  //いままでの譜面を記憶している
        private int _count;

        public int[,] Board
        {
            get { return _boardList[_count]; }
            private set
            {
                _boardList[_count] = value;
            }
        }

        public void Start()  //スタート
        {
            _boardList.Clear();
            _boardList.Add(new int[8, 8]);
            _count = 0;
            Turn = -1;

            //最初に置いてある石
            Board[3, 4] = -1;
            Board[4, 3] = -1;
            Board[3, 3] = 1;
            Board[4, 4] = 1;
        }

        public int Turn //今が黒の番か、白の番か
        {
            get; private set;
        }


        public bool Put(Point point)  //石を置く
        {
            // TODO とりあえず
            _boardList.Add(CopyBoard(Board));
            _count++;
            Board[point.x, point.y] = Turn;
            Turn *= -1;
            return true;
        }

        public void Pass()
        {
            throw new NotImplementedException();
        }

        public int GetDiscNumber(int disc) //石の数を取得(-1なら黒、1なら白、0なら空白の数を返す)
        {
            var count = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (Board[x, y] == disc)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool IsPossiblePoint(Point point, int turn)  //おける場所の判定
        {
            if (Board[point.x, point.y] != 0) return false;
            for (int i = 0; i < 8; i++)
            {
                if (point.x + dir[i].X < 0 || point.x + dir[i].X > 7 || point.y + dir[i].Y < 0 || point.y + dir[i].Y > 7)
                {
                    continue;
                }
                if (Board[point.x + (int)dir[i].X, point.y + (int)dir[i].Y] == -turn)
                {
                    int count = 1;
                    while (true)
                    {
                        count++;
                        if (point.x + dir[i].X * count < 0 || point.x + dir[i].X * count > 7 || point.y + dir[i].Y * count < 0 ||
                            point.y + dir[i].Y * count > 7)
                        {
                            break;
                        }
                        int color = Board[point.x + (int)dir[i].X * count, point.y + (int)dir[i].Y * count];
                        if (color == 0)
                        {
                            break;
                        }
                        if (color == turn)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public List<Point> GetPossiblePoints(int disc) //置ける場所
        {
            var res = new List<Point>();

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (IsPossiblePoint(new Point(i, j), disc))
                        res.Add(new Point(i, j));
                }
            if (res.Count == 0)
            {
                //TODO
            }
            //res.Add(new Point(1,1)); //(1,1)が置けると判ったら、左のをすればよい（それを過不足なく）
            //res.Add(new Point(x,y)

            return res;
        }

        public bool Back() //戻る
        {
            if (_count > 0)
            {
                _boardList.Remove(_boardList.Last()); //Listの最後を除去した
                _count--;
                Turn *= -1;
                return true;
            }
            else return false;
        }

        private int[,] CopyBoard(int[,] board)
        {
            var b = new int[8, 8];
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    b[x, y] = board[x, y];
                }
            }
            return b;
        }
    }
}
