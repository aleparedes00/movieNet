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

        public ViewModelBase CurrentView { get; set; }

        public MainViewModel()
        {
            LoadFilmsMethod();
            NewFilmCommand = new RelayCommand(NewFilmCommandMethod);
            SaveFilmsCommand = new RelayCommand(SaveFilmsCommandMethod);
           // MyCommand = new RelayCommand(MyCommandExecute, MyCommandCanExecute);
        }
            
        public ICommand NewFilmCommand { get; private set; }
        public ICommand SaveFilmsCommand { get; private set; }

        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        public ObservableCollection<Film> FilmsList {

            get { return films; }
        }

        private Film newFilm;

        public Film NewFilm
        {
            get { return newFilm; }
            set {
                newFilm = value;
                RaisePropertyChanged("NewFilm");
            }
        }
        //Wrong, the method NewFilmCommandMethod only will send the user to another view. I keep the code to have the reference
        private string title;
        private string genres;
        private string synopsis;

        public void NewFilmCommandMethod() {
            //if (String.IsNullOrEmpty(title) == true) {
            //  Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Plese, introduce a title"));
            //}
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("This will be drive to another page"));
        }
            

        private void LoadFilmsMethod()
        {
            List<Film> listFucking = serviceFacade.FilmDao.FindAllFilms();
            if (listFucking != null)
            {
                films = new ObservableCollection<Film>();
                listFucking.ForEach(f => films.Add(f));
            }
            this.RaisePropertyChanged(() => this.FilmsList);
        }
        
        public Film FilmSelected {
            get {
                return filmSelected;
            }
            set {
                filmSelected = value;
                RaisePropertyChanged("FilmSelected");
            }
        }

        public void SaveFilmsCommandMethod()
        {
            if (FilmSelected != null)
            {
                serviceFacade.FilmDao.UpdateFilm(filmSelected);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done! In theory..."));
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("You have to select a film"));
            }
        }
    }
}
