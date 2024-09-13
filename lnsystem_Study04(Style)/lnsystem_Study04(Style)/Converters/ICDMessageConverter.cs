using System;
using System.Text;
using System.Windows;

namespace lnsystem_Study04_Style_.Converters
{
    public static class IcdMessageConverter
    {
        public static byte[] CreateDataPacket(string? id, string? chatting)
        {
            byte[] finalBytes = null;
            byte btError = 0;
            byte[] idBytes = Encoding.UTF8.GetBytes(id);

            if (string.IsNullOrEmpty(id) || chatting == null || idBytes.Length > 10)
            {
                if (string.IsNullOrEmpty(id))
                {
                    btError = 1;
                }
                else if (chatting == null)
                {
                    btError = 2;
                }
                else
                {
                    btError = 3;
                }
            }

            if (btError == 0)
            {
                byte[] paddedIdBytes = new byte[10];
                Buffer.BlockCopy(idBytes, 0, paddedIdBytes, 0, idBytes.Length);

                byte[] chatBytes = Encoding.UTF8.GetBytes(chatting);
                byte[] chatLengthBytes = BitConverter.GetBytes(chatBytes.Length);

                finalBytes = new byte[paddedIdBytes.Length + chatLengthBytes.Length + chatBytes.Length];
                Buffer.BlockCopy(paddedIdBytes, 0, finalBytes, 0, paddedIdBytes.Length);
                Buffer.BlockCopy(chatLengthBytes, 0, finalBytes, paddedIdBytes.Length, chatLengthBytes.Length);
                Buffer.BlockCopy(chatBytes, 0, finalBytes, paddedIdBytes.Length + chatLengthBytes.Length, chatBytes.Length);
            }
            else
            {
                string errorMessage = btError switch
                {
                    1 => "ID는 null이거나 비어 있을 수 없습니다.",
                    2 => "채팅 메시지는 null일 수 없습니다.",
                    3 => "ID는 10바이트를 초과할 수 없습니다.",
                    _ => "알 수 없는 오류가 발생했습니다."
                };
                MessageBox.Show(errorMessage, "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return finalBytes;
        }

        public static (string ID, string Chatting) ParseDataPacket(byte[] receivedBytes)
        {
            string id = string.Empty;
            string chatting = string.Empty;

            if (receivedBytes == null || receivedBytes.Length < 14)
            {
                MessageBox.Show("수신된 데이터 패킷이 null이거나 너무 짧습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return (id, chatting);
            }

            byte[] idBytes = new byte[10];
            Buffer.BlockCopy(receivedBytes, 0, idBytes, 0, 10);
            id = Encoding.UTF8.GetString(idBytes).TrimEnd('\0');

            byte[] chatLengthBytes = new byte[4];
            Buffer.BlockCopy(receivedBytes, 10, chatLengthBytes, 0, 4);
            int chatLength = BitConverter.ToInt32(chatLengthBytes, 0);

            if (chatLength < 0 || receivedBytes.Length < 14 + chatLength)
            {
                MessageBox.Show("수신된 데이터 패킷의 채팅 메시지 길이가 유효하지 않습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return (id, chatting);
            }

            byte[] chatBytes = new byte[chatLength];
            Buffer.BlockCopy(receivedBytes, 14, chatBytes, 0, chatLength);
            chatting = Encoding.UTF8.GetString(chatBytes);

            return (id, chatting);
        }
    }
}
