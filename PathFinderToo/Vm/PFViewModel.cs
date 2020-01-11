using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PathFinderToo.Logic;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        public ItemsControl SquaresItemControl { get; set; }
        public ObservableCollection<Square> SquaresList { get; set; }

        private Point mousePos;
        public Point MousePos
        {
            get
            {
                return mousePos;
            }
            set
            {
                mousePos = value;
                NotifyPropertyChanged();
            }
        }
        
        public PFViewModel(ItemsControl itemsControl)
        {
            SquaresList = new ObservableCollection<Square>();
            SquaresList.SquarePopulate();
            SquaresItemControl = itemsControl;
            SquaresItemControl.MouseDown += PanelMouseDown;
            SquaresItemControl.MouseMove += PanelMouseMove;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
