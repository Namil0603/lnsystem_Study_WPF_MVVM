using System.Collections.ObjectModel;
using lnsystem_Study02_UDP_Socket_.Data;

namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class MessageModel
    {
        #region 속성

        /// <summary>
        /// 입력 텍스트 박스의 내용을 가져오거나 설정합니다.
        /// </summary>
        public string InputTextBox { get; set; }

        /// <summary>
        /// 채팅 메시지 목록을 가져오거나 설정합니다.
        /// </summary>
        public ObservableCollection<Message> ChattingList { get; set; }

        #endregion

        #region 생성자

        public MessageModel()
        {
            ChattingList = new ObservableCollection<Message>();
        }

        #endregion
    }
}