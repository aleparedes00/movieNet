using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNetData.ViewModel
{
    //This class is used to contain static references for all the ViewModels in the application. 
    //Is VMLocator who is referenced on App.xaml. It is also de DataContext inside MainWindow.xaml

    public class VMLocator
    {
        public VMLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginVM>();
            SimpleIoc.Default.Register<NewFilmVM>();
            SimpleIoc.Default.Register<RegistrationVM>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }
        public RegistrationVM RegistrationVM {
            get { return ServiceLocator.Current.GetInstance<RegistrationVM>(); }
        }

        public LoginVM LoginVM
        {
            get { return ServiceLocator.Current.GetInstance<LoginVM>();  }
        }

        public MainViewModel MainVM
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }

        }

        public NewFilmVM NewFilmVM
        {
            get { return ServiceLocator.Current.GetInstance<NewFilmVM>(); }
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }

    }
}
