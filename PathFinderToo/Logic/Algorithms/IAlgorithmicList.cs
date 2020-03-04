using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Logic.Algorithms
{
    public interface IAlgorithmicList : IEnumerable<PFNode>
    {
        void SortedAdd(List<PFNode> newList);
        Task Add(PFNode square);
        Task Remove(PFNode square);
        
    }
}
