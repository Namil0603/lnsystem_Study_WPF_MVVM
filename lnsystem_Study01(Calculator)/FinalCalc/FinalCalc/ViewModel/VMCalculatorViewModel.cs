using System.Collections.ObjectModel;
using System.Windows.Input;
using FinalCalc.Model;
using FinalCalc.Tools;

namespace FinalCalc.ViewModel
{
    public class VMCalculatorViewModel : ViewModelBase
    {
        #region 필드
        private readonly CalculatorModel _calculatorModel; // 계산기 모델
        public ObservableCollection<OperatorType> Operators { get; } // 연산자 목록
        public ICommand CalculateCommand { get; } // 계산 명령
        #endregion

        #region 속성
        public double FirstInput
        {
            get => _calculatorModel.FirstNumber; // 첫 번째 입력값 가져오기
            set
            {
                _calculatorModel.FirstNumber = value; // 첫 번째 입력값 설정
                OnPropertyChanged(); // 속성 변경 알림
            }
        }
        public double SecondInput
        {
            get => _calculatorModel.SecondNumber; // 두 번째 입력값 가져오기
            set
            {
                _calculatorModel.SecondNumber = value; // 두 번째 입력값 설정
                OnPropertyChanged(); // 속성 변경 알림
            }
        }

        public OperatorType SelectedOperator
        {
            get => _calculatorModel.Operator; // 선택된 연산자 가져오기
            set
            {
                _calculatorModel.Operator = value; // 선택된 연산자 설정
                OnPropertyChanged(); // 속성 변경 알림
            }
        }

        public double CalculationResult
        {
            get => _calculatorModel.Result; // 계산 결과 가져오기
            set
            {
                _calculatorModel.Result = value; // 계산 결과 설정
                OnPropertyChanged(); // 속성 변경 알림
            }
        }
        #endregion

        #region 생성자
        public VMCalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel(); // 계산기 모델 초기화
            Operators = new ObservableCollection<OperatorType>
            {
                OperatorType.Add, // 더하기
                OperatorType.Subtract, // 빼기
                OperatorType.Multiply, // 곱하기
                OperatorType.Divide // 나누기
            };
            CalculateCommand = new RelayCommand(OnCalculateButtonClick); // 계산 명령 초기화
        }
        #endregion

        #region 메서드
        private void OnCalculateButtonClick()
        {
            // 계산 버튼 클릭 시 호출되는 메서드
            CalculationResult = PerformCalculation(FirstInput, SecondInput, SelectedOperator);
        }

        private double PerformCalculation(double firstNumber, double secondNumber, OperatorType selectedOperator)
        {
            // 실제 계산을 수행하는 메서드
            return selectedOperator switch
            {
                OperatorType.Add => firstNumber + secondNumber, // 더하기
                OperatorType.Subtract => firstNumber - secondNumber, // 빼기
                OperatorType.Multiply => firstNumber * secondNumber, // 곱하기
                OperatorType.Divide => secondNumber != 0 ? firstNumber / secondNumber : 0, // 나누기 (0으로 나누기 방지)
                _ => 0 // 기본값
            };
        }
        #endregion
    }
}
