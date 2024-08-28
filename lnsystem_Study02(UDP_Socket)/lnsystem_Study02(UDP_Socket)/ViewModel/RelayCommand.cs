using System.Windows.Input;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public class RelayCommand : ICommand
    {
        // 실행할 작업을 나타내는 Action
        private readonly Action execute;
        // 실행 가능 여부를 결정하는 Func
        private readonly Func<bool> canExecute;

        // 생성자
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        // ICommand.CanExecute 구현
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        // ICommand.Execute 구현
        public void Execute(object parameter)
        {
            execute();
        }

        // ICommand.CanExecuteChanged 구현
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}