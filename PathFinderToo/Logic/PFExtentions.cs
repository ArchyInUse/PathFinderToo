using PathFinderToo.Vm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathFinderToo.Logic
{
    /// <summary>
    /// A class containing extension and helper methods commonly used but not needed in the ViewModel
    /// </summary>
    public static class PFExtentions
    {
        public static void ResetBoard(this ObservableCollection<PFNode> board)
        {
            MainWindow.UiCtx.Send(x => board.Clear(), null);
            for (int i = 0; i < 53; i++)
            {
                for (int j = 0; j < 53; j++)
                {
                    MainWindow.UiCtx.Send(x => board.Add(new PFNode(i, j)), null);
                }
            }
            
            PFNode.EndPoint = new PFNode(-1, -1);
            PFNode.StartPoint = new PFNode(-1, -1);
        }

        // TODO: Test this
        public static PFNode[,] To2D(this ObservableCollection<PFNode> board)
        {
            PFNode[,] toR = new PFNode[53, 53];
            for(int i = 0; i < 53; i++)
            {
                int mult = i;
                if (mult == 0) mult = 1;

                for(int j = 0; j < 53; j++)
                {
                    toR[i,j] = board[mult * j];
                }
            }
            return toR;
        }

        public static SolidColorBrush GetColor(this SquareType type)
        {
            switch(type)
            {
                case SquareType.Empty:
                    return new SolidColorBrush(Colors.LightGray);
                case SquareType.EndPoint:
                case SquareType.StartPoint:
                    return new SolidColorBrush(Colors.Turquoise);
                case SquareType.Wall:
                    return new SolidColorBrush(Colors.Black);
                default:
                    return new SolidColorBrush(Colors.Green);
            }
        }

        public static SquareType GetTypeFromVisual(this VisualSquareType type)
        {
            switch(type)
            {
                case VisualSquareType.Empty:
                case VisualSquareType.Visited:
                case VisualSquareType.Sorrounding:
                    return SquareType.Empty;
                case VisualSquareType.Bomb:
                    return SquareType.Bomb;
                case VisualSquareType.Wall:
                    return SquareType.Wall;
                default:
                    return SquareType.Empty;
            }
        }

        public static SolidColorBrush GetColor(this VisualSquareType type)
        {
            switch(type)
            {
                case VisualSquareType.Empty:
                    return new SolidColorBrush(Colors.LightGray);
                case VisualSquareType.Wall:
                    return new SolidColorBrush(Colors.Black);
                case VisualSquareType.Visited:
                    return new SolidColorBrush(Colors.Red);
                case VisualSquareType.Sorrounding:
                    return new SolidColorBrush(Colors.Green);
                case VisualSquareType.StartEndPoint:
                    return new SolidColorBrush(Colors.Cyan);
                default:
                    return new SolidColorBrush(Colors.Turquoise);
            }
        }

        public static List<T> From<T>(this List<T> list, int startIndex, int endIndex = -1)
        {
            if (endIndex == -1) endIndex = list.Count;
            if (startIndex > list.Count) throw new ArgumentException();
            List<T> returnValue = new List<T>();

            for(; startIndex < endIndex; startIndex++)
            {
                returnValue.Add(list[startIndex]);
            }

            return returnValue;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Point
        {
            public int X;
            public int Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
    }
}
