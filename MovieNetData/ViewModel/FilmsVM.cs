﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand DeleteFilmCommand { get; private set; }

        //Services
        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        //Constructeur
        public FilmsVM()
        {
            filmToUpdate = null;
            _filmSelected = new Film(_title, _genres, _synopsis, _director);
            comentarios = new HashSet<Comment>();
            _comments = new List<Comment>();

            //Create the films to show
            if (FilmsList == null)
            {
                FilmsList = new ObservableCollection<Film>();
            }
            LoadFilmsMethod();

            //Commands
            NewFilmCommand = new RelayCommand(NewFilmCommandMethod);
            SaveFilmsCommand = new RelayCommand(SaveFilmsCommandMethod);
            ProfileCommand = new RelayCommand(ProfileCommandMethod);
            DeleteFilmCommand = new RelayCommand(DeleteFilmCommandMethod);
        }

        //Properties
        public ObservableCollection<Film> FilmsList
        {
            get { return _films; }
            set
            {
                _films = value;
                RaisePropertyChanged("FilmsList");
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
                if (value != null)
                    Comments = serviceFacade.GetFilmComments(_filmSelected.Id);
                RaisePropertyChanged("FilmSelected");
            }
        }

        private void LoadFilmsMethod()
        {
            FilmsList.Clear();
            List<Film> listDao = serviceFacade.FilmDao.FindAllFilms();
            if (listDao != null)
            {
                listDao.ForEach(f => FilmsList.Add(f));

                //if (listDao.Count > FilmsList.Count) { 
                //    for (int i = FilmsList.Count; i < listDao.Count - FilmsList.Count; i++)
                //        FilmsList.Add(listDao[i]);
                //}
            }
        }

        public void UpdateFilms()
        {
            LoadFilmsMethod();
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
                    
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done! In theory..."));
                }
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("You have to select a film"));
            }
        }

        public void DeleteFilmCommandMethod()
        {
            if (String.IsNullOrWhiteSpace(FilmSelected.Title))
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("You have to select a film"));
            }
            else
            {
                var result = serviceFacade.FilmDao.DeleteFilm(FilmSelected);
                if (result)
                {
                    FilmsList.Remove(FilmSelected);
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Done, in theory"));
                }
            }
        }

        public void ProfileCommandMethod()
        {
            CurrentViewModel = FilmsVM._profileViewModel;
        }


    }
}
