using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lnsystem_Study02_UDP_Socket_.Tools.Socket.Client
{
    public class SocketClient
    {
        private UdpClient _udpClient;
        private IPEndPoint _serverEndPoint;
        private IPEndPoint _localEndPoint;

        public event Action<string> MessageReceived;

        public SocketClient(string serverIp, int serverPort)
        {
            _serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            _localEndPoint = new IPEndPoint(IPAddress.Any, 0); // 로컬 포트에 바인딩
            _udpClient = new UdpClient(_localEndPoint);
            Task.Run(() => ReceiveMessageAsync());
        }

        public async Task SendMessageAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _udpClient.SendAsync(data, data.Length, _serverEndPoint);
        }

        private async Task ReceiveMessageAsync()
        {
            while (true)
            {
                var result = await _udpClient.ReceiveAsync();
                string receivedData = Encoding.UTF8.GetString(result.Buffer);
                MessageReceived?.Invoke(receivedData);
            }
        }
    }
}