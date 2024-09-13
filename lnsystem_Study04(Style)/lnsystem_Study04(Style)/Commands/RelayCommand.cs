using System;
using System.Windows;
using System.Windows.Input;

namespace lnsystem_Study04_Style_.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            if (execute == null)
            {
                MessageBox.Show("실행할 작업이 null일 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}