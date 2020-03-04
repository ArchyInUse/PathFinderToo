using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Logic.Algorithms
{
    /// <summary>
    /// a custom list class that wraps all needed calculations for A* algorithm
    /// </summary>
    public class AStarAvailablityList : IAlgorithmicList
    {
        #region IEnumerable
        public IEnumerator<PFNode> GetEnumerator() => Available.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Available.GetEnumerator();
        #endregion

        private List<PFNode> Available { get; set; }

        public async Task Add(PFNode square)
        {
            if (!await Task.Run(() => Available.Contains(square)))
            {
                Available.Add(square);
            }
        }

        public async Task Remove(PFNode square) => await Task.Run(() => Available.Remove(square));

        public void SortedAdd(List<PFNode> newList)
        {
            // sort the new elements
            newList.Sort(new AStarSquareComparer());
            Queue<PFNode> newQueue = new Queue<PFNode>(newList);

            for(int i = 0, j = 0; i < newList.Count; i++)
            {
                if(newList[i].FCost <= Available[j].FCost && newList[i].FCost >= Available[j + 1].FCost)
                {
                    Available.Insert(j, newList[i]);
                    newQueue.Dequeue();
                }
                else
                    j++;
                if(j == Available.Count)
                    Available.AddRange(newQueue);
            }
        }
    }
}
