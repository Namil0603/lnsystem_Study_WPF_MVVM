﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using lnsystem_Study02_UDP_Socket_.ViewModel;

namespace lnsystem_Study02_UDP_Socket_.View
{
    /// <summary>
    /// ChattingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChattingView : UserControl
    {
        #region 생성자

        public ChattingView(Status status)
        {
            InitializeComponent();
            DataContext = new ChattingViewModel(status);
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

