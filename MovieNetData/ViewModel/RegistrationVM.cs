using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieNetData.ViewModel
{
    public class RegistrationVM : ViewModelBase
    {
        //TODO: Navigation Missing button for back (to home view)

        public RegistrationVM()
        {
            RegistrationCommand = new RelayCommand(RegistrationButtonMethod);
            this._password = String.Empty;
            this.PasswordChangedCommand = new RelayCommand<PasswordBox>(PasswordChangerEvent, true);
        }
        //Buttons
        public ICommand RegistrationCommand { get; private set; }

        //_variables and Properties
        private string _username;

        public string Username
        {
            get { return _username; }
            set {
                _username = value;
                Console.WriteLine(value: _username);
                RaisePropertyChanged("Username");
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        //Password Logic
        private void PasswordChangerEvent(PasswordBox passwordBox)
        {
            if (passwordBox != null)
            {
                Console.WriteLine(passwordBox.Password);
                Password = passwordBox.Password;
            }
        }

        public RelayCommand<PasswordBox> PasswordChangedCommand { get; }

        public bool isValidInfo()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(Username) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Username is required field"));
            }
            else if (string.IsNullOrWhiteSpace(Password) == true || Password.Length < 5)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Pasword is required and more than 5 letters or symbols"));
            }
            return isValid;
        }

        //Services
        ServiceFacade servicefacade = ServiceFacade.Instance;
        
        //Command Methods
        public void RegistrationButtonMethod() {
            User user = new User(Username, Password);
            servicefacade.CreateUser(user);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done, in theory!"));
        }

    }
}