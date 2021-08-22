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
using PathFinderToo.Logic.Algorithms;

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

        public Command DecreaseStepButtonCommand { get; set; }
        public Command IncreaseStepButtonCommand { get; set; }
        public Command StartButtonClickCommand { get; set; }
        public Command WallButtonClickCommand { get; set; }
        // SP stands for Start Point
        public Command SPButtonClickCommand { get; set; }
        // EP stands for End Point
        public Command EPButtonClickCommand { get; set; }
        public Command SEButtonClickCommand { get; set; }

        private void InitEvents()
        {
            WallButtonClickCommand = new Command(WallButtonClick);
            SPButtonClickCommand = new Command(SPButtonClick);
            EPButtonClickCommand = new Command(EPButtonClick);
            StartButtonClickCommand = new Command(AlgorithmButtonClick);
            DecreaseStepButtonCommand = new Command(DecreaseStepButtonClick);
            IncreaseStepButtonCommand = new Command(IncreaseStepButtonClick);
            SEButtonClickCommand = new Command(SEButtonClick);
        }
        
        /// <summary>
        /// Checks if zoom border's zoom is high/low enough to enable/disable the detailed values
        /// </summary>
        public void OnMouseWheel()
        {

        }
        private void DecreaseStepButtonClick()
        {
            if (Step > 1)
            {
                Step--;
                SquaresList = States[Step - 1].Squares;
                NotifyPropertyChanged(nameof(SquaresList));
            }
        }
        private void IncreaseStepButtonClick()
        {
            if (Step < MaxStep)
            {
                Step++;
                SquaresList = States[Step - 1].Squares;
            }
        }
        private async void AlgorithmButtonClick()
        {
            // check for start and end points
            if (PFNode.StartPoint.X == -1)
                throw new NodeNotSetException(PFNode.StartPoint);
            if (PFNode.EndPoint.Y == -1)
                throw new NodeNotSetException(PFNode.EndPoint);

            if(SteppedMode)
            {
                await AStarAlgorithmSteppedAsync(CurrentState);
            }
            else
            {
                await AStarAlgorithmAsync();
            }
        }
        private void SEButtonClick() => EditingState = EditingState.StrongEmpty;
        private void WallButtonClick() => EditingState = EditingState.Wall;
        private void SPButtonClick() => EditingState = EditingState.StartPoint;
        private void EPButtonClick() => EditingState = EditingState.EndPoint;
    }
}
