using System.Windows;
using lnsystem_Study03_ICD_ItemTemplate_.Data;
using lnsystem_Study03_ICD_ItemTemplate_.Tools.Socket.Client;
using lnsystem_Study03_ICD_ItemTemplate_.Tools.Socket.Server;
using lnsystem_Study03_ICD_ItemTemplate_.ViewModel;

namespace lnsystem_Study03_ICD_ItemTemplate_.Manager
{
    public class ChatManager
    {
        #region 상수

        private const string ServerIp = "127.0.0.1"; // 서버 IP 주소
        private const int ServerPort = 12345; // 서버 포트 번호

        #endregion

        #region 멤버 변수

        private SocketServer? _socketServer;
        private SocketClient? _socketClient;

        #endregion

        #region 이벤트

        public event Action<Message>? MessageReceived;

        #endregion

        #region 생성자

        /// <summary>
        /// ChatManager의 생성자입니다.
        /// </summary>
        /// <param name="status">현재 상태 (서버 또는 클라이언트)</param>
        public ChatManager(Status status)
        {
            InitializeSocket(status);
        }

        #endregion

        #region 비공개 메서드

        /// <summary>
        /// 소켓을 초기화합니다.
        /// </summary>
        /// <param name="status">현재 상태 (서버 또는 클라이언트)</param>
        private void InitializeSocket(Status status)
        {
            if (status == Status.Server)
            {
                _socketServer = new SocketServer(ServerPort);
                _socketServer.MessageReceived += OnMessageReceived;
                _socketServer.StartServer();
            }
            else if (status == Status.Client)
            {
                _socketClient = new SocketClient(ServerIp, ServerPort);
                _socketClient.MessageReceived += OnMessageReceived;
            }
        }

        /// <summary>
        /// 메시지 수신 시 호출됩니다.
        /// </summary>
        /// <param name="id">보낸 사람의 ID</param>
        /// <param name="chat">수신된 채팅 메시지</param>
        private void OnMessageReceived(string id, string chat)
        {
            var message = new Message(id, chat);
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageReceived?.Invoke(message);
            });
        }

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 메시지를 비동기적으로 전송합니다.
        /// </summary>
        /// <param name="message">전송할 메시지</param>
        /// <param name="status">현재 상태 (서버 또는 클라이언트)</param>
        public async Task SendMessageAsync(Message message, Status status)
        {
            if (string.IsNullOrWhiteSpace(message.Chat)) return;

            var icd = ICDMessageConverter.CreateDataPacket(message.ID, message.Chat);

            if (status == Status.Server && _socketServer != null)
            {
                await _socketServer.SendMessageToClientsAsync(icd);
            }
            else if (status == Status.Client && _socketClient != null)
            {
                await _socketClient.SendMessageAsync(icd);
            }
        }

        #endregion
    }
}
