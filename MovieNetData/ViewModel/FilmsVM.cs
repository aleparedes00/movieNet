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
    public class FilmsVM : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        readonly static NewFilmVM _newFilmViewModel = new NewFilmVM();
        readonly static ProfileVM _profileViewModel = new ProfileVM();

        //_variables
        private ObservableCollection<Film> _films;
        private ICollection<Comment> comentarios;
        private List<Comment> _comments;
        private Film _filmSelected;
        public Film filmToUpdate;
        private string _title = "";
        private string _genres = "";
        private string _synopsis = "";
        private string _director = "";
        private short _year;
        private double _score;

        public ViewModelBase CurrentViewModel {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    RaisePropertyChanged("CurrentViewModel");
                }
            }
        }

        //Buttons
        public ICommand NewFilmCommand { get; private set; }
        public ICommand SaveFilmsCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }

        //Services
        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        //Constructeur
        public FilmsVM()
        {
            //TODO PROF: How should I create the access to the comments
            filmToUpdate = null;
            _filmSelected = new Film(_title, _genres, _synopsis, _director);
            comentarios = new HashSet<Comment>();
            _comments = new List<Comment>();

            //Create the films to show
            LoadFilmsMethod();
            //Commands
            NewFilmCommand = new RelayCommand(NewFilmCommandMethod);
            SaveFilmsCommand = new RelayCommand(SaveFilmsCommandMethod);
            ProfileCommand = new RelayCommand(ProfileCommandMethod);
        }

        //Properties
        public ObservableCollection<Film> FilmsList {

            get { return _films; }
            set
            {
                //LoadFilmsMethod();
                RaisePropertyChanged(() => FilmsList);
            }
        }
        public Film FilmSelected
        {
            get
            {
                return _filmSelected;
            }
            set
            {
                _filmSelected = value;
                //_comments = serviceFacade.GetFilmComments(filmSelected.Id);
                //affectComment();
                RaisePropertyChanged("FilmSelected");
            }
        }

        private void LoadFilmsMethod()
        {
            List<Film> listDao = serviceFacade.FilmDao.FindAllFilms();
            if (listDao != null)
            {
                _films = new ObservableCollection<Film>();
                listDao.ForEach(f => _films.Add(f));
            }
            RaisePropertyChanged(() => FilmsList);
        }

        //HasSet approch
        public HashSet<Comment> Comentarios
        {
            get { return (HashSet<Comment>) comentarios; }
            set { comentarios = value;
                RaisePropertyChanged("Comemtarios");
            }
        }
        //List approch
        public List<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value;
                RaisePropertyChanged("Comments");
            }
        }

        public void affectComment()
        {
            FilmSelected.Comment = Comentarios;
            //User user = serviceFacade.UserDao.FindUserById(1);
            //user.Comment = serviceFacade.GetUserComments(user.Id);
            //Film film = FilmSelected;
            //var id = FilmSelected.Id;
            //FilmSelected.Comment = serviceFacade.GetFilmComments(id);
            //Comment testComment = new Comment("I liked it", user, film);
            //_comments.Add(testComment);
        }

        //Buttons Methods
        public void NewFilmCommandMethod()
        {
            CurrentViewModel = FilmsVM._newFilmViewModel;
        }

        public Boolean IsValidInfo()
        {
            Boolean isValid = true;
            if (String.IsNullOrWhiteSpace(FilmSelected.Title) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Title is a required field"));
            }
            else if (String.IsNullOrWhiteSpace(FilmSelected.Genres) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Genres is a required field"));
            }
            else if (String.IsNullOrWhiteSpace(FilmSelected.Synopsis) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Synopsis is a required field"));
            }
            else if (String.IsNullOrWhiteSpace(FilmSelected.Director) == true)
            {
                isValid = false;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Director is a required field"));
            }
            if (isValid == true)
            {
                //filmToUpdate = new Film();
                filmToUpdate = serviceFacade.FilmDao.FindFilmById(FilmSelected.Id);
                filmToUpdate.Title = FilmSelected.Title;
                filmToUpdate.Genres = FilmSelected.Genres;
                filmToUpdate.Synopsis = FilmSelected.Synopsis;
                filmToUpdate.Director = FilmSelected.Director;
                if (FilmSelected.Score != null)
                {
                    filmToUpdate.Score = FilmSelected.Score;
                }
                if (FilmSelected.Year != null)
                {
                    filmToUpdate.Year = FilmSelected.Year;
                }
            }
            return isValid;
        }

        public void SaveFilmsCommandMethod()
        {
            if (IsValidInfo())
            {
                if (filmToUpdate != null)
                {
                    var film = serviceFacade.FilmDao.UpdateFilm(filmToUpdate);
                    Console.WriteLine(film.Director);//Test
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done! In theory..."));
                }
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("You have to select a film"));
            }
        }

        public void ProfileCommandMethod()
        {
            CurrentViewModel = FilmsVM._profileViewModel;
        }


    }
}
