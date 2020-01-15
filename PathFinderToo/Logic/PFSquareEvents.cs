using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using PathFinderToo.Vm;

namespace PathFinderToo.Logic
{
    public partial class PFSquare : INotifyPropertyChanged
    {
        public Command OnMouseEnterCommand { get; set; }
        public Command OnMouseDownCommand { get; set; }

        private void InitEvents()
        {
            OnMouseEnterCommand = new Command(OnMouseEnter);
            OnMouseDownCommand = new Command(OnMouseDown);
        }

        private void OnMouseEnter()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                OnMouseDown();
        }

        private void OnMouseDown()
        {
            if (PFViewModel.EditingState == EditingState.Wall)
            {
                Fill = new SolidColorBrush(Colors.Red);
            } 
            else if(PFViewModel.EditingState == EditingState.StartPoint)
            {
                Fill = new SolidColorBrush(Colors.Blue);
            }
            else if(PFViewModel.EditingState == EditingState.EndPoint)
            {
                Fill = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
