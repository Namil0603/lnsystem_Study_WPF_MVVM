using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using lnsystem_Study04_Style_.ViewModel;

namespace lnsystem_Study04_Style_.View
{
    /// <summary>
    /// ChattingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChattingView
    {
        #region 생성자

        /// <summary>
        /// ChattingView의 생성자입니다.
        /// </summary>
        /// <param name="status">현재 상태 (서버 또는 클라이언트)</param>
        public ChattingView(Status status)
        {
            InitializeComponent();
            DataContext = new ChattingViewModel(status);

            // 창이 로드된 후 텍스트 입력 상자에 포커스를 설정합니다.
            Loaded += (_, _) => InputTextBox.Focus();
        }

        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// Enter 키를 눌렀을 때 메시지 전송
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is ChattingViewModel viewModel && viewModel.SendMessageCommand.CanExecute(null))
                {
                    viewModel.SendMessageCommand.Execute(null);
                    ((TextBox)sender).Clear();
                }
            }
        }

        /// <summary>
        /// ListBox가 로드될 때 스크롤을 끝으로 이동
        /// </summary>
        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (VisualTreeHelper.GetChild(listBox, 0) is Border border &&
                    VisualTreeHelper.GetChild(border, 0) is ScrollViewer scrollViewer)
                {
                    scrollViewer.ScrollToEnd();
                }
            }
        }

        /// <summary>
        /// ListBox의 스크롤이 변경될 때 스크롤을 끝으로 이동
        /// </summary>
        private void ListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (VisualTreeHelper.GetChild(listBox, 0) is Border border &&
                    VisualTreeHelper.GetChild(border, 0) is ScrollViewer scrollViewer)
                {
                    if (e.ExtentHeightChange > 0)
                    {
                        scrollViewer.ScrollToEnd();
                    }
                }
            }
        }

        #endregion
    }
}
