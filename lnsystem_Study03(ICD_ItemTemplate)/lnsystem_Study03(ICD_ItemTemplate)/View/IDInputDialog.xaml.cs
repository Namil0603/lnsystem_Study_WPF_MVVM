using System.Text;
using System.Windows;
using System.Windows.Media;

namespace lnsystem_Study03_ICD_ItemTemplate_.View
{
    public partial class IDInputDialog : Window
    {
        public string UserID { get; private set; }

        public IDInputDialog() => InitializeComponent();

        private void IDTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int byteCount = Encoding.UTF8.GetByteCount(IDTextBox.Text);
            ByteCountTextBlock.Text = $"입력한 ID의 바이트 수: {byteCount}";

            if (byteCount > 10)
            {
                ByteCountTextBlock.Foreground = Brushes.Red;
                ByteCountTextBlock.Text += " (10바이트를 초과할 수 없습니다)";
            }
            else
            {
                ByteCountTextBlock.Foreground = Brushes.Black;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int byteCount = Encoding.UTF8.GetByteCount(IDTextBox.Text);
            if (byteCount <= 10)
            {
                UserID = IDTextBox.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("ID는 10바이트를 초과할 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}