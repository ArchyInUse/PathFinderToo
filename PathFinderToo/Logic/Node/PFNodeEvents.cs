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
    public partial class PFNode : INotifyPropertyChanged
    {
        public Command OnMouseEnterCommand { get; set; }
        public Command OnMouseDownCommand { get; set; }
        public Command OnMouseRightCommand { get; set; }

        private void InitEvents()
        {
            OnMouseEnterCommand = new Command(OnMouseEnter);
            OnMouseDownCommand = new Command(OnMouseDown);
            OnMouseRightCommand = new Command(OnRightMouseDown);
        }

        private void OnMouseEnter()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                OnMouseDown();
            else if (Mouse.RightButton == MouseButtonState.Pressed)
                OnRightMouseDown();
        }

        private void OnMouseDown()
        {
            if(Mouse.RightButton == MouseButtonState.Pressed)
            {
                OnRightMouseDown();
                return;
            }

            switch (PFViewModel.EditingState)
            {
                case EditingState.Wall:
                    VisualType = VisualSquareType.Wall;
                    break;
                case EditingState.StartPoint:
                    VisualType = VisualSquareType.StartPoint;
                    if((X,Y) != (StartPoint.X, StartPoint.Y))
                    {
                        StartPoint.VisualType = VisualSquareType.Empty;
                        StartPoint = this;
                    }
                    break;
                case EditingState.EndPoint:
                    VisualType = VisualSquareType.EndPoint;
                    if ((X,Y) != (EndPoint.X, EndPoint.Y))
                    {
                        EndPoint.VisualType = VisualSquareType.Empty;
                        EndPoint = this;
                    }
                    break;
                case EditingState.Bomb:
                    VisualType = VisualSquareType.Bomb;
                    break;
                case EditingState.StrongEmpty:
                    VisualType = VisualSquareType.StrongEmpty;
                    Cost = 2;
                    break;
            }

            // if the a start/end point was overwritten, reset them
            if(PFViewModel.EditingState != EditingState.EndPoint && PFViewModel.EditingState != EditingState.StartPoint)
            {
                if ((X, Y) == (StartPoint.X, StartPoint.Y))
                    StartPoint = new PFNode(-1, -1);
                else if ((X, Y) == (EndPoint.X, EndPoint.Y))
                    EndPoint = new PFNode(-1, -1);
            }
        }

        private void OnRightMouseDown()
        {
            Cost = 1;
            VisualType = VisualSquareType.Empty;
        }
    }
}
