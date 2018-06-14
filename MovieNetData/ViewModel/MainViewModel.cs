using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace MovieNetData.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        readonly static FilmsVM _filmsViewModel = new FilmsVM();
        readonly static NewFilmVM _newFilmViewModel = new NewFilmVM();
        readonly static ProfileVM _profileViewModel = new ProfileVM();

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

        private string _test = "Le binding a réussi !";
        public int Test { get; set; }

        //Buttons
        public ICommand FilmsViewCommand { get; private set; }
        public ICommand NewFilmViewCommand { get; private set; }
        public ICommand ProfileViewCommand { get; private set; }

        //Constructeur
        public MainViewModel()
        {
            //Commands
            FilmsViewCommand = new RelayCommand(FilmsViewCommandMethod);
            NewFilmViewCommand = new RelayCommand(NewFilmViewCommandMethod);
            ProfileViewCommand = new RelayCommand(ProfileViewCommandMethod);
        }

        //Buttons Methods
        public void FilmsViewCommandMethod()
        {
            CurrentViewModel = _filmsViewModel;
        }

        public void NewFilmViewCommandMethod()
        {
            CurrentViewModel = _newFilmViewModel;
        }

        public void ProfileViewCommandMethod()
        {
            CurrentViewModel = _profileViewModel;
        }
    }
}
