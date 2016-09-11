using System;
using System.Windows.Input;

namespace MakeOthello.Utility
{
    class SimpleCommand:ICommand
    {
        public SimpleCommand(Action<object> action)
        {
            _action = action;
        }

        private readonly Action<object> _action; 

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
