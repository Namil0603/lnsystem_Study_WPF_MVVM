using lnsystem_Study02_UDP_Socket_.View;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region 멤버 변수

        public static MainWindowViewModel? Instance;
        private object _currentView = null!;
        private string _windowTitle = null!;

        #endregion

        #region 속성

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string WindowTitle
        {
            get => _windowTitle;
            set
            {
                _windowTitle = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 생성자

        public MainWindowViewModel()
        {
            Instance = this;
            CurrentView = new ViewSelectView();
            WindowTitle = "Client/Server Select";
        }

        #endregion

        #region 공개 메서드

        /// <summary>
        /// 현재 뷰를 변경합니다.
        /// </summary>
        /// <param name="view">변경할 뷰 객체</param>
        public void ChangeView(object view)
        {
            CurrentView = view;
        }

        /// <summary>
        /// 윈도우 타이틀을 변경합니다.
        /// </summary>
        /// <param name="title">변경할 타이틀 문자열</param>
        public void ChangeTitle(string title)
        {
            WindowTitle = title;
        }

        #endregion
    }
}