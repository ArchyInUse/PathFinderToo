using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PathFinderToo.Logic;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        public void ResetBoard() => SquaresList.SquarePopulate();
    }
}
