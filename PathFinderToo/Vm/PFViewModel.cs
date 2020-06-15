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
using PathFinderToo.Logic.Algorithms;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        public ItemsControl SquaresItemControl { get; set; }
        public ObservableCollection<PFNode> SquaresList { get; set; }

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

        private bool steppedMode = false;
        public bool SteppedMode
        {
            get => steppedMode;
            set
            {
                steppedMode = value;
                NotifyPropertyChanged();
            }
        }

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

        private int step = 1;
        public int Step
        {
            get => step;
            set
            {
                step = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxStep { get; set; } = 1;

        public PFViewModel(ItemsControl itemsControl)
        {
            SquaresList = new ObservableCollection<PFNode>();
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
