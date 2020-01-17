using PathFinderToo.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Disables the UI components for the algorithms to run
        /// </summary>
        public void DisableAllUIComponents() { }

        public async void AStarAlgorithmAsync()
        {
            // calculate all costs
            List<Task> tasks = new List<Task>();
            
            Task t = Task.WhenAll(tasks);
            await t;


        }

        #region Helpers

        public async Task CheckSquareSorroundings(PFSquare square)
        {
            var x = square.X;
            var y = square.Y;
            List<Task> tasks = new List<Task>();
            var jaggedSquares = SquaresList.To2D();
            // this list is any square sorrounding the square that we need to check
            List<(int, int)> toCheck = new List<(int, int)>()
            {
                (x + 1, y + 1),
                (x, y + 1),
                (x - 1, y + 1),
                (x + 1, y),
                (x - 1, y),
                (x + 1, y - 1),
                (x, y - 1),
                (x - 1, y - 1),
            };
            
            // because we are removing elements in toCheck I'd like to avoid using the same list
            foreach(var t in toCheck.ToList())
            {
                if(t.Item1 < 0 || t.Item1 > 52 || t.Item2 < 0 || t.Item2 > 52)
                {
                    toCheck.Remove(t);
                }
                else if(jaggedSquares[t.Item1, t.Item2].Type == SquareType.Empty)
                {

                }
            }

            foreach(var t in toCheck)
            {
                tasks.Add(Task.Run(jaggedSquares[t.Item1, t.Item2].CalculateCosts));
                
            }

            await Task.WhenAll(tasks);
        }

        #endregion
    }
}
