using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieNetData.ViewModel
{
    public class NewFilmVM : ViewModelBase
    { 
        public NewFilmVM()
        {
            _newFilm = new Film();
            SaveFilmCommand = new RelayCommand(SaveFilmCommandMethod);
            BackButtonCommand = new RelayCommand(BackButtonCommandMethod);
        }
        private static readonly ServiceFacade serviceFacade = ServiceFacade.Instance;
        public ICommand SaveFilmCommand { get; private set; }
        public ICommand BackButtonCommand { get; private set; }

        private Film _newFilm;

        public Film NewFilm
        {
            get { return _newFilm; }
            set
            {
                _newFilm = value;
                RaisePropertyChanged("NewFilm");
            }
        }

        public Boolean IsValidInfo() {
            Boolean isValid = true;
            if (String.IsNullOrWhiteSpace(NewFilm.Title) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Title is a required field"));
            }
            else if (String.IsNullOrWhiteSpace(NewFilm.Genres) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Genres is a required field"));
            }
            else if (String.IsNullOrWhiteSpace(NewFilm.Synopsis) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Synopsis is a required field"));
            }
            return isValid;
        }

        public void SaveFilmCommandMethod()
        {
            if (IsValidInfo() == true)
            {
                serviceFacade.FilmDao.CreateFilm(NewFilm);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done, in theory"));
            }
        }

        public void BackButtonCommandMethod() {
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Going back to main window"));
        }
    }
}
