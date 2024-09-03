using System.Windows;
using lnsystem_Study03_ICD_ItemTemplate_.Model;

namespace lnsystem_Study03_ICD_ItemTemplate_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var _ = new MessageModel(); // 메시지 모델 생성
            var __ = new UserDataModel(); // 사용자 데이터 모델 생성(ID)
        }
    }
}