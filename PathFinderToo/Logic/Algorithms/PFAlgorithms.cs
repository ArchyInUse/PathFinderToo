// Note: the reason this class is not in PathFinderToo.Logic.Algorithms is because it's a part of the PFViewModel class
// partial classes can't be spread out between namespaces

using PathFinderToo.Logic;
using PathFinderToo.Logic.Algorithms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Disables the UI components for the algorithms to run
        /// should be a bool property
        /// </summary>
        public void DisableAllUIComponents() { }

        private bool solved = false;

        /// <summary>
        /// fully async A* algorithm implementation
        /// </summary>
        /// <param name="curr">leave null</param>
        /// <returns>solved or not</returns>
        public async Task AStarAlgorithmAsync()
        {
            while(!solved)
            {
                await AStarAlgorithmSteppedAsync(CurrentState);
                await Task.Delay(50);
            }
        }

        /* IGNORE FOR NOW */
        private AlgorithmState CurrentState = null;
        private ObservableCollection<AlgorithmState> States = new ObservableCollection<AlgorithmState>();
        
        public async Task AStarAlgorithmSteppedAsync(AlgorithmState last = null)
        {
            // first time check
            if (last is null)
            {
                last = new AlgorithmState(SquaresList, new List<PFNode>() { PFNode.StartPoint }, PFNode.StartPoint, null);
                Debug.WriteLine($"Last:{Environment.NewLine}{last}");
                PFNode.StartPoint.Visited = true;
                States.Add(CurrentState);
            }
            else
            {
                PFNode.StartPoint.VisualType = VisualSquareType.StartEndPoint;
                PFNode.EndPoint.VisualType = VisualSquareType.StartEndPoint;
                last = CurrentState;
            }
            // end condition
            if (last.CurrentlyChecking == PFNode.EndPoint)
            {
                Debug.WriteLine($"Solution found");
                solved = true;
                return;
            }
            Debug.WriteLine($"Last checked square is not the endpoint, continue");

            // calculate current square
            PFNode currentlyChecking = last.Available[0];
            Debug.WriteLine($"Currently checking the square {currentlyChecking}");
            
            currentlyChecking.Visited = true;

            // could be wrapped
            List<PFNode> sorroundings = await CalculateSquareSorroundings(currentlyChecking);
            Debug.WriteLine($"Got sorroundings of {currentlyChecking}:");
            sorroundings = RemoveVisited(sorroundings);
            OutputSquaresList(sorroundings);
            sorroundings.ForEach(x => x.VisualType = VisualSquareType.Sorrounding);
            Debug.WriteLine($"Removed visited squares");
            last.Available.Remove(currentlyChecking);
            last.Available = last.Available.Distinct(new AStarSquareComparer()).ToList();
            
            var newAvailable = new List<PFNode>(last.Available);
            newAvailable.AddRange(sorroundings);
            RemoveVisited(newAvailable);
            newAvailable.Sort(new AStarSquareComparer());
            Debug.WriteLine($"Added last's sorroundings and removed all visited.");
            CurrentState = new AlgorithmState(SquaresList, newAvailable, currentlyChecking, last);
            States.Add(CurrentState);
            last.CurrentlyChecking.VisualType = VisualSquareType.Visited;
            Debug.WriteLine("");
            Debug.WriteLine($"CurrentAmountOfSquares = {newAvailable.Count}");
        }

        #region Helpers

        private void OutputSquaresList(List<PFNode> Sorroundings)
        {
            foreach(var s in Sorroundings)
            {
                Debug.WriteLine(s);
            }
        }

        private bool SquareIsWalkable(PFNode s) => s.Type != SquareType.Bomb && s.Type != SquareType.Wall;
        
        private List<PFNode> RemoveVisited(List<PFNode> list)
        {
            var newList = new List<PFNode>(list);
            foreach(var s in list)
            {
                if (s.Visited)
                {
                    newList.Remove(s);
                }
            }
            return newList;
        }

        private async Task<int> GreedySearchSizeOnly(PFNode start, PFNode goal, int count = 0)
        {
            if (start == goal) return count;

            List<PFNode> sorroundings = await CalculateSquareSorroundings(start, false);
            return 0;
        }

        #region A*

        // TODO: update UI components affected (put it in Calculate function for each of the squares)
        private async Task<List<PFNode>> CalculateSquareSorroundings(PFNode square, bool calcCosts = true)
        {
            var x = square.X;
            var y = square.Y;
            List<Task> tasks = new List<Task>();
            List<PFNode> toR = new List<PFNode>();
            // this list is any square sorrounding the square that we need to check
            List<(int, int)> toCheck = new List<(int, int)>()
            {
                (x + 1, y + 1),
                (x, y + 1),
                (x - 1, y + 1),
                (x + 1, y),
                (x, y),
                (x - 1, y),
                (x + 1, y - 1),
                (x, y - 1),
                (x - 1, y - 1),
            };

            // because we are removing elements in toCheck I'd like to avoid using the same list
            foreach (var t in toCheck.ToList())
            {
                if (t.Item1 < 0 || t.Item1 > 52 || t.Item2 < 0 || t.Item2 > 52)
                {
                    toCheck.Remove(t);
                }
                else if (!SquareIsWalkable(SquaresList[t.Item1 * 53 + t.Item2]))
                {
                    toCheck.Remove(t);
                }
            }

            if (calcCosts)
            {
                foreach (var t in toCheck)
                {
                    PFNode curr = SquaresList[t.Item1 * 53 + t.Item2];
                    tasks.Add(Task.Run(curr.AStarCalculateCosts));
                    toR.Add(curr);
                }
            }
            else
            {
                foreach(var k in toCheck)
                {
                    toR.Add(SquaresList[k.Item1 * 53 + k.Item2]);
                }
                return toR;
            }
            

            await Task.WhenAll(tasks);
            return toR;
        }

        #endregion

        #endregion
    }
}
