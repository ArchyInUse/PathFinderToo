using PathFinderToo.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PathFinderToo.Vm;
using System.Threading;
using Wpf.Controls.PanAndZoom;
using System.Diagnostics;

namespace PathFinderToo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PFViewModel Vm;
        public static SynchronizationContext UiCtx;

        public MainWindow()
        {
            UiCtx = SynchronizationContext.Current;
            InitializeComponent();
            this.KeyDown += OnKeyPress;
            Vm = new PFViewModel(ItemsPanel, ZoomViewbox);
            DataContext = Vm;
        }

        private void Reset()
        {
            Vm = new PFViewModel(ItemsPanel, ZoomViewbox);
            DataContext = Vm;
        }

        private async void OnKeyPress(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Z)
            {
                Reset();
            }
            else if (args.Key == Key.Enter)
            {
                await Vm.AStarAlgorithmAsync();
            }
            else if (args.Key == Key.A)
            {
                await Vm.AStarAlgorithmAsync();
            }
        }

        /// <summary>
        /// This event's reason is for displaying the numeric values on each of the squares.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Vm.OnMouseWheel();
        }
    }
}
