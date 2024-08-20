using NewCalc.Model;
using NewCalc.Tool;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NewCalc.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        // Commands
        public ICommand CalculateCommand { get; }

        // Properties
        public ObservableCollection<string> Operators { get; }

        private string firstInput;
        private string secondInput;
        private string finalText;
        private string selectedOperators;

        public string FirstInput
        {
            get => firstInput;
            set => SetField(ref firstInput, value);
        }

        public string SecondInput
        {
            get => secondInput;
            set => SetField(ref secondInput, value);
        }

        public string FinalText
        {
            get => finalText;
            set => SetField(ref finalText, value);
        }
        public string SelectedOperators
        {
            get => selectedOperators;
            set
            {
                selectedOperators = value;
                OnPropertyChanged();
            }
        }

        private readonly CalculatorModel calculatorModel;

        // Constructor
        public CalculatorViewModel()
        {
            CalculateCommand = new RelayCommand(CalculateExecute);
            Operators = new ObservableCollection<string> { "+", "-", "*", "/" };
            calculatorModel = new CalculatorModel();
        }

        // Methods
        private void CalculateExecute()
        {
            if (string.IsNullOrEmpty(FirstInput) || string.IsNullOrEmpty(SecondInput) || string.IsNullOrEmpty(SelectedOperators))
            {
                FinalText = "Please fill all the fields";
                return;
            }

            if (!double.TryParse(FirstInput, out double firstNumber) || !double.TryParse(SecondInput, out double secondNumber))
            {
                FinalText = "Please enter a valid number";
                return;
            }

            FinalText = SelectedOperators switch
            {
                "+" => calculatorModel.Add(firstNumber, secondNumber).ToString(),
                "-" => calculatorModel.Subtract(firstNumber, secondNumber).ToString(),
                "*" => calculatorModel.Multiply(firstNumber, secondNumber).ToString(),
                "/" => calculatorModel.Divide(firstNumber, secondNumber).ToString(),
                _ => "Invalid Operator"
            };
        }

        // INotifyPropertyChanged Implementation
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
