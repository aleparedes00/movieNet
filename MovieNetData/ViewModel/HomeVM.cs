using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieNetData.ViewModel
{
    public class HomeVM : ViewModelBase
    {
        private UserControl _registration;
        private int index;

        public RelayCommand ChangeViewCommand { get; }

        public HomeVM()
        {
            this._registration = null;
            this.index = 0;

            this.ChangeViewCommand = new RelayCommand(ChangeView, true);
            RegistrationCommand = new RelayCommand(RegistrationCommandMethod);
        }

        public UserControl Registration
        {
            get { return this._registration; }
            set
            {
                this._registration = value;
                this.RaisePropertyChanged("Registration");
            }
        }

        public void ChangeView()
        {
            if ((this.index % 2) == 0)
            {
                //Registration = new ;
            }
        }

        public ICommand RegistrationCommand { get; private set; }

        public void RegistrationCommandMethod()
        {
            
        }
    }
}
