using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

    }
}
