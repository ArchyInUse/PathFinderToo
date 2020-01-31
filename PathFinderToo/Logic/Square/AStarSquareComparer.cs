using System;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Logic
{
    /// <summary>
    /// this class sorts squares by their FCost
    /// </summary>
    public class AStarSquareComparer : IComparer<PFSquare>, IEqualityComparer<PFSquare>
    {
        public bool Equals(PFSquare x, PFSquare y)
        {
            return x == y;
        }

        public int GetHashCode(PFSquare obj)
        {
            return HashCode.Combine(obj.X, obj.Y);
        }

        int IComparer<PFSquare>.Compare(PFSquare x, PFSquare y)
        {
            if (x == y)
                return 0;

            if (x.FCost < y.FCost) return -1;
            else if (x.FCost > y.FCost) return 1;
            else return 0;
        }
    }
}
