using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace MovieNetData.ViewModel
{
    //This class has the content for the property (The label inside MainWindow.xaml called label1)
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Film> films;
        private Film filmSelected;

        public MainViewModel()
        {
            LoadFilmsCommand = new RelayCommand(LoadFilmsCommandMethod);
            SaveFilmsCommand = new RelayCommand(SaveFilmsCommandMethod);
           // MyCommand = new RelayCommand(MyCommandExecute, MyCommandCanExecute);
        }

        public ICommand LoadFilmsCommand { get; private set; }
        public ICommand SaveFilmsCommand { get; private set; }

        private static ServiceFacade serviceFacade = ServiceFacade.Instance;


        public ObservableCollection<Film> FilmsList {

            get { return films; }
        }

        private void LoadFilmsCommandMethod()
        {
            List<Film> listFucking = serviceFacade.FilmDao.findAllFilms();
            if (listFucking != null)
            {
                films = new ObservableCollection<Film>();
                listFucking.ForEach(f => films.Add(f));
            }
            this.RaisePropertyChanged(() => this.FilmsList);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Films Loaded"));
        }

        public Film FilmSelected {
            get {
                return filmSelected;
            }
            set {
                filmSelected = value;
                RaisePropertyChanged("Film Selected");
            }
        }

        public void SaveFilmsCommandMethod()
        {
            if (FilmSelected != null)
            {
                serviceFacade.FilmDao.upadteFilm(filmSelected);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done! In theory..."));
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("You have to select a film"));
            }
        }

        //private string name;

        //public string Name
        //{
        //    get { return name; }
        //    set {
        //        name = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public RelayCommand MyCommand { get;}

        //void MyCommandExecute()
        //{
        //    name = "Welcome to MovieNet";
        //}

        //bool MyCommandCanExecute() {
        //    return true;
        //}
    }
}
