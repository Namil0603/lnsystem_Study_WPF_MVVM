using lnsystem_Study04_Style_.Model;
using System.Windows;
using System.Windows.Input;

namespace lnsystem_Study04_Style_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var _ = new MessageModel(); // 메시지 모델 생성
            var __ = new UserDataModel(); // 사용자 데이터 모델 생성(ID)
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}