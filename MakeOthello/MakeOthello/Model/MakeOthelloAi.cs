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

        public MakeOthelloAi(int cpulevel)
        {
          SetLevel(cpulevel);
            
        }

        public override void Put(IOthello othello, List<Point> ablePoints)
        {
            if (othello.Count > finalLookTurn)
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
            switch (i)
            {
                case 1:
                    lookNum = 1;
                    finalLookTurn = 63 - 8;
                    break;
                case 2:
                    lookNum = 2;
                    finalLookTurn = 63 - 10;
                    break;
                case 3:
                    lookNum = 5;
                    finalLookTurn = 63 - 12;
                    break;
            }
        }
    }
}
