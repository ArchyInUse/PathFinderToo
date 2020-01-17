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
                    Type = SquareType.Wall;
                    Fill = Type.GetColor();
                    break;
                case EditingState.StartPoint:
                    Type = SquareType.StartPoint;
                    Fill = Type.GetColor();
                    if((X,Y) != (StartPoint.X, StartPoint.Y))
                    {
                        StartPoint.Type = SquareType.Empty;
                        StartPoint.Fill = Type.GetColor();
                        StartPoint = this;
                    }
                    break;
                case EditingState.EndPoint:
                    Type = SquareType.EndPoint;
                    Fill = Type.GetColor();
                    if ((X,Y) != (EndPoint.X, EndPoint.Y))
                    {
                        EndPoint.Type = SquareType.Empty;
                        EndPoint.Fill = Type.GetColor();
                        EndPoint = this;
                    }
                    break;
                case EditingState.Bomb:
                    Type = SquareType.Bomb;
                    Fill = Type.GetColor();
                    break;
            }

            // if the a start/end point was overwritten, reset them
            if(PFViewModel.EditingState != EditingState.EndPoint && PFViewModel.EditingState != EditingState.StartPoint)
            {
                if ((X, Y) == (StartPoint.X, StartPoint.Y))
                    StartPoint = new PFSquare(-1, -1);
                else if ((X, Y) == (EndPoint.X, EndPoint.Y))
                    EndPoint = new PFSquare(-1, -1);
            }
        }

        private void OnRightMouseDown()
        {
            Type = SquareType.Empty;
            Fill = new SolidColorBrush(Colors.LightGray);
        }
    }
}
