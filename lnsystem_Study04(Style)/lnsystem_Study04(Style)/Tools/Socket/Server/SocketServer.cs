using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using lnsystem_Study04_Style_.Converters;

namespace lnsystem_Study04_Style_.Tools.Socket.Server
{
    /// <summary>
    /// UDP 소켓 서버
    /// </summary>
    public class SocketServer
    {
        #region 멤버 변수

        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _endPoint;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly List<IPEndPoint> _clientEndPoints;

        #endregion

        #region 이벤트

        public event Action<string, string>? MessageReceived;

        #endregion

        #region 생성자

        public SocketServer(int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _udpClient = new UdpClient(_endPoint);
            _cancellationTokenSource = new CancellationTokenSource();
            _clientEndPoints = new List<IPEndPoint>();
        }

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 서버를 시작합니다.
        /// </summary>
        public void StartServer() => Task.Run(() => ReceiveDataAsync(_cancellationTokenSource.Token));

        /// <summary>
        /// 서버를 중지합니다.
        /// </summary>
        public void StopServer()
        {
            _cancellationTokenSource.Cancel();
            _udpClient.Close();
        }

        /// <summary>
        /// 클라이언트들에게 메시지를 전송합니다.
        /// </summary>
        /// <param name="message">전송할 메시지</param>
        /// <param name="excludeEndPoint">제외할 클라이언트의 EndPoint</param>
        public async Task SendMessageToClientsAsync(byte[] message, IPEndPoint? excludeEndPoint = null)
        {
            foreach (var clientEndPoint in _clientEndPoints.Where(clientEndPoint => !Equals(clientEndPoint, excludeEndPoint)))
            {
                await _udpClient.SendAsync(message, message.Length, clientEndPoint);
            }
        }

        #endregion

        #region 비공개 메서드

        /// <summary>
        /// 데이터를 비동기적으로 수신합니다.
        /// </summary>
        /// <param name="cancellationToken">취소 토큰</param>
        private async Task ReceiveDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = await _udpClient.ReceiveAsync(cancellationToken);
                    var (id, chat) = ICDMessageConverter.ParseDataPacket(result.Buffer);

                    MessageReceived?.Invoke(id, chat);

                    // 클라이언트의 IP 주소와 포트 번호를 저장
                    if (!_clientEndPoints.Contains(result.RemoteEndPoint))
                    {
                        _clientEndPoints.Add(result.RemoteEndPoint);
                    }

                    // 메시지를 보낸 클라이언트를 제외하고 다른 클라이언트들에게 메시지를 전송
                    await SendMessageToClientsAsync(result.Buffer, result.RemoteEndPoint);
                }
            }
            catch (ObjectDisposedException)
            {
                // 소켓이 닫힌 경우 예외 처리
                Debug.WriteLine("소켓이 닫혔습니다.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"오류 발생: {ex.Message}");
            }
        }

        #endregion
    }
}
