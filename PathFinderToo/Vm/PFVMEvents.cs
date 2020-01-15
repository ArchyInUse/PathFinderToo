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
        public void PanelMouseDown(object sender, MouseButtonEventArgs args)
        {
            MousePos = PFExtentions.GetMousePosition();
        }

        public void PanelMouseMove(object sender, MouseEventArgs args)
        {
            MousePos = PFExtentions.GetMousePosition();
        }
        #endregion

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
        }

        private void WallButtonClick() => EditingState = EditingState.Wall;
        private void SPButtonClick() => EditingState = EditingState.StartPoint;
        private void EPButtonClick() => EditingState = EditingState.EndPoint;
    }
}
