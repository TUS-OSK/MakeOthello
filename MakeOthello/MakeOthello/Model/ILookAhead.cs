using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    interface ILookAhead
    {
        Point LookAhead(IOthello Othello, int turn);
        //MakeLookAhead,FinalMakeLookAheadを継承
    }
}
