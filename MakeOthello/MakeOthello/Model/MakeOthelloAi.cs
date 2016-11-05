﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class MakeOthelloAi:OthelloAiBase
    {
        public int finalLookTurn { get; set; }
        public int lookNum { get; set; }

        public MakeOthelloAi()
        {  
            lookNum = 4;

            finalLookTurn = 46;
        }

        public override void Put(IOthello othello, List<Point> ablePoints)
        {
            if (othello.Turn > finalLookTurn)
            {
                var look = new FinalMakeLookAhead();
                othello.Put(look.LookAhead(othello.Clone(),0));
            }
            else
            {
                var look = new MakeLookAhead();
                othello.Put(look.LookAhead(othello.Clone(), lookNum));
            }
        }

        public override void SetLevel(int i)
        {
            throw new NotImplementedException();
        }
    }
}
