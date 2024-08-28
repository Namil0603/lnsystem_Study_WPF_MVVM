using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lnsystem_Study02_UDP_Socket_.Tools.Socket.Server
{
    /// <summary>
    /// UDP 소켓 서버
    /// </summary>
    public class SocketServer
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint;
        private CancellationTokenSource _cancellationTokenSource;
        private List<IPEndPoint> _clientEndPoints;

        public event Action<string> MessageReceived;

        public SocketServer(int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _udpClient = new UdpClient(_endPoint);
            _cancellationTokenSource = new CancellationTokenSource();
            _clientEndPoints = new List<IPEndPoint>();
        }

        public void StartServer()
        {
            Task.Run(() => ReceiveDataAsync(_cancellationTokenSource.Token));
        }

        public void StopServer()
        {
            _cancellationTokenSource.Cancel();
            _udpClient.Close();
        }

        public async Task SendMessageToClientsAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var clientEndPoint in _clientEndPoints)
            {
                await _udpClient.SendAsync(data, data.Length, clientEndPoint);
            }
        }

        private async Task ReceiveDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = await _udpClient.ReceiveAsync();
                    string receivedData = Encoding.UTF8.GetString(result.Buffer);
                    MessageReceived?.Invoke(receivedData);

                    // 클라이언트의 IP 주소와 포트 번호를 저장
                    if (!_clientEndPoints.Contains(result.RemoteEndPoint))
                    {
                        Debug.WriteLine(result.RemoteEndPoint);
                        _clientEndPoints.Add(result.RemoteEndPoint);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // 소켓이 닫힌 경우 예외 처리
                Debug.WriteLine("이게 머선일인지 파악해봐야 압니다 녜;...");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:?!?!?!?! :  {ex.Message}");
            }
        }
    }
}
