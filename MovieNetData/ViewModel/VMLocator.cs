﻿using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
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
            SimpleIoc.Default.Register<FilmsVM>();
            SimpleIoc.Default.Register<LoginVM>();
            SimpleIoc.Default.Register<NewFilmVM>();
            SimpleIoc.Default.Register<RegistrationVM>();
            SimpleIoc.Default.Register<HomeVM>();
            SimpleIoc.Default.Register<ProfileVM>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }

        public FilmsVM FilmsVM
        {
            get { return ServiceLocator.Current.GetInstance<FilmsVM>(); }
        }

        public ProfileVM ProfileVM
        {
            get { return ServiceLocator.Current.GetInstance<ProfileVM>(); }
        }
        public HomeVM HomeVM {
            get { return ServiceLocator.Current.GetInstance<HomeVM>(); }
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
