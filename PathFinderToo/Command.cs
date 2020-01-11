using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PathFinderToo
{
    public class Command : ICommand
    {
        private Action _action;
        private Func<bool> _canExcecute;

        public Command(Action action, Func<bool> canExececute = null)
        {
            _action = action;
            if (canExececute is null) canExececute = DefaultCanExcecute;
            _canExcecute = canExececute;
        }

        private static bool DefaultCanExcecute() => true;
        public bool CanExecute(object parameter) => _canExcecute.Invoke();
        public void Execute(object parameter) => _action.Invoke();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
