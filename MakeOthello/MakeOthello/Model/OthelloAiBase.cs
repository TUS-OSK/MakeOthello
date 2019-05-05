using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    public abstract class OthelloAiBase
    {
        public abstract void Put(IOthello othello, List<Point> ablePoints);

        public abstract void SetLevel(int i);

        public async Task PutAsync(IOthello othello, List<Point> ablePoints = null)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            if (ablePoints == null)
                ablePoints = othello.GetPossiblePoints(othello.Turn);
            if (ablePoints.Count == 0)
            {
                othello.Pass();
                return;
            }

            await Task.Run(() => Put(othello, ablePoints));

            sw.Stop();

            TimeSpan ts = TimeSpan.FromMilliseconds(1000) - sw.Elapsed;
            if (ts > TimeSpan.FromMilliseconds(0))
            {
                await Task.Delay(ts);
            }
        }
    }
}
