using System;
using System.ComponentModel;
using System.Windows.Input;
using FinalCalc.Tools;

namespace FinalCalc.ViewModel
{
    public class ExampleViewModel : INotifyPropertyChanged
    {
        private bool _canExecuteCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ExampleViewModel()
        {
            // RelayCommand�� �ʱ�ȭ�� �� canExecute�� �����մϴ�.
            MyCommand = new RelayCommand(ExecuteCommand, CanExecuteCommand);
        }

        public ICommand MyCommand { get; }

        public bool CanExecuteCommand()
        {
            // Ư�� ������ ������ ���� true�� ��ȯ�մϴ�.
            return _canExecuteCommand;
        }

        public void ExecuteCommand()
        {
            // ������ �۾��� �����մϴ�.
            Console.WriteLine("Command Executed");
        }

        public bool CanExecuteCommandProperty
        {
            get => _canExecuteCommand;
            set
            {
                _canExecuteCommand = value;
                OnPropertyChanged(nameof(CanExecuteCommandProperty));
                // CanExecuteChanged �̺�Ʈ�� �߻����� CanExecute �޼��带 �ٽ� ���մϴ�.
                CommandManager.InvalidateRequerySuggested();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}