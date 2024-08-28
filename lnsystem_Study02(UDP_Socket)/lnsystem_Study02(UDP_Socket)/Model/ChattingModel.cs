using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class ChattingModel
    {
        public List<ChattingData> userDatas;
    }

    public class ChattingData
    {
        public string Time { get; set; }
        public string socketId { get; set; }
        public string Message { get; set; }
    }
}
