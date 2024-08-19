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
        public ICommand CalculateViewCommand { get; }
        // 종료 명령
        public ICommand QuitViewCommand { get; }

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
            QuitViewCommand = new RelayCommand(QuitApplication);
            CalculateViewCommand = new RelayCommand(OnCalculateButtonClick);
            CalculateDataList = new ObservableCollection<CalculateData>();
            Operators = new ObservableCollection<string> { "+", "-", "*", "/" };
        }

        // 애플리케이션 종료
        private void QuitApplication() => Application.Current.Shutdown();

        // 계산 버튼 클릭 시 실행되는 메서드
        private void OnCalculateButtonClick()
        {
            Debug.WriteLine("Calculate Button Clicked");
            Debug.WriteLine($"First Input: {FirstInput}");
            Debug.WriteLine($"Second Input: {SecondInput}");
            Debug.WriteLine($"Selected Operator: {SelectedOperator}");

            // 입력 값 정리
            string cleanedFirstInput = FirstInput.Trim();
            string cleanedSecondInput = SecondInput.Trim();

            if (double.TryParse(cleanedFirstInput, out double operand1) && double.TryParse(cleanedSecondInput, out double operand2))
            {
                double result;
                Operator op;

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
                            MessageBox.Show("0으로 나눌 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show("유효하지 않은 연산자입니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }

                var calculateData = new CalculateData
                {
                    Operand1 = operand1,
                    Operand2 = operand2,
                    Operator = op,
                    Result = result
                };
                CalculateDataList.Add(calculateData);
            }
            else
            {
                MessageBox.Show("유효한 숫자를 입력하세요.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
