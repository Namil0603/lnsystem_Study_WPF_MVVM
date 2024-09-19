using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace lnsystem_Study04_Style_.View
{
    /// <summary>
    /// SplashView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SplashView : UserControl
    {
        public SplashView()
        {
            InitializeComponent();
            StartFadeOutAnimation();
        }

        private void StartFadeOutAnimation()
        {
            var storyboard = (Storyboard)FindResource("FadeOutStoryboard");
            storyboard.Begin();
        }
    }
}