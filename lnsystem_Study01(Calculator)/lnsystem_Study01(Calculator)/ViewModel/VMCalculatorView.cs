using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using lnsystem_Study01_Calculator_.Data;
using lnsystem_Study01_Calculator_.Tools;

namespace lnsystem_Study01_Calculator_.ViewModel
{
    public class VMCalculatorView : INotifyPropertyChanged
    {
        private string firstInput;
        private string secondInput;
        private string selectedOperator;

        // 계산 데이터 리스트
        public ObservableCollection<CalculateData> CalculateDataList { get; }
        // 연산자 리스트
        public ObservableCollection<string> Operators { get; }
        // 계산 명령
        public ICommand CalculateCommand { get; }
        // 종료 명령
        public ICommand QuitCommand { get; }

        // 첫 번째 입력 값
        public string FirstInput
        {
            get => firstInput;
            set
            {
                firstInput = value;
                OnPropertyChanged();
            }
        }

        // 두 번째 입력 값
        public string SecondInput
        {
            get => secondInput;
            set
            {
                secondInput = value;
                OnPropertyChanged();
            }
        }

        // 선택된 연산자
        public string SelectedOperator
        {
            get => selectedOperator;
            set
            {
                selectedOperator = value;
                OnPropertyChanged();
            }
        }

        // 생성자
        public VMCalculatorView()
        {
            QuitCommand = new RelayCommand(QuitApplication);
            CalculateCommand = new RelayCommand(OnCalculateButtonClick);
            CalculateDataList = new ObservableCollection<CalculateData>();
            Operators = new ObservableCollection<string> { "+", "-", "*", "/" };
        }

        // 애플리케이션 종료
        private void QuitApplication() => Application.Current.Shutdown();

        // 계산 버튼 클릭 시 실행되는 메서드
        private void OnCalculateButtonClick()
        {
            // 입력 값 정리
            string cleanedFirstInput = FirstInput?.Trim();
            string cleanedSecondInput = SecondInput?.Trim();

            if (TryParseInputs(cleanedFirstInput, cleanedSecondInput, out double operand1, out double operand2))
            {
                if (TryCalculateResult(operand1, operand2, out double result, out Operator op))
                {
                    AddCalculateData(operand1, operand2, op, result);
                }
            }
            else
            {
                ShowErrorMessage("유효한 숫자를 입력하세요.");
            }
        }

        // 입력 값을 double로 변환 시도
        private bool TryParseInputs(string firstInput, string secondInput, out double operand1, out double operand2)
        {
            operand1 = 0;
            operand2 = 0;

            if (string.IsNullOrWhiteSpace(firstInput) || string.IsNullOrWhiteSpace(secondInput))
            {
                ShowErrorMessage("입력 값이 비어 있습니다.");
                return false;
            }

            if (!double.TryParse(firstInput, out operand1))
            {
                ShowErrorMessage("첫 번째 입력 값이 유효한 숫자가 아닙니다.");
                return false;
            }

            if (!double.TryParse(secondInput, out operand2))
            {
                ShowErrorMessage("두 번째 입력 값이 유효한 숫자가 아닙니다.");
                return false;
            }

            return true;
        }

        // 계산 결과를 시도
        private bool TryCalculateResult(double operand1, double operand2, out double result, out Operator op)
        {
            result = 0;
            op = Operator.Plus;

            switch (SelectedOperator)
            {
                case "+":
                    result = operand1 + operand2;
                    op = Operator.Plus;
                    break;
                case "-":
                    result = operand1 - operand2;
                    op = Operator.Minus;
                    break;
                case "*":
                    result = operand1 * operand2;
                    op = Operator.Multiply;
                    break;
                case "/":
                    if (operand2 != 0)
                    {
                        result = operand1 / operand2;
                        op = Operator.Divide;
                    }
                    else
                    {
                        ShowErrorMessage("0으로 나눌 수 없습니다.");
                        return false;
                    }
                    break;
                default:
                    ShowErrorMessage("유효하지 않은 연산자입니다.");
                    return false;
            }

            return true;
        }

        // 계산 데이터를 추가
        private void AddCalculateData(double operand1, double operand2, Operator op, double result)
        {
            var calculateData = new CalculateData
            {
                Operand1 = operand1,
                Operand2 = operand2,
                Operator = op,
                Result = result
            };
            CalculateDataList.Add(calculateData);
        }

        // 오류 메시지 표시
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "오류", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // 속성 변경 알림 이벤트
        public event PropertyChangedEventHandler PropertyChanged;

        // 속성 변경 알림 메서드
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
