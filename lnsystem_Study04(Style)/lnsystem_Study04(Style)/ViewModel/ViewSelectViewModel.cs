using System.Windows.Input;
using lnsystem_Study04_Style_.Commands;
using lnsystem_Study04_Style_.Dialog;
using lnsystem_Study04_Style_.Model;
using lnsystem_Study04_Style_.View;

namespace lnsystem_Study04_Style_.ViewModel
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
            var idInputDialog = new IdInputDialog();
            if (idInputDialog.ShowDialog() == true)
            {
                UserDataModel.Instance.LocalId = idInputDialog.UserId;
                MainWindowViewModel.Instance?.ChangeTitle("Socket UDP Client");
                MainWindowViewModel.Instance?.ChangeView(new ChattingView(Status.Client));
            }
        }

        /// <summary>
        /// 서버 뷰로 변경합니다.
        /// </summary>
        private void SocketServerClick()
        {
            UserDataModel.Instance.LocalId = "Server"; // 서버는 강제로 ID를 서버로 할당
            MainWindowViewModel.Instance?.ChangeTitle("Socket UDP Server");
            MainWindowViewModel.Instance?.ChangeView(new ChattingView(Status.Server));
        }

        #endregion
    }
}