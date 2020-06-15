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

        public static SquareType GetTypeFromVisual(this VisualSquareType type)
        {
            switch(type)
            {
                case VisualSquareType.StartPoint:
                    return SquareType.StartPoint;
                case VisualSquareType.EndPoint:
                    return SquareType.EndPoint;
                case VisualSquareType.Empty:
                case VisualSquareType.Visited:
                case VisualSquareType.Sorrounding:
                    return SquareType.Empty;
                case VisualSquareType.Bomb:
                    return SquareType.Bomb;
                case VisualSquareType.Wall:
                    return SquareType.Wall;
                case VisualSquareType.StrongEmpty:
                    return SquareType.StrongEmpty;
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
                case VisualSquareType.StrongEmpty:
                    return new SolidColorBrush(Colors.DarkMagenta);
                case VisualSquareType.Visited:
                    return new SolidColorBrush(Colors.Red);
                case VisualSquareType.Sorrounding:
                    return new SolidColorBrush(Colors.Green);
                case VisualSquareType.StartPoint:
                    return new SolidColorBrush(Colors.Coral);
                case VisualSquareType.EndPoint:
                    return new SolidColorBrush(Colors.Cyan);
                case VisualSquareType.FinishPath:
                    return new SolidColorBrush(Colors.Yellow);
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
