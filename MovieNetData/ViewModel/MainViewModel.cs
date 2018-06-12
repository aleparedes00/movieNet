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
        private ICollection<Comment> comentarios;
        private List<Comment> _comments;
        private Film filmSelected;
        private string _title = "";
        private string _genres = "";
        private string _synopsis = "";
        private string _director = "";
        private short _year;
        private double _score;

        public ViewModelBase CurrentView { get; set; }

        //Constrocteur
        public MainViewModel()
        {
            filmSelected = new Film(_title, _genres, _synopsis, _director);
            comentarios = new HashSet<Comment>();
            LoadFilmsMethod();
            _comments = new List<Comment>();
            NewFilmCommand = new RelayCommand(NewFilmCommandMethod);
            SaveFilmsCommand = new RelayCommand(SaveFilmsCommandMethod);
            ProfileCommand = new RelayCommand(ProfileCommandMethod);
        }
         
        //Buttons
        public ICommand NewFilmCommand { get; private set; }
        public ICommand SaveFilmsCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }

        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        public ObservableCollection<Film> FilmsList {

            get { return films; }
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

        //Buttons Methods
        public void NewFilmCommandMethod() {
            
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("This will be drive to another page"));
        }

        public void SaveFilmsCommandMethod()
        {
            if (FilmSelected != null)
            {
                var film = serviceFacade.FilmDao.UpdateFilm(filmSelected);
                Console.WriteLine(film.Director);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done! In theory..."));
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("You have to select a film"));
            }
        }

        public void ProfileCommandMethod()
        {
            //Go to ProfileVM
        }

        public Film FilmSelected
        {
            get
            {
                return filmSelected;
            }
            set
            {

                filmSelected = value;
                //_comments = serviceFacade.GetFilmComments(filmSelected.Id);
                //affectComment();
                RaisePropertyChanged("FilmSelected");
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
        
 

    }
}
