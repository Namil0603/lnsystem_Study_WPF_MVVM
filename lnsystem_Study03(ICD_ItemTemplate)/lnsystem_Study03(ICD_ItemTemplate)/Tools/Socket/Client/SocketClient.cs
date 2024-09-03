using System.Net;
using System.Net.Sockets;
using lnsystem_Study03_ICD_ItemTemplate_.Data;

namespace lnsystem_Study03_ICD_ItemTemplate_.Tools.Socket.Client
{
    public class SocketClient
    {
        #region 멤버 변수

        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _serverEndPoint;
        private readonly IPEndPoint _localEndPoint;

        #endregion

        #region 이벤트

        public event Action<string, string>? MessageReceived;

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
                var (id,chat) = ICDMessageConverter.ParseDataPacket(result.Buffer);
                MessageReceived?.Invoke(id,chat);
            }
        }

        #endregion
    }
}