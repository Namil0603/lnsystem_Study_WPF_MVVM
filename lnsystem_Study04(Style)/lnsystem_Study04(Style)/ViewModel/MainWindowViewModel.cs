using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using lnsystem_Study04_Style_.Commands;
using lnsystem_Study04_Style_.View;

namespace lnsystem_Study04_Style_.ViewModel
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

        public ICommand MinimizeCommand { get; }
        public ICommand CloseCommand { get; }

        #endregion

        #region 생성자

        public MainWindowViewModel()
        {
            Instance = this;
            ShowSplashScreen();

            MinimizeCommand = new RelayCommand(MinimizeWindow);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        #endregion

        #region 공개 메서드

        public void ChangeView(object view)
        {
            CurrentView = view;
        }

        public void ChangeTitle(string title)
        {
            WindowTitle = title;
        }

        #endregion

        #region 비공개 메서드

        private async void ShowSplashScreen()
        {
            CurrentView = new SplashView();
            WindowTitle = "LinkNine Talk";

            await Task.Delay(3000);

            CurrentView = new ViewSelectView();
            WindowTitle = "Client/Server Select";
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }

        #endregion
    }
}