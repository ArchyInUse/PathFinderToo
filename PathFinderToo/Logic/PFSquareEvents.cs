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
            switch (PFViewModel.EditingState)
            {
                case EditingState.Wall:
                    Fill = new SolidColorBrush(Colors.Black);
                    Type = SquareType.Wall;
                    break;
                case EditingState.StartPoint:
                    Fill = new SolidColorBrush(Colors.Blue);
                    Type = SquareType.StartPoint;
                    if((X,Y) != (StartPoint.X, StartPoint.Y))
                    {
                        StartPoint.Type = SquareType.Empty;
                        StartPoint.Fill = new SolidColorBrush(Colors.LightGray);
                        StartPoint = this;
                    }
                    break;
                case EditingState.EndPoint:
                    Fill = new SolidColorBrush(Colors.Green);
                    Type = SquareType.EndPoint;
                    if((X,Y) != (EndPoint.X, EndPoint.Y))
                    {
                        EndPoint.Type = SquareType.Empty;
                        EndPoint.Fill = new SolidColorBrush(Colors.LightGray);
                        EndPoint = this;
                    }
                    break;
                case EditingState.Bomb:
                    Fill = new SolidColorBrush(Colors.Black);
                    Type = SquareType.Bomb;
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
    }
}
