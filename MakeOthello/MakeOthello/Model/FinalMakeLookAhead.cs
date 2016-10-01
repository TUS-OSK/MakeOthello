using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOthello.Model
{
    class FinalMakeLookAhead : ILookAhead
    {
        public FinalMakeLookAhead()
        {

        }
        public Point LookAhead(IOthello Othello, int turn)
        {
            List<int> scoreList = new List<int>();
            for(int i = 0; i < Othello.Count; i++)
            {
                scoreList.Add(0);
                //Othello.Put(Othello);
                switch (turn)
                {

                }
            }
            throw new NotImplementedException();
        }

    }
}
