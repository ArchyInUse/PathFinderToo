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

namespace PathFinderToo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PFViewModel Vm;

        public MainWindow()
        {
            InitializeComponent();
            var window = GetWindow(this);
            window.KeyDown += OnKeyPress;
            Vm = new PFViewModel(ItemsPanel);
            DataContext = Vm;
        }

        private void OnKeyPress(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Z)
            {
                Vm.ResetBoard();
            }
        }
    }
}
