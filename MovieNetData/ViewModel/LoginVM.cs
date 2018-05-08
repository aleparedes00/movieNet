using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MovieNetData.ViewModel
{
   public class LoginVM : ViewModelBase
    {
        
        public LoginVM()
        {
            CurrentUser = new User { Username = "text", Password = "Password"};
            LoginCommand = new RelayCommand(LoginExecute, LoginCanExecute);
        }
        private static ServiceFacade serviceFacade = ServiceFacade.Instance;
        public static User CurrentUser;
        public RelayCommand LoginCommand { get; private set; }

        void LoginExecute()
        {
            Console.WriteLine(CurrentUser.Username);
        }

        bool LoginCanExecute() {
            if (CurrentUser.Username != null)
            { return true;  }
            return false;

        }
    }
}
