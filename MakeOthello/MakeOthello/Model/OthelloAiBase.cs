using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    abstract class OthelloAiBase
    {
        public abstract void Put(IOthello othello, List<Point> ablePoints);

        public abstract void SetLevel(int i);

        public async Task PutAsync(IOthello othello, List<Point> ablePoints = null)
        {
            if (ablePoints == null)
                ablePoints = othello.GetPossiblePoints(othello.Turn);
            if (ablePoints.Count == 0)
            {
                othello.Pass();
                return;
            }
            await Task.Run((() => Put(othello, ablePoints)));
        }
    }
}
