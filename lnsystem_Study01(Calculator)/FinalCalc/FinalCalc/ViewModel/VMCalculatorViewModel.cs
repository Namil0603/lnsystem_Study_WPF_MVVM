using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalCalc.ViewModel
{
    public class VMCalculatorViewModel : INotifyPropertyChanged
    {
        #region 상수
        // 오류 메시지 상수 정의
        private const string DivisionByZeroError = "오류: 0으로 나눌 수 없습니다";
        private const string InvalidOperatorError = "오류: 잘못된 연산자";
        private const string InvalidInputError = "오류: 잘못된 입력";
        #endregion

        #region 필드
        // 연산자 목록과 계산 명령 정의
        public ObservableCollection<string> Operators { get; }
        public ICommand CalculateCommand { get; }

        // 입력 값과 결과를 저장할 필드
        private string _firstInput;
        private string _selectedOperator;
        private string _secondInput;
        private string _calculationResult;
        #endregion

        #region 속성
        // 첫 번째 입력 값 속성
        public string FirstInput
        {
            get => _firstInput;
            set => SetField(ref _firstInput, value);
        }

        // 두 번째 입력 값 속성
        public string SecondInput
        {
            get => _secondInput;
            set => SetField(ref _secondInput, value);
        }

        // 선택된 연산자 속성
        public string SelectedOperator
        {
            get => _selectedOperator;
            set => SetField(ref _selectedOperator, value);
        }

        // 계산 결과 속성
        public string CalculationResult
        {
            get => _calculationResult;
            set => SetField(ref _calculationResult, value);
        }
        #endregion

        #region 생성자
        // 생성자: 연산자 목록 초기화 및 계산 명령 설정
        public VMCalculatorViewModel()
        {
            Operators = new ObservableCollection<string> { "+", "-", "*", "/" };
            CalculateCommand = new Tools.RelayCommand(OnCalculateButtonClick);
        }
        #endregion

        #region 메서드
        // 계산 버튼 클릭 시 호출되는 메서드
        private void OnCalculateButtonClick()
        {
            // 입력 값이 유효한 숫자인지 확인
            if (double.TryParse(FirstInput, out double firstNumber) && double.TryParse(SecondInput, out double secondNumber))
            {
                // 유효한 경우 계산 수행
                CalculationResult = PerformCalculation(firstNumber, secondNumber, SelectedOperator);
            }
            else
            {
                // 유효하지 않은 경우 오류 메시지 설정
                CalculationResult = InvalidInputError;
            }
        }

        // 실제 계산을 수행하는 메서드
        private string PerformCalculation(double firstNumber, double secondNumber, string selectedOperator)
        {
            // 선택된 연산자에 따라 계산 수행
            return selectedOperator switch
            {
                "+" => (firstNumber + secondNumber).ToString(),
                "-" => (firstNumber - secondNumber).ToString(),
                "*" => (firstNumber * secondNumber).ToString(),
                "/" => secondNumber != 0 ? (firstNumber / secondNumber).ToString() : DivisionByZeroError,
                _ => InvalidOperatorError,
            };
        }

        // 속성 값이 변경되었음을 알리는 이벤트
        public event PropertyChangedEventHandler? PropertyChanged;

        // 속성 값이 변경되었음을 알리는 메서드
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 필드 값을 설정하고 속성 값이 변경되었음을 알리는 메서드
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}

