using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace MovieNetData.ViewModel
{
   public class LoginVM : ViewModelBase
    {
        public LoginVM()
        {
            LoginCommand = new RelayCommand(LoginCommandMethod);
            BackCommand = new RelayCommand(BackCommandMethod);
            this._password = String.Empty;
            this.PasswordChangedCommand = new RelayCommand<PasswordBox>(PasswordChangerEvent, true);
            
        }
        public RelayCommand<PasswordBox> PasswordChangedCommand { get; }

        public ICommand LoginCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                Console.WriteLine(value: _username);
                RaisePropertyChanged("Username");
             
            }
        }

        private string _password;

        public string Password
        {
            get { return this._password; }
            set
            {
                    _password = value;
            }
        }

        void M(PasswordBox P)
        { }

        public void BackCommandMethod() {
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("This is the back button"));
        }

        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        public void LoginCommandMethod() {
            if (Username != null && Password != null)
            {
                User user = serviceFacade.UserDao.UserValidation(Username, Password);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Welcome to MovieNew" + Username));
            }
            else
            {
                Console.WriteLine("Username -> " + _username + " pass -> " + Password);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Check your info. Something is wrong"));
            }
        }

        public void PasswordChangerEvent(PasswordBox passwordBox) {
            if (passwordBox != null) {
                Console.WriteLine(passwordBox.Password);
                Password = passwordBox.Password;
            }
        }
    }
}
