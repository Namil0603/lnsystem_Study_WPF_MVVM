using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using lnsystem_Study04_Style_.Commands;
using lnsystem_Study04_Style_.Data;
using lnsystem_Study04_Style_.Manager;
using lnsystem_Study04_Style_.Model;

namespace lnsystem_Study04_Style_.ViewModel
{
    #region 열거형

    public enum Status
    {
        Server,
        Client
    }

    #endregion

    public class ChattingViewModel : ViewModelBase
    {
        #region 멤버 변수

        private readonly ChatManager _chatManager;

        #endregion

        #region 명령

        public ICommand SendMessageCommand { get; }

        #endregion

        #region 속성

        private Status CurrentStatus { get; }
        public ObservableCollection<Message>? MessagesModel => MessageModel.Instance?.MessagesModel;

        public string? NewMessage
        {
            get => MessageModel.Instance?.InputTextBox;
            set
            {
                if (MessageModel.Instance != null) MessageModel.Instance.InputTextBox = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// ChattingViewModel의 생성자입니다.
        /// </summary>
        /// <param name="status">현재 상태 (서버 또는 클라이언트)</param>
        public ChattingViewModel(Status status)
        {
            CurrentStatus = status;
            _chatManager = new ChatManager(CurrentStatus);
            SendMessageCommand = new RelayCommand(SendMessage);
            _chatManager.MessageReceived += OnMessageReceived;
        }

        #endregion

        #region 비공개 메서드

        /// <summary>
        /// 메시지를 전송합니다.
        /// </summary>
        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            var message = new Message(UserDataModel.Instance.LocalId, NewMessage, true);
            MessageModel.Instance?.MessagesModel?.Add(message);
            await _chatManager.SendMessageAsync(message, CurrentStatus);

            NewMessage = string.Empty;
            OnPropertyChanged(nameof(NewMessage));
        }

        /// <summary>
        /// 메시지 수신 시 호출됩니다.
        /// </summary>
        /// <param name="message">수신된 메시지</param>
        private void OnMessageReceived(Message message)
        {
            message.IsLocal = false;
            Application.Current.Dispatcher.BeginInvoke(() => MessageModel.Instance?.MessagesModel?.Add(message));
        }

        #endregion
    }
}
