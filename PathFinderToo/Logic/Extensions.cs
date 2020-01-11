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
    public static class Extensions
    {
        public static void SquarePopulate(this ObservableCollection<Square> panelCollection)
        {
            panelCollection.Clear();
            for (int i = 0; i < 53; i++)
            {
                for (int j = 0; j < 53; j++)
                {
                    Rectangle rec = new Rectangle()
                    {
                        Fill = new SolidColorBrush(Colors.LightGray),
                        Height = 15,
                        Width = 15,
                        Margin = new Thickness(0.3, 0.3, 0, 0)
                    };

                    panelCollection.Add(new Square(rec));
                }
            }
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
