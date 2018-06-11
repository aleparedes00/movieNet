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
        private List<Comment> _comments;
        private Film filmSelected;


        public ViewModelBase CurrentView { get; set; }

        public MainViewModel()
        {
            LoadFilmsMethod();
            _comments = new List<Comment>();
            NewFilmCommand = new RelayCommand(NewFilmCommandMethod);
            SaveFilmsCommand = new RelayCommand(SaveFilmsCommandMethod);
        }
            
        public ICommand NewFilmCommand { get; private set; }
        public ICommand SaveFilmsCommand { get; private set; }
        

        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        public ObservableCollection<Film> FilmsList {

            get { return films; }
        }

        public List<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value;
                RaisePropertyChanged("Comments");
            }
        }
        public void NewFilmCommandMethod() {
            
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("This will be drive to another page"));
        }

        public void affectComment()
        {
            User user = serviceFacade.UserDao.FindUserById(1);
            user.Comment = serviceFacade.GetUserComments(user.Id);
            Film film = FilmSelected;
            var id = FilmSelected.Id;
            FilmSelected.Comment = serviceFacade.GetFilmComments(id);
            //Comment testComment = new Comment("I liked it", user, film);
            //_comments.Add(testComment);
        }

        private void LoadFilmsMethod()
        {
            List<Film> listDao = serviceFacade.FilmDao.FindAllFilms();
            if (listDao != null)
            {
                films = new ObservableCollection<Film>();
                listDao.ForEach(f => films.Add(f));
            }
            this.RaisePropertyChanged(() => this.FilmsList);
        }
        
        public Film FilmSelected {
            get {
                return filmSelected;
            }
            set {
                filmSelected = value;
                //_comments = serviceFacade.GetFilmComments(filmSelected.Id);
                affectComment();
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
