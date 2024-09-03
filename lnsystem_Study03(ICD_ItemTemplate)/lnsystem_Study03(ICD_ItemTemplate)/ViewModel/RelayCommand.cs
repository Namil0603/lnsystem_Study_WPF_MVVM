using System.Windows.Input;

namespace lnsystem_Study03_ICD_ItemTemplate_.ViewModel
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
            // 'execute'가 null이면 ArgumentNullException을 던져 예외를 발생시키고, 그렇지 않으면 'this.execute'에 'execute'를 할당합니다.
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            this.execute = execute;

            // 'canExecute'가 null이 아니면 'this.canExecute'에 'canExecute'를 할당합니다.
            this.canExecute = canExecute;
        }

        // ICommand.CanExecute 구현
        // 명령이 실행 가능한지 여부를 결정합니다.
        // 'canExecute'가 null이면 true를 반환하고, 그렇지 않으면 'canExecute'의 결과를 반환합니다.
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            return canExecute();
        }

        // ICommand.Execute 구현
        // 명령을 실행합니다.
        // 'execute'를 호출하여 작업을 실행합니다.
        public void Execute(object parameter)
        {
            execute();
        }

        // ICommand.CanExecuteChanged 구현
        // CanExecute의 결과가 변경될 때 발생하는 이벤트입니다.
        // CommandManager.RequerySuggested 이벤트에 핸들러를 추가하거나 제거합니다.
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}