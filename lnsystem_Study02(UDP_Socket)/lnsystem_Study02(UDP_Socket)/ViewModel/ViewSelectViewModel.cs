using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using lnsystem_Study02_UDP_Socket_.Tools;
using lnsystem_Study02_UDP_Socket_.View;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public class ViewSelectViewModel : ViewModelBase
    {
        public ICommand SocketClientClickCommand { get; }
        public ICommand SocketServerClickCommand { get; }
        
        public ViewSelectViewModel()
        {
            SocketClientClickCommand = new RelayCommand(SocketClientClick);
            SocketServerClickCommand = new RelayCommand(SocketServerClick);
        }
        
        private void SocketClientClick()
        {
            MainWindowViewModel.Instance?.ChangeTitle("Socket UDP Client");
            MainWindowViewModel.Instance?.ChangeView(new ChattingView(Status.Client));
        }
        
        private void SocketServerClick()
        {
            MainWindowViewModel.Instance?.ChangeTitle("Socket UDP Server");
            MainWindowViewModel.Instance?.ChangeView(new ChattingView(Status.Server));
        }
    }
}
