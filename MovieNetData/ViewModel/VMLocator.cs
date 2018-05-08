using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNetData.ViewModel
{
    //This class is used to contain static references for all the ViewModels in the application. 
    //Is VMLocator who is referenced on App.xaml. It is also de DataContext inside MainWindow.xaml
    public class VMLocator
    {
        public static MainViewModel MainVM { get; } = new MainViewModel();
        public static LoginVM LoginVM { get; } = new LoginVM();
    }
}
