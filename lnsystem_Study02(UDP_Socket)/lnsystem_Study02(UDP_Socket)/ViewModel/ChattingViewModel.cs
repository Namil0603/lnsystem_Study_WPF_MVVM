﻿using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using lnsystem_Study02_UDP_Socket_.Data;
using lnsystem_Study02_UDP_Socket_.Manager;
using lnsystem_Study02_UDP_Socket_.Model;

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
        private readonly MessageModel _messageModel;

        #endregion

        #region 속성

        public ICommand SendMessageCommand { get; }
        public Status CurrentStatus { get; }
        public ObservableCollection<string> Messages { get; }

        public string NewMessage
        {
            get => _messageModel.InputTextBox;
            set
            {
                _messageModel.InputTextBox = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 생성자

        public ChattingViewModel(Status status)
        {
            CurrentStatus = status;
            _messageModel = new MessageModel();
            _chatManager = new ChatManager(CurrentStatus);
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

            var user = new User(GetLocalIpAddress(), CurrentStatus.ToString());
            var message = new Message(user, NewMessage);
            Messages.Add(message.ToString());

            _messageModel.ChattingList.Add(message); // 캐싱

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
                _messageModel.ChattingList.Add(message); // 캐싱
                Messages.Add(message.ToString());
            });
        }

        /// <summary>
        /// 로컬 IP 주소를 가져옵니다.
        /// </summary>
        /// <returns>로컬 IP 주소</returns>
        private string GetLocalIpAddress()
        {
            // 현재 호스트(컴퓨터)의 DNS 정보를 가져옵니다.
            // Dns.GetHostName() 메서드는 현재 컴퓨터의 호스트 이름을 반환합니다.
            // Dns.GetHostEntry() 메서드는 해당 호스트 이름에 대한 IP 주소 정보를 포함하는 IPHostEntry 객체를 반환합니다.
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                // IPv4 주소인지 확인합니다.
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("시스템에 IPv4 주소를 가진 네트워크 어댑터가 없습니다!");
        }

        #endregion
    }
}
