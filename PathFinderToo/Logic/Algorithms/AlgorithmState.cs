using PathFinderToo.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Logic.Algorithms
{
    /// <summary>
    /// this class represents the current history of the board to allow cycling through states and to allow stepped mode
    /// </summary>
    public class AlgorithmState
    {
        public ObservableCollection<PFNode> Squares { get; set; }
        public List<PFNode> Available { get; set; }
        public PFNode CurrentlyChecking { get; set; }
        public AlgorithmState Last { get; set; } = null;

        public AlgorithmState(ObservableCollection<PFNode> squares, List<PFNode> available, PFNode currentlyChecking, AlgorithmState last)
        {
            Squares = squares;
            CurrentlyChecking = currentlyChecking;
            Available = available;
            Last = last;
        }

        public override string ToString()
        {
            string str = "";
            str += $"State Status:{Environment.NewLine}";
            str += $"Available Squares:{Environment.NewLine}";
            Available.ForEach(x => str += $"{x}{Environment.NewLine}");
            str += $"Currently Checking: {CurrentlyChecking}";
            return str;
        }
    }
}
