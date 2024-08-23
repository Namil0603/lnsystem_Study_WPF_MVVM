using System.Windows.Input;

namespace FinalCalc.Tools
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
            // execute가 null이면 ArgumentNullException을 던집니다.
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            // execute와 canExecute를 초기화합니다.
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // ICommand.CanExecute 구현
        public bool CanExecute(object parameter)
        {
            // canExecute가 null이면 true를 반환하고, 그렇지 않으면 canExecute()의 결과를 반환합니다.
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute();
            }
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