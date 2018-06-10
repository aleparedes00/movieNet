using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MovieNetData.DAO;

namespace MovieNetData.ViewModel
{
    public class ServiceFacade : ViewModelBase
    {
        private static ServiceFacade INSTANCE = null;

        private UserDao _userDao;
        private FilmDao _filmDao;
        private CommentDao _commentDao;

        private ServiceFacade()
        {
            UserDao = new UserDao();
            FilmDao = new FilmDao();
            CommentDao = new CommentDao();
        }

        public static ServiceFacade Instance
        {
            get
            {
                if (INSTANCE == null) INSTANCE = new ServiceFacade();
                return INSTANCE;
            }
        }

        public UserDao UserDao { get; set; }

        public FilmDao FilmDao { get; set; }

        public CommentDao CommentDao { get; set; }

        // Queries

        public User GetUserFromId(int userId)
        {
            User user = UserDao.FindUserById(userId);

            // Retrieve Comments
            if (user != null)
            {
                user.Comment = GetUserComments(userId);
            }

            return user;
        }

        public Film GetFilmFromId(int filmId)
        {
            Film film = FilmDao.FindFilmById(filmId);

            // Retrieve Comments
            if (film != null)
            {
                film.Comment = GetFilmComments(filmId);
            }

            return film;
        }

        public Comment GetCommentFromId(int commentId)
        {
            return CommentDao.FindCommentById(commentId);
        }

        public List<Comment> GetFilmComments(int filmId)
        {
            return CommentDao.FindCommentsByFilmId(filmId);
        }

        public List<Comment> GetUserComments(int userId)
        {
            return CommentDao.FindCommentsByUserId(userId);
        }

        // Creation

        public User CreateUser(User userToCreate)
        {
            // Check if user with same username already exists
            if (UserDao.FindUserByUsername(userToCreate.Username) != null)
            {
                Console.WriteLine("Error: username " + userToCreate.Username + " already taken.");
                return null;
            }

            return UserDao.CreateUser(userToCreate);
        }

        public Film CreateFilm(Film filmToCreate)
        {
            // Check if film already exists: same title and year
            List<Film> filmsWithSameTitle = FilmDao.FindFilmsByTitle(filmToCreate.Title);
            if (filmsWithSameTitle.Count(f => f.Year == filmToCreate.Year) > 0)
            {
                Console.WriteLine("Error: film " + filmToCreate.Title + "(" + filmToCreate.Year + ") already exists.");
                return null;
            }

            return FilmDao.CreateFilm(filmToCreate);
        }

        public Comment AddComment(Comment commentToAdd)
        {
            Comment addedComment = CommentDao.CreateComment(commentToAdd);

            if (addedComment != null)
            {
                // Add comment to film and user
                addedComment.Film.Comment.Add(addedComment);
                addedComment.User.Comment.Add(addedComment);

                // Update film's score if user added a score
                if (addedComment.Score != null)
                {
                    UpdateScore(addedComment.Film);
                }
            }

            return addedComment;
        }

        // Update

        public void UpdateScore(Film filmToUpdate)
        {
            if (filmToUpdate.Comment.Count(c => c.Score != null) == 0)
                filmToUpdate.Score = null;

            else
                filmToUpdate.Score = ((double)filmToUpdate.Comment.Sum(c => (c.Score != null) ? c.Score : 0))
                    / filmToUpdate.Comment.Count(c => c.Score != null);

            FilmDao.UpdateFilm(filmToUpdate);
        }

        // Deletion

        public Boolean DeleteUser(User userToDelete)
        {
            // TODO: Do we keep comments by a deleted user?
            List<Comment> toDelete = CommentDao.FindCommentsByUserId(userToDelete.Id);
            foreach (Comment c in toDelete)
            {
                DeleteComment(c);
            }

            return UserDao.DeleteUser(userToDelete);
        }

        public Boolean DeleteFilm(Film filmToDelete)
        {
            List<Comment> toDelete = CommentDao.FindCommentsByFilmId(filmToDelete.Id);
            foreach (Comment c in toDelete)
            {
                DeleteComment(c);
            }

            return FilmDao.DeleteFilm(filmToDelete);
        }

        public Boolean DeleteComment(Comment commentToDelete)
        {
            Boolean deletionSuccessful = CommentDao.DeleteComment(commentToDelete);

            if (deletionSuccessful)
            {
                commentToDelete.User.Comment.Remove(commentToDelete);
                commentToDelete.Film.Comment.Remove(commentToDelete);
                UpdateScore(commentToDelete.Film);
            }

            return deletionSuccessful;
        }
    }
}
