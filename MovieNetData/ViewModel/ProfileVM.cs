using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieNetData.ViewModel
{
    public class ProfileVM : ViewModelBase
    {
        //TODO: Login or Registration will create an Static User???

        //Constructeur
        public ProfileVM()
        {
            this._password = String.Empty;
            this.PasswordChangedCommand = new RelayCommand<PasswordBox>(PasswordChangerEvent, true);
            this.UpdateCommand = new RelayCommand(UpdateCommandMethod);
            this.BackCommand = new RelayCommand(BackCommandMethod);

        }
        //Buttons
        public ICommand UpdateCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        //_variables(private) and Properties
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        
        //TODO: This value is null for now
        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                //PROF: Should I affect Current User with the password property???
                _currentUser.Password = Password;
                RaisePropertyChanged("CurrentUser");
            }
        }

        //password Logic
        public RelayCommand<PasswordBox> PasswordChangedCommand { get; }

        private void PasswordChangerEvent(PasswordBox passwordBox)
        {
            if (passwordBox != null)
            {
                Console.WriteLine(passwordBox.Password);
                Password = passwordBox.Password;
            }
        }

        public bool isValidInfo()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(Password) == true || Password.Length < 5)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Pasword is required and more than 5 letters or symbols"));
            }
            return isValid;
        }

        //Services
        ServiceFacade serviceFacade = ServiceFacade.Instance;

        //Command Methods
        public void UpdateCommandMethod()
        {
            User user = serviceFacade.UserDao.UpdateUser(CurrentUser);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done, in theory!"));
        }
        public void BackCommandMethod()
        {
            //TODO: Navigation --  Redirect this MainWindowView?
        }

    }
}
