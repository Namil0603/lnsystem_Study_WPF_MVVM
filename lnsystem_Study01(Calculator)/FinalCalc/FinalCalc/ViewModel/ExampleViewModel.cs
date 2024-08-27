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
            // RelayCommand를 초기화할 때 canExecute를 전달합니다.
            MyCommand = new RelayCommand(ExecuteCommand, CanExecuteCommand);
        }

        public ICommand MyCommand { get; }

        public bool CanExecuteCommand()
        {
            // 특정 조건을 만족할 때만 true를 반환합니다.
            return _canExecuteCommand;
        }

        public void ExecuteCommand()
        {
            // 실행할 작업을 정의합니다.
            Console.WriteLine("Command Executed");
        }

        public bool CanExecuteCommandProperty
        {
            get => _canExecuteCommand;
            set
            {
                _canExecuteCommand = value;
                OnPropertyChanged(nameof(CanExecuteCommandProperty));
                // CanExecuteChanged 이벤트를 발생시켜 CanExecute 메서드를 다시 평가합니다.
                CommandManager.InvalidateRequerySuggested();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}