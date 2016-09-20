using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Text.Core;

namespace MakeOthello.Model
{
    class Othello : IOthello
    {

        // 前盤面を格納するリスト
        private List<int[,]> _boardList = new List<int[,]>();

        // 置ける場所を保管しておくリスト
        private List<Point> _possiblePoints;

        /// <summary>
        /// ゲーム終了時に発生するイベント
        /// </summary>
        public event OthelloEndEventHandler EndEvent;

        /// <summary>
        /// 石が置けなくなった場合に発生するイベント
        /// </summary>
        public event OthelloPassEventHandler PassEvent;


        public Othello()
        {
            _boardList.Add(new int[8, 8]);
        }

        /// <summary>
        /// 現在の盤面
        /// </summary>
        public int[,] Board
        {
            get { return _boardList[Count]; }
            private set
            {
                _boardList[Count] = value;
            }
        }

        /// <summary>
        /// 盤面をクリアし、最初から始める
        /// </summary>
        public void Start()
        {
            _boardList.Clear();
            _boardList.Add(new int[8, 8]);
            Count = 0;
            Turn = -1;

            //最初に置いてある石
            Board[3, 4] = -1;
            Board[4, 3] = -1;
            Board[3, 3] = 1;
            Board[4, 4] = 1;
        }

        /// <summary>
        /// 黒番 -1 白番 1
        /// </summary>
        public int Turn { get; private set; }

        /// <summary>
        /// 0からはじまる番数
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 石を置く
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>

        public bool Put(Point point)
        {
            if (Board[point.x, point.y] != 0)  //空じゃないなら
                return false;                  //おけない
            // 置ける場所を変更する
            _possiblePoints = null;

            bool putFlag = false;
            for (int i = 0; i < 8; i++)
            {
                if (point.x + Dir[i].x < 0
                    || point.x + Dir[i].x > 7
                    || point.y + Dir[i].y < 0
                    || point.y + Dir[i].y > 7)
                    continue;
                if (Board[point.x + Dir[i].x, point.y + Dir[i].y] == -Turn)  //一つ隣が相手の色なら
                {
                    int k = 1; //ベクトルの係数
                    while (true)
                    {
                        k++;
                        if (point.x + k * Dir[i].x < 0
                            || point.x + k * Dir[i].x > 7
                            || point.y + k * Dir[i].y < 0
                            || point.y + k * Dir[i].y > 7)
                            break;
                        if (Board[point.x + k * Dir[i].x, point.y + k * Dir[i].y] == 0)
                            break;
                        if (Board[point.x + k * Dir[i].x, point.y + k * Dir[i].y] == Turn)
                        {
                            putFlag = true;
                            for (; k >= 1; k--)
                            {
                                Board[point.x + k * Dir[i].x, point.y + k * Dir[i].y] = Turn;
                            }
                            break;
                        }
                    }
                }
            }
            if (putFlag)
            {
                _boardList.Add(CopyBoard(Board));
                Count++;
                Board[point.x, point.y] = Turn;
                Turn *= -1;
                var points = GetPossiblePoints(Turn);

                if (points.Count == 0)
                {
                    var bnum = GetDiscNumber(-1);
                    var wnum = GetDiscNumber(1);
                    if (bnum + wnum < 64)
                    {
                        OnPassEvent(this, Turn);
                    }
                    else 
                    {
                        if (bnum == wnum)
                            OnEndEvent(this, 0);
                        else
                        {
                            OnEndEvent(this, Math.Max(bnum, wnum));
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 置かずにパスする（置ける場合も実行可能）
        /// </summary>

        //フィールドにパスカウントやっといて、２回連続なら判定するように
        //パスしなければ0に戻すようなかんじ
        public void Pass()
        {
            // 置ける場所を変更する
            _possiblePoints = null;
            _boardList.Add(CopyBoard(Board));
            Count++;
            Turn *= -1;             //次いって
            var points = GetPossiblePoints(Turn);
            if (points.Count == 0)  //また置ける場所が0だったら
            {
                var bnum = GetDiscNumber(-1);
                var wnum = GetDiscNumber(1);
                if (bnum == wnum)
                    OnEndEvent(this, 0);
                else
                {
                    OnEndEvent(this, Math.Max(bnum, wnum));
                }
            }
        }

        /// <summary>
        /// 石または空白の数を取得
        /// </summary>
        /// <param name="disc">石または空白（-1,0,1）</param>
        /// <returns></returns>
        public int GetDiscNumber(int disc)  //引数にいれた数字の個数　結果は、GetDiscNumber(-1)とGetDiscNumber(1)の大小比較
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

        /// <summary>
        /// 置けるPointのListを返す
        /// </summary>
        /// <param name="disc">置く石の色</param>
        /// <returns></returns>
        public List<Point> GetPossiblePoints(int disc)
        {
            if (_possiblePoints != null)
                return _possiblePoints;

            var res = new List<Point>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (IsPossiblePoint(new Point(i, j), disc))
                        res.Add(new Point(i, j));
                }
            return _possiblePoints = res;
        }

        /// <summary>
        /// 一手戻る
        /// </summary>
        /// <returns></returns>
        public bool Back()
        {
            // 置ける場所を変更する
            _possiblePoints = null;

            if (Count > 0)
            {
                // Listの最後を除去
                _boardList.Remove(_boardList.Last());
                Count--;
                Turn *= -1;
                return true;
            }
            else return false;
        }

        private static readonly Point[] Dir ={ new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1),
           new Point(-1, 0), new Point(-1, -1), new Point(0, -1), new Point(1, -1)};

        // pointにturnが置けるかどうかを返す
        private bool IsPossiblePoint(Point point, int turn)
        {
            if (Board[point.x, point.y] != 0)
            {
                return false;
            }
            for (var i = 0; i < 8; i++)
            {
                if (point.x + Dir[i].x < 0
                    || point.x + Dir[i].x > 7
                    || point.y + Dir[i].y < 0
                    || point.y + Dir[i].y > 7)
                {
                    continue;
                }
                if (Board[point.x + Dir[i].x, point.y + Dir[i].y] == -turn)
                {
                    var count = 1;
                    while (true)
                    {
                        count++;
                        if (point.x + Dir[i].x * count < 0
                            || point.x + Dir[i].x * count > 7
                            || point.y + Dir[i].y * count < 0
                            || point.y + Dir[i].y * count > 7)
                        {
                            break;
                        }
                        var color = Board[point.x + Dir[i].x * count, point.y + Dir[i].y * count];
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

        // 盤面の複製
        private static int[,] CopyBoard(int[,] board)
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

        protected virtual void OnEndEvent(IOthello othello, int result)
        {
            EndEvent?.Invoke(othello, result);
        }

        protected virtual void OnPassEvent(IOthello othello, int pass)
        {
            PassEvent?.Invoke(othello, pass);
        }
    }
}
