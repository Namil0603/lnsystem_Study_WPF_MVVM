using System.Collections.ObjectModel;
using System.Windows.Input;
using FinalCalc.Model;
using FinalCalc.Tools;

namespace FinalCalc.ViewModel
{
    public class VMCalculatorViewModel : ViewModelBase
    {
        #region 필드
        private readonly CalculatorModel _calculatorModel;
        public ObservableCollection<OperatorType> Operators { get; }
        public ICommand CalculateCommand { get; }
        #endregion

        #region 속성
        public double FirstInput
        {
            get => _calculatorModel.FirstNumber;
            set
            {
                _calculatorModel.FirstNumber = value;
                OnPropertyChanged();
            }
        }

        public double SecondInput
        {
            get => _calculatorModel.SecondNumber;
            set
            {
                _calculatorModel.SecondNumber = value;
                OnPropertyChanged();
            }
        }

        public OperatorType SelectedOperator
        {
            get => _calculatorModel.Operator;
            set
            {
                _calculatorModel.Operator = value;
                OnPropertyChanged();
            }
        }

        public double CalculationResult
        {
            get => _calculatorModel.Result;
            set
            {
                _calculatorModel.Result = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region 생성자
        public VMCalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            Operators = new ObservableCollection<OperatorType>
            {
                OperatorType.Add,
                OperatorType.Subtract,
                OperatorType.Multiply,
                OperatorType.Divide
            };
            CalculateCommand = new RelayCommand(OnCalculateButtonClick);
        }
        #endregion

        #region 메서드
        private void OnCalculateButtonClick()
        {
            CalculationResult = PerformCalculation(_calculatorModel.FirstNumber, _calculatorModel.SecondNumber, SelectedOperator);
        }

        private double PerformCalculation(double firstNumber, double secondNumber, OperatorType selectedOperator)
        {
            return selectedOperator switch
            {
                OperatorType.Add => firstNumber + secondNumber,
                OperatorType.Subtract => firstNumber - secondNumber,
                OperatorType.Multiply => firstNumber * secondNumber,
                OperatorType.Divide => secondNumber != 0 ? firstNumber / secondNumber : 0,
                _ => 0
            };
        }
        #endregion
    }
}
