using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lnsystem_Study02_UDP_Socket_.Tools.Socket.Client
{
    public class SocketClient
    {
        #region 멤버 변수

        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _serverEndPoint;
        private readonly IPEndPoint _localEndPoint;

        #endregion

        #region 이벤트

        public event Action<string>? MessageReceived;

        #endregion

        #region 생성자

        public SocketClient(string serverIp, int serverPort)
        {
            _serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            _localEndPoint = new IPEndPoint(IPAddress.Any, 0); // 로컬 포트에 바인딩
            _udpClient = new UdpClient(_localEndPoint);
            Task.Run(ReceiveMessagesAsync);
        }

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 서버로 메시지를 비동기적으로 전송합니다.
        /// </summary>
        /// <param name="message">전송할 메시지</param>
        public async Task SendMessageAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _udpClient.SendAsync(data, data.Length, _serverEndPoint);
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
                string receivedData = Encoding.UTF8.GetString(result.Buffer);
                MessageReceived?.Invoke(receivedData);
            }
        }

        #endregion
    }
}