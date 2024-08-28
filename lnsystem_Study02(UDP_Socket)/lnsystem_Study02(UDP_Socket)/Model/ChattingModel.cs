using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using lnsystem_Study02_UDP_Socket_.Tools.Socket.Client;
using lnsystem_Study02_UDP_Socket_.Tools.Socket.Server;
using lnsystem_Study02_UDP_Socket_.ViewModel;

namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class ChattingModel
    {
        private SocketServer? _socketServer;
        private SocketClient? _socketClient;
        public ObservableCollection<Message> Messages { get; }
        public ObservableCollection<User> Users { get; }
        public string NewMessage { get; set; }

        public event Action<Message>? MessageReceived;

        public ChattingModel(Status status)
        {
            Messages = new ObservableCollection<Message>();
            Users = new ObservableCollection<User>();
            NewMessage = string.Empty; // NewMessage 초기화

            if (status == Status.Server)
            {
                _socketServer = new SocketServer(12345); // 포트 번호는 필요에 따라 변경
                _socketServer.MessageReceived += OnMessageReceived;
                _socketServer.StartServer();
            }
            else if (status == Status.Client)
            {
                _socketClient = new SocketClient("127.0.0.1", 12345); // 서버 IP와 포트 번호는 필요에 따라 변경
                _socketClient.MessageReceived += OnMessageReceived;
            }
        }

        public async Task SendMessageAsync(string message, Status status)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            if (status == Status.Server && _socketServer != null)
            {
                await _socketServer.SendMessageToClientsAsync(message);
            }
            else if (status == Status.Client && _socketClient != null)
            {
                await _socketClient.SendMessageAsync(message);
            }
        }

        private void OnMessageReceived(string message)
        {
            var receivedMessage = new Message("Server", message);
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(receivedMessage);
                MessageReceived?.Invoke(receivedMessage);
            });
        }
    }
}


