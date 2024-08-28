using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnsystem_Study02_UDP_Socket_.View;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Instance = this;
            CurrentView = new ViewSelectView();
            WindowTitle = "Client/Server Select";
        }

        public static MainWindowViewModel? Instance;
        private object _currentView;
        private string _windowTitle;
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
        public void ChangeView(object view)
        {
            CurrentView = view;
        }
        public void ChangeTitle(string title)
        {
            WindowTitle = title;
        }
    }
}
