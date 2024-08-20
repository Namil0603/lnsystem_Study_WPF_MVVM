using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewNewCalc.Tool;

namespace NewNewCalc.ViewModel
{
    public class VMCalculatorView : INotifyPropertyChanged
    {
        public ICommand Input0 { get; }
        public ICommand Input1 { get; }
        public ICommand Input2 { get; }
        public ICommand Input3 { get; }
        public ICommand Input4 { get; }
        public ICommand Input5 { get; }
        public ICommand Input6 { get; }
        public ICommand Input7 { get; }
        public ICommand Input8 { get; }
        public ICommand Input9 { get; }
        public ICommand InputAdd { get; }
        public ICommand InputSubtract { get; }
        public ICommand InputMultiply { get; }
        public ICommand InputDivide { get; }
        public ICommand InputRemove { get; }
        public ICommand InputResult { get; }
        public ICommand InputClear { get; }
        public ICommand InputDecimal { get; }

        private string _currentInputField;
        private string _lastInput;
        private string _currentOperator;

        public string CurrentInputField
        {
            get => _currentInputField;
            set => SetField(ref _currentInputField, value);
        }
        public string LastInput
        {
            get => _lastInput;
            set => SetField(ref _lastInput, value);
        }

        public VMCalculatorView()
        {
            Input0 = new RelayCommand(() => CurrentInputField += "0");
            Input1 = new RelayCommand(() => CurrentInputField += "1");
            Input2 = new RelayCommand(() => CurrentInputField += "2");
            Input3 = new RelayCommand(() => CurrentInputField += "3");
            Input4 = new RelayCommand(() => CurrentInputField += "4");
            Input5 = new RelayCommand(() => CurrentInputField += "5");
            Input6 = new RelayCommand(() => CurrentInputField += "6");
            Input7 = new RelayCommand(() => CurrentInputField += "7");
            Input8 = new RelayCommand(() => CurrentInputField += "8");
            Input9 = new RelayCommand(() => CurrentInputField += "9");
            InputClear = new RelayCommand(Clear);
            InputRemove = new RelayCommand(RemoveLastCharacter);
            InputAdd = new RelayCommand(() => SetOperator("+"));
            InputSubtract = new RelayCommand(() => SetOperator("-"));
            InputMultiply = new RelayCommand(() => SetOperator("*"));
            InputDivide = new RelayCommand(() => SetOperator("/"));
            InputResult = new RelayCommand(CalculateResult);
            InputDecimal = new RelayCommand(AddDecimalPoint);
        }

        private void Clear()
        {
            CurrentInputField = "";
            LastInput = "";
            _currentOperator = null;
        }

        private void RemoveLastCharacter()
        {
            if (!string.IsNullOrEmpty(CurrentInputField))
            {
                CurrentInputField = CurrentInputField.Remove(CurrentInputField.Length - 1);
            }
        }

        private void SetOperator(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(CurrentInputField))
            {
                LastInput = CurrentInputField;
                CurrentInputField = "";
                _currentOperator = operatorSymbol;
            }
        }

        private void CalculateResult()
        {
            if (!string.IsNullOrEmpty(CurrentInputField) && !string.IsNullOrEmpty(LastInput) && !string.IsNullOrEmpty(_currentOperator))
            {
                double num1 = double.Parse(LastInput);
                double num2 = double.Parse(CurrentInputField);
                double result = 0;

                switch (_currentOperator)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            result = 0;
                        }
                        break;
                }

                CurrentInputField = result.ToString();
                LastInput = "";
                _currentOperator = null;
            }
        }

        private void AddDecimalPoint()
        {
            if (!CurrentInputField.Contains("."))
            {
                CurrentInputField += ".";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
