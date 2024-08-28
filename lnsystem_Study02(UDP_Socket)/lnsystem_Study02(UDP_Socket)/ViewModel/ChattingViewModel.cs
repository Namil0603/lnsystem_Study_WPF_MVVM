using System.Collections.ObjectModel;
using System.Windows.Input;
using lnsystem_Study02_UDP_Socket_.Tools;
using lnsystem_Study02_UDP_Socket_.Tools.Socket.Client;
using lnsystem_Study02_UDP_Socket_.Tools.Socket.Server;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public enum Status
    {
        Server,
        Client
    }

    public class ChattingViewModel : ViewModelBase
    {
        public ICommand SendMessageCommand { get; }
        public Status CurrentStatus { get; }
        private SocketServer _socketServer;
        private SocketClient _socketClient;
        public ObservableCollection<string> Messages { get; }
        public ObservableCollection<string> Users { get; }
        public string NewMessage { get; set; }

        public ChattingViewModel(Status status)
        {
            CurrentStatus = status;
            Messages = new ObservableCollection<string>();
            Users = new ObservableCollection<string>();
            SendMessageCommand = new RelayCommand(SendMessage);

            if (CurrentStatus == Status.Server)
            {
                _socketServer = new SocketServer(12345); // 포트 번호는 필요에 따라 변경
                _socketServer.MessageReceived += OnMessageReceived;
                _socketServer.StartServer();
            }
            else if (CurrentStatus == Status.Client)
            {
                _socketClient = new SocketClient("127.0.0.1", 12345); // 서버 IP와 포트 번호는 필요에 따라 변경
                _socketClient.MessageReceived += OnMessageReceived;
            }
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            string message = NewMessage;
            Messages.Add($"Me: {message}");

            if (CurrentStatus == Status.Server)
            {
                // 서버로서의 동작
                await _socketServer.SendMessageToClientsAsync(message);
            }
            else if (CurrentStatus == Status.Client)
            {
                // 클라이언트로서의 동작
                await _socketClient.SendMessageAsync(message);
            }

            NewMessage = string.Empty;
            OnPropertyChanged(nameof(NewMessage));
        }

        private void OnMessageReceived(string message)
        {
            // UI 스레드에서 ObservableCollection을 수정
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add($"Others : {message}");
            });
        }
    }
}
