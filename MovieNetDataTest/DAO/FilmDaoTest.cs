﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieNetData;

namespace MovieNetDataTest.DAO
{
    /// <summary>
    /// Description résumée pour FilmDaoTest
    /// </summary>
    [TestClass]
    public class FilmDaoTest
    {
        private static MovieNetData.ViewModel.ServiceFacade serviceFacade = null;

        private static readonly int NB_FILMS = 5;
        private static readonly string FIND_FILM_BY_NAME = "The Club";
        private static readonly string FIND_FILM_BY_GENRES = "Comedy";
        private static readonly int NB_FILMS_BY_GENRES = 2;



        [TestInitialize]
        public void Init()
        {
            serviceFacade = MovieNetData.ViewModel.ServiceFacade.Instance;
        }

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
       
        [TestMethod]
        public void TestFindAllFilms()
        {
            List<Film> listFilms = serviceFacade.FilmDao.FindAllFilms();

            if (listFilms.Count > 0) {
                Assert.AreEqual(NB_FILMS, listFilms.Count);
            }
        }

        // Find by Criteria tests

        [TestMethod]
        public void testFindByCriteia()
        {
            List<Film> films = serviceFacade.FilmDao.FindFilmsByTitle(FIND_FILM_BY_NAME);
            Assert.AreEqual(FIND_FILM_BY_GENRES, films.Find(f => f.Title.Equals(FIND_FILM_BY_NAME)).Genres);

            List<Film> filmByGenres = serviceFacade.FilmDao.FindFilmsByGenre(FIND_FILM_BY_GENRES);
            Assert.AreEqual(NB_FILMS_BY_GENRES, filmByGenres.Count);
            
        }

        [TestMethod]
        public void TestCRUDFilm()
        {
            // Create
            Film filmCRUD = new Film("The Jungle Book", "Animation", "Bagheera the Panther and Baloo the Bear have a difficult time trying to convince a boy to leave the jungle for human civilization.", "Wolfgang Reitherman");

            filmCRUD = serviceFacade.FilmDao.CreateFilm(filmCRUD);
            Assert.IsNotNull(filmCRUD);
            Assert.AreEqual("The Jungle Book", filmCRUD.Title);
            Assert.AreEqual("Animation", filmCRUD.Genres);

            // Update
            filmCRUD.Genres = "Adventure";
            filmCRUD = serviceFacade.FilmDao.UpdateFilm(filmCRUD);
            Assert.IsNotNull(filmCRUD);
            Assert.AreEqual("Adventure", filmCRUD.Genres);

            // Delete

            Assert.IsTrue(serviceFacade.FilmDao.DeleteFilm(filmCRUD));
            Assert.IsNull(serviceFacade.FilmDao.FindFilmById(filmCRUD.Id));
            List<Film> finalFilms = serviceFacade.FilmDao.FindAllFilms();
            if (finalFilms != null)
            {
                Assert.AreEqual(NB_FILMS, finalFilms.Count);
            }
            else Assert.Fail();
        }
    }
}
