using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using lnsystem_Study02_UDP_Socket_.Model;
using lnsystem_Study02_UDP_Socket_.Tools;

namespace lnsystem_Study02_UDP_Socket_.ViewModel
{
    public enum Status
    {
        Server,
        Client
    }

    public class ChattingViewModel : ViewModelBase
    {
        public ICommand SendMessageCommand { get; }
        public Status CurrentStatus { get; }
        private ChattingModel _chattingModel;
        public ObservableCollection<string> Messages { get; }
        public ObservableCollection<User> Users => _chattingModel.Users;
        public string NewMessage
        {
            get => _chattingModel.NewMessage;
            set
            {
                _chattingModel.NewMessage = value;
                OnPropertyChanged(nameof(NewMessage));
            }
        }

        public ChattingViewModel(Status status)
        {
            CurrentStatus = status;
            _chattingModel = new ChattingModel(status);
            Messages = new ObservableCollection<string>();
            SendMessageCommand = new RelayCommand(SendMessage);

            _chattingModel.MessageReceived += OnMessageReceived;
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            string message = NewMessage;
            Messages.Add(new Message("Me", message).ToString());

            await _chattingModel.SendMessageAsync(message, CurrentStatus);

            NewMessage = string.Empty;
            OnPropertyChanged(nameof(NewMessage));
        }

        private void OnMessageReceived(Message message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(message.ToString());
            });
        }
    }
}