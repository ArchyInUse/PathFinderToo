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
using PathFinderToo.Vm.Algorithms;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        public ItemsControl SquaresItemControl { get; set; }
        public ObservableCollection<PFSquare> SquaresList { get; set; }

        // default editing state is Wall
        // static is for the squares to be able to use this property
        public static EditingState EditingState { get; set; } = 0;

        private AlgorithmType selectedAlgorithmType = AlgorithmType.None;
        public AlgorithmType SelectedAlgorithmType
        {
            get => selectedAlgorithmType;
            set
            {
                selectedAlgorithmType = value;
                NotifyPropertyChanged();
            }
        }

        public static bool SteppedMode = true;

        private Point mousePos;
        public Point MousePos
        {
            get => mousePos;
            set
            {
                mousePos = value;
                NotifyPropertyChanged();
            }
        }
        
        public PFViewModel(ItemsControl itemsControl)
        {
            SquaresList = new ObservableCollection<PFSquare>();
            SquaresList.ResetBoard();
            SquaresItemControl = itemsControl;
            SquaresItemControl.MouseDown += PanelMouseDown;
            SquaresItemControl.MouseMove += PanelMouseMove;
            InitEvents();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
