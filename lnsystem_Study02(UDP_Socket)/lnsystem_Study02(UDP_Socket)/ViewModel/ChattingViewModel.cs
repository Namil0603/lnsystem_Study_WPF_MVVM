using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using lnsystem_Study02_UDP_Socket_.Model;
using lnsystem_Study02_UDP_Socket_.Tools;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
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

        #region 속성

        public ICommand SendMessageCommand { get; }
        public Status CurrentStatus { get; }
        public ObservableCollection<string> Messages { get; }

        public string NewMessage
        {
            get => _chatManager.NewMessage;
            set
            {
                _chatManager.NewMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 생성자

        public ChattingViewModel(Status status)
        {
            CurrentStatus = status;
            _chatManager = new ChatManager(status);
            Messages = new ObservableCollection<string>();
            SendMessageCommand = new RelayCommand(SendMessage);

            _chatManager.MessageReceived += OnMessageReceived;
        }

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 메시지를 전송합니다.
        /// </summary>
        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            string message = NewMessage;
            Messages.Add(new Message("Me", message).ToString());

            await _chatManager.SendMessageAsync(message, CurrentStatus);

            NewMessage = string.Empty;
            OnPropertyChanged(nameof(NewMessage));
        }

        #endregion

        #region 비공개 메서드

        /// <summary>
        /// 메시지 수신 시 호출됩니다.
        /// </summary>
        /// <param name="message">수신된 메시지</param>
        private void OnMessageReceived(Message message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(message.ToString());
            });
        }

        #endregion
    }
}

