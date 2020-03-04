using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PathFinderToo.Logic;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        #region Temporary Events
        // make these perma events (using commands and interactions)
        public void PanelMouseDown(object sender, MouseButtonEventArgs args)
        {
            MousePos = PFExtentions.GetMousePosition();
        }

        public void PanelMouseMove(object sender, MouseEventArgs args)
        {
            MousePos = PFExtentions.GetMousePosition();
        }
        #endregion

        public Command StartButtonClickCommand { get; set; }
        public Command WallButtonClickCommand { get; set; }
        // SP stands for Start Point
        public Command SPButtonClickCommand { get; set; }
        // EP stands for End Point
        public Command EPButtonClickCommand { get; set; }

        private void InitEvents()
        {
            WallButtonClickCommand = new Command(WallButtonClick);
            SPButtonClickCommand = new Command(SPButtonClick);
            EPButtonClickCommand = new Command(EPButtonClick);
            StartButtonClickCommand = new Command(AlgorithmButtonClick);
        }

        private void WallButtonClick() => EditingState = EditingState.Wall;
        private void SPButtonClick() => EditingState = EditingState.StartPoint;
        private void EPButtonClick() => EditingState = EditingState.EndPoint;
        private async void AlgorithmButtonClick()
        {
            if(PFNode.StartPoint.X == -1 || PFNode.EndPoint.Y == -1)
            {
                throw new Exception("Endpoint or Startpoint not set.");
            }
            //await AStarAlgorithmSteppedAsync(CurrentState);
            await AStarAlgorithmAsync();
        }
    }
}
