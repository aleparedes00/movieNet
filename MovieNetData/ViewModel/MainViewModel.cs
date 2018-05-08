using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MovieNetData.ViewModel
{
    //This class has the content for the property (The label inside MainWindow.xaml called label1)
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            
            MyCommand = new RelayCommand(MyCommandExecute, MyCommandCanExecute);
        }

        private static ServiceFacade serviceFacade = ServiceFacade.Instance;

        private List<Film> _filmList = serviceFacade.FilmDao.findAllFilms();

        public List<Film> Name
        {
            get { return _filmList; }
            set {
                _filmList = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand MyCommand { get;}

        void MyCommandExecute()
        {
            Name.Add(new Film());  
        }

        bool MyCommandCanExecute() {
            return true;
        }
    }
}
