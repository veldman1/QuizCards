using System;
using System.Windows.Input;

namespace QuizCards
{
    public class SimpleCommand : ICommand
    {
        private Action<object> execute;

        public SimpleCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}