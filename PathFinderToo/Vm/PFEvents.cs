using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PathFinderToo.Logic;

namespace PathFinderToo.Vm
{
    public partial class PFViewModel : INotifyPropertyChanged
    {
        public void PanelMouseDown(object sender, MouseButtonEventArgs args)
        {
            MousePos = Extensions.GetMousePosition();
        }

        public void PanelMouseMove(object sender, MouseEventArgs args)
        {
            MousePos = Extensions.GetMousePosition();
        }

        public void OnMouseEnter(object sender, MouseEventArgs args)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var rec = VisualTreeHelper.GetChild(sender as ContentPresenter, 0) as Rectangle;
                rec.Fill = new SolidColorBrush(Colors.Red);
            }
        }
        
        public void OnMouseDown(object sender, MouseEventArgs args)
        {
            var rec = VisualTreeHelper.GetChild(sender as ContentPresenter, 0) as Rectangle;
            rec.Fill = new SolidColorBrush(Colors.Red);
        }
    }
}
