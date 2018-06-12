using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNetData.ViewModel
{
    public class ProfileVM : ViewModelBase
    {
        //Login will create an Static User???

        //Constructeur
        public ProfileVM()
        {

        }

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }



    }
}
