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
    }
}
