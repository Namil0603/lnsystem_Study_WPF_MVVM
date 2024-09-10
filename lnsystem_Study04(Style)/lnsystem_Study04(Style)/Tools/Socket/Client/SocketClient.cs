using System.Net;
using System.Net.Sockets;
using System.Text;
using lnsystem_Study04_Style_.Converters;

namespace lnsystem_Study04_Style_.Tools.Socket.Client
{
    public class SocketClient
    {
        #region 멤버 변수

        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _serverEndPoint;

        #endregion

        #region 이벤트

        public event Action<string, string>? MessageReceived;

        #endregion

        #region 생성자

        public SocketClient(string serverIp, int serverPort)
        {
            _serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            var localEndPoint = new IPEndPoint(IPAddress.Any, 0); // 로컬 포트에 바인딩
            _udpClient = new UdpClient(localEndPoint);
            Task.Run(ReceiveMessagesAsync);

            // 클라이언트가 처음 시작할 때 서버로 특정 메시지를 보냄
            // INITIAL_MESSAGE 라는 메시지를 보내면 서버에서 클라이언트의 ID를 설정함
            var initialMessage = "INITIAL_MESSAGE"u8.ToArray();
            SendMessageAsync(initialMessage).Wait();
        }

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 서버로 메시지를 비동기적으로 전송합니다.
        /// </summary>
        /// <param name="message">전송할 메시지</param>
        public async Task SendMessageAsync(byte[] message)
        {
            await _udpClient.SendAsync(message, message.Length, _serverEndPoint);
        }

        #endregion

        #region 비공개 메서드

        /// <summary>
        /// 서버로부터 메시지를 비동기적으로 수신합니다.
        /// </summary>
        private async Task ReceiveMessagesAsync()
        {
            while (true)
            {
                var result = await _udpClient.ReceiveAsync();
                var (id, chat) = IcdMessageConverter.ParseDataPacket(result.Buffer);
                MessageReceived?.Invoke(id, chat);
            }
        }

        #endregion
    }
}