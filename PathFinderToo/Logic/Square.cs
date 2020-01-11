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

namespace PathFinderToo.Logic
{
    public partial class Square : INotifyPropertyChanged
    {
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get => rectangle;
            set
            {
                rectangle = value;
                NotifyPropertyChanged();
            }
        }

        public int Width { get; set; } = 15;
        public int Height { get; set; } = 15;
        public SquareType Type { get; set; }
        public Command OnMouseEnterCommand { get; set; }

        #region INotifyPropertyChanged
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public Square(Rectangle rectangle)
        {
            Rectangle = rectangle;
            OnMouseEnterCommand = new Command(OnMouseEnter);
        }

        private void OnMouseEnter()
        {
            Rectangle.Fill = new SolidColorBrush(Colors.Red);
        }
    }
}
