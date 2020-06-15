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
    public class AStarSquareComparer : IComparer<PFNode>, IEqualityComparer<PFNode>
    {
        public bool Equals(PFNode x, PFNode y) => x == y;

        public int GetHashCode(PFNode obj) => HashCode.Combine(obj.X, obj.Y);

        int IComparer<PFNode>.Compare(PFNode x, PFNode y)
        {
            if (x == y)
                return 0;

            if (x.FCost < y.FCost) return -1;
            else if (x.FCost > y.FCost) return 1;
            else return 0;
        }
    }
}
