using System;
using System.Collections.Generic;
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

        public async void AStarAlgorithm()
        {
            // calculate all costs
            List<Task> tasks = new List<Task>();
            foreach(var s in SquaresList)
            {
                tasks.Add(Task.Run(s.CalculateCosts));
            }
            Task t = Task.WhenAll(tasks);
            await t;


        }
    }
}
