using System.Text;

namespace lnsystem_Study03_ICD_ItemTemplate_.Data
{
    /// <summary>
    /// 아래 메서드들은 ICD 프로토콜에 따라 데이터 패킷을 생성하고 파싱합니다.
    /// 프로토콜은 ID(10바이트)와 채팅 메시지 길이(4바이트) 그리고 채팅 메시지로 구성됩니다.
    /// 한글을 전송하기 위해 ASCII가 아닌 UTF-8 인코딩을 사용합니다.
    /// </summary>
    public static class ICDMessageConverter
    {
        /// <summary>
        /// 주어진 ID와 채팅 메시지를 바탕으로 데이터 패킷을 생성합니다.
        /// </summary>
        /// <param name="ID">사용자 ID</param>
        /// <param name="chatting">채팅 메시지</param>
        /// <returns>생성된 데이터 패킷</returns>
        public static byte[] CreateDataPacket(string ID, string chatting)
        {
            // ID가 null이거나 길이가 10을 초과하는지 확인
            if (string.IsNullOrEmpty(ID))
            {
                throw new ArgumentException("ID는 null이거나 비어 있을 수 없습니다.");
            }

            // 채팅 메시지가 null인지 확인
            if (chatting == null)
            {
                throw new ArgumentNullException(nameof(chatting), "채팅 메시지는 null일 수 없습니다.");
            }

            // ID를 UTF-8 인코딩으로 변환
            byte[] idBytes = Encoding.UTF8.GetBytes(ID);
            if (idBytes.Length > 10)
            {
                throw new ArgumentException("ID는 10바이트를 초과할 수 없습니다.");
            }

            // ID를 10바이트로 고정 (부족한 부분은 '\0'으로 채움)
            byte[] paddedIdBytes = new byte[10];
            Buffer.BlockCopy(idBytes, 0, paddedIdBytes, 0, idBytes.Length);

            // 채팅 메시지를 바이트 배열로 변환
            byte[] chatBytes = Encoding.UTF8.GetBytes(chatting);

            // 채팅 메시지 길이를 4바이트 정수로 변환
            int chatLength = chatBytes.Length;
            byte[] chatLengthBytes = BitConverter.GetBytes(chatLength);

            // 최종 바이트 배열 생성
            byte[] finalBytes = new byte[paddedIdBytes.Length + chatLengthBytes.Length + chatBytes.Length];
            Buffer.BlockCopy(paddedIdBytes, 0, finalBytes, 0, paddedIdBytes.Length);
            Buffer.BlockCopy(chatLengthBytes, 0, finalBytes, paddedIdBytes.Length, chatLengthBytes.Length);
            Buffer.BlockCopy(chatBytes, 0, finalBytes, paddedIdBytes.Length + chatLengthBytes.Length, chatBytes.Length);

            return finalBytes;
        }


        /// <summary>
        /// 주어진 데이터 패킷을 파싱하여 ID와 채팅 메시지를 추출합니다.
        /// </summary>
        /// <param name="receivedBytes">수신된 데이터 패킷</param>
        /// <returns>추출된 ID와 채팅 메시지</returns>
        public static (string ID, string Chatting) ParseDataPacket(byte[] receivedBytes)
        {
            // receivedBytes가 null이거나 최소 길이(14바이트)보다 짧은지 확인
            if (receivedBytes == null || receivedBytes.Length < 14)
            {
                throw new ArgumentException("수신된 데이터 패킷이 null이거나 너무 짧습니다.");
            }

            // ID 추출 (UTF-8 인코딩 사용)
            byte[] idBytes = new byte[10];
            Buffer.BlockCopy(receivedBytes, 0, idBytes, 0, 10);
            string ID = Encoding.UTF8.GetString(idBytes).TrimEnd('\0');

            // 채팅 메시지 길이 추출
            byte[] chatLengthBytes = new byte[4];
            Buffer.BlockCopy(receivedBytes, 10, chatLengthBytes, 0, 4);
            int chatLength = BitConverter.ToInt32(chatLengthBytes, 0);

            // 채팅 메시지 길이가 유효한지 확인
            if (chatLength < 0 || receivedBytes.Length < 14 + chatLength)
            {
                throw new ArgumentException("수신된 데이터 패킷의 채팅 메시지 길이가 유효하지 않습니다.");
            }

            // 채팅 메시지 추출
            byte[] chatBytes = new byte[chatLength];
            Buffer.BlockCopy(receivedBytes, 14, chatBytes, 0, chatLength);
            string chatting = Encoding.UTF8.GetString(chatBytes);

            return (ID, chatting);
        }
    }
}
