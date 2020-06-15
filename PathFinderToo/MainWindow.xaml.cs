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
            var window = GetWindow(this);
            window.KeyDown += OnKeyPress;
            Vm = new PFViewModel(ItemsPanel);
            DataContext = Vm;
        }

        private void Reset()
        {
            Vm = new PFViewModel(ItemsPanel);
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

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }
    }
}
