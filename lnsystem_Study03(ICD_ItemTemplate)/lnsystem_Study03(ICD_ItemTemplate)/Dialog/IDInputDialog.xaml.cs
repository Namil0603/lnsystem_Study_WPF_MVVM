﻿using System.Text;
using System.Windows;
using System.Windows.Media;

namespace lnsystem_Study03_ICD_ItemTemplate_.Dialog
{
    /// <summary>
    /// ID 입력 다이얼로그 창입니다.
    /// </summary>
    public partial class IDInputDialog : Window
    {
        #region 속성

        /// <summary>
        /// 사용자가 입력한 ID를 가져옵니다.
        /// </summary>
        public string UserID { get; private set; }

        #endregion

        #region 생성자

        /// <summary>
        /// IDInputDialog의 생성자입니다.
        /// </summary>
        public IDInputDialog() => InitializeComponent();

        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// ID 텍스트 박스의 내용이 변경될 때 호출됩니다.
        /// </summary>
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

        /// <summary>
        /// 제출 버튼이 클릭될 때 호출됩니다.
        /// </summary>
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

        #endregion
    }
}