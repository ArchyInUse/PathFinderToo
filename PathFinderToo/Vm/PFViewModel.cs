using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using PathFinderToo.Logic;
using PathFinderToo.Logic.Algorithms;
using Wpf.Controls.PanAndZoom;

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

        public PFViewModel(ItemsControl itemsControl, ZoomBorder zoomBorder)
        {
            SquaresList = new ObservableCollection<PFNode>();
            SquaresList.ResetBoard();
            SquaresItemControl = itemsControl;
            SquaresItemControl.MouseDown += PanelMouseDown;
            SquaresItemControl.MouseMove += PanelMouseMove;
            // this function is in PFVMEvents.cs
            InitEvents();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
