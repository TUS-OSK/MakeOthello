using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class SampleOthelloAi : OthelloAiBase
    {
        public override void Put(IOthello othello, List<Point> ablePoints)
        {
            othello.Put(ablePoints.First());
        }

        public override void SetLevel(int i)
        {
            // Do Nothing
        }
    }
}
