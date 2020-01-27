using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Logic
{
    /// <summary>
    /// this class sorts squares by their FCost
    /// </summary>
    public class SquareComparer : IComparer<PFSquare>
    {
        int IComparer<PFSquare>.Compare(PFSquare x, PFSquare y)
        {
            if ((x.X, x.Y) == (y.X, y.Y))
                return 0;

            if (x.FCost < y.FCost) return -1;
            else if (x.FCost > y.FCost) return 1;
            else return 0;
        }
    }
}
