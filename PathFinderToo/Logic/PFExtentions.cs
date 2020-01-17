﻿using System;
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
        public static void SquarePopulate(this ObservableCollection<PFSquare> board)
        {
            board.Clear();
            for (int i = 0; i < 53; i++)
            {
                for (int j = 0; j < 53; j++)
                {
                    board.Add(new PFSquare(i, j));
                }
            }
            
            PFSquare.EndPoint = new PFSquare(-1, -1);
            PFSquare.StartPoint = new PFSquare(-1, -1);
        }

        // TODO: Test this
        public static PFSquare[,] To2D(this ObservableCollection<PFSquare> board)
        {
            PFSquare[,] toR = new PFSquare[53, 53];
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
                case SquareType.Bomb:
                    return new SolidColorBrush(Colors.Red);
                case SquareType.CheckedAndFailedPath:
                    return new SolidColorBrush(Colors.Orange);
                case SquareType.CurrentlyChecking:
                    return new SolidColorBrush(Colors.DarkBlue);
                case SquareType.CurrentlyCheckingPath:
                    return new SolidColorBrush(Colors.DarkCyan);
                case SquareType.Empty:
                    return new SolidColorBrush(Colors.LightGray);
                case SquareType.EndPoint:
                    return new SolidColorBrush(Colors.Turquoise);
                case SquareType.StartPoint:
                    return new SolidColorBrush(Colors.Wheat);
                case SquareType.Wall:
                    return new SolidColorBrush(Colors.Black);
                default:
                    return new SolidColorBrush(Colors.Green);
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
