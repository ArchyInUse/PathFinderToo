using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

namespace PathFinderToo.Logic
{
    public partial class PFSquare : INotifyPropertyChanged
    {
        private SolidColorBrush fill;
        public SolidColorBrush Fill
        {
            get => fill;
            set
            {
                fill = value;
                NotifyPropertyChanged();
            }
        }
        
        public int Width { get; set; } = 15;
        public int Height { get; set; } = 15;
        public Thickness Margin { get; set; } = new Thickness(0.3, 0.3, 0, 0);
        public SquareType Type { get; set; }
        #region Basic Algorithm Components
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }
        public static PFSquare StartPoint;
        public static PFSquare EndPoint;
        #endregion
        #region A* Path Finding Algorithm Components
        /// <summary>
        /// Distance from starting node
        /// </summary>
        public double GCost { get; set; }
        /// <summary>
        /// Distance from end node
        /// </summary>
        public double HCost { get; set; }
        /// <summary>
        /// Distance from starting to end node combined to recieve a general cost of going to that block
        /// </summary>
        public int FCost { get
            {
                return (int)(GCost * 10) + (int)(HCost * 10);
            } }
        #endregion
        #region Djikstra's Path Finding Algorithm Components

        #endregion

        #region INotifyPropertyChanged
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public PFSquare()
        {
            Fill = new SolidColorBrush(Colors.LightGray);
            InitEvents();
        }

        public PFSquare(int x, int y) : this()
        {
            X = x;
            Y = y;
        }
    }
}
