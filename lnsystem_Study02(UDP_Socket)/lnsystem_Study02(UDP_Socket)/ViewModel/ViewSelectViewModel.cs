﻿using System.Windows.Input;
using lnsystem_Study02_UDP_Socket_.View;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public class ViewSelectViewModel : ViewModelBase
    {
        #region 명령어

        public ICommand SocketClientClickCommand { get; }
        public ICommand SocketServerClickCommand { get; }

        #endregion

        #region 생성자

        public ViewSelectViewModel()
        {
            SocketClientClickCommand = new RelayCommand(SocketClientClick);
            SocketServerClickCommand = new RelayCommand(SocketServerClick);
        }

        #endregion

        #region 비공개 메서드

        /// <summary>
        /// 클라이언트 뷰로 변경합니다.
        /// </summary>
        private void SocketClientClick()
        {
            MainWindowViewModel.Instance?.ChangeTitle("Socket UDP Client");
            MainWindowViewModel.Instance?.ChangeView(new ChattingView(Status.Client));
        }

        /// <summary>
        /// 서버 뷰로 변경합니다.
        /// </summary>
        private void SocketServerClick()
        {
            MainWindowViewModel.Instance?.ChangeTitle("Socket UDP Server");
            MainWindowViewModel.Instance?.ChangeView(new ChattingView(Status.Server));
        }

        #endregion
    }
}