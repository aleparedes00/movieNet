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
            _userDao = new UserDao();
            _filmDao = new FilmDao();
            _commentDao = new CommentDao();
        }

        public static ServiceFacade Instance
        {
            get
            {
                if (INSTANCE == null) INSTANCE = new ServiceFacade();
                return INSTANCE;
            }
        }

        public UserDao UserDao {
            get
            {
                return _userDao;
            }
        }

        public FilmDao FilmDao {
            get
            {
                return _filmDao;
            }
        }

        public CommentDao CommentDao {
            get
            {
                return _commentDao;
            }
        }

    }
}
