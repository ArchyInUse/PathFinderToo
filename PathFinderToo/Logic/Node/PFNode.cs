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
using PathFinderToo.Vm;

namespace PathFinderToo.Logic
{
    public partial class PFNode : INotifyPropertyChanged
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
        
        public Thickness Margin { get; set; } = new Thickness(0.3, 0.3, 0, 0);
        public SquareType Type { get; set; }

        private VisualSquareType visualType;
        public VisualSquareType VisualType
        {
            get => visualType;
            set
            {
                visualType = value;
                Type = visualType.GetTypeFromVisual();
                NotifyPropertyChanged();
            }
        }

        #region Basic Algorithm Components

        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }
        public int Cost { get; set; } = 2;
        public static PFNode StartPoint;
        public static PFNode EndPoint;

        #endregion

        #region A* Path Finding Algorithm Components

        public PFNode PreviousNode { get; set; } = null;

        /// Distance from starting node
        public double GCost { get; set; }

        /// Distance from end node
        public double HCost { get; set; }
        
        /// Distance from starting to end node combined to recieve a general cost of going to that block
        public int FCost { get
            {
                return (int)(GCost * 10) + (int)(HCost * 10);
            } }
        
        public void AStarCalculateCosts()
        {
            HCost = CalculateHCost();
            var node = this;
            double currentCost = 0;
            while (!(node is null))
            {
                currentCost += node.Cost;
                node = node.PreviousNode;
            }
            GCost = currentCost;
        }
        
        public void DjikstrasCalculateCosts()
        {
            GCost = 0;
            HCost = CalculateHCost();
        }

        private double CalculateHCost()
        {
            var x1 = EndPoint.X;
            var x2 = X;
            var y1 = EndPoint.Y;
            var y2 = Y;

            return Distance(x1, x2, y1, y2);
        }

        private double Distance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

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

        public PFNode()
        {
            Fill = new SolidColorBrush(Colors.LightGray);
            InitEvents();
        }

        public PFNode(PFNode other)
        {
            VisualType = other.VisualType;
            X = other.X;
            Y = other.Y;
            Visited = other.Visited;
            Cost = other.Cost;

            InitEvents();
        }

        public PFNode(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        #region Overrides

        public static bool operator==(PFNode p1, PFNode p2)
        {
            if (p1 is null || p2 is null) return false;
            return (p1.X, p1.Y) == (p2.X, p2.Y);
        }

        public static bool operator !=(PFNode p1, PFNode p2) => !(p1 == p2);

        public override bool Equals(object obj)
        {
            if(obj is PFNode toCompare)
                return toCompare == this;
            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"({X}, {Y}) with FCost {FCost}";

        #endregion

        public void SetType(SquareType type)
        {
            Type = type;
        }

        public void SetVisualType(VisualSquareType type)
        {
            Fill = type.GetColor();
        }
    }
}
