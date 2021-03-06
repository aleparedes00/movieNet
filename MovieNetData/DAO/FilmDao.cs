﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNetData.DAO
{
    public class FilmDao
    {
        // Find All
        public List<Film> FindAllFilms()
        {
            using (var ctx = new MovieNetModelContainer())
            {
                List<Film> allFilms = new List<Film>();

                try
                {
                    allFilms = ctx.FilmSet.ToList();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("[FILM DAO] Nothing found.");
                }
                return allFilms;
            }
        }

        // Find by criteria
        public List<Film> FindFilmsByGenre(string genre) 
        {
            using (var ctx = new MovieNetModelContainer())
            {
                List<Film> filmsByGenre = new List<Film>();
                try
                {
                    filmsByGenre = ctx.FilmSet.Where(f => f.Genres.Contains(genre)).ToList();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("[FILM DAO] Nothing Found under genre: " + genre);
                }
                return filmsByGenre;
            }
        }

        public List<Film> FindFilmsByTitle(string title)
        {
            using (var ctx = new MovieNetModelContainer())
            {
                List<Film> filmsByTitle = new List<Film>();
                try
                {
                    filmsByTitle = ctx.FilmSet.Where(f => f.Title.Contains(title)).ToList();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("[FILM DAO] Nothing Found under title :" + title);
                }
                return filmsByTitle;
            }
        }

        public Film FindFilmById(int id)
        {
            using (var ctx = new MovieNetModelContainer())
            {
                Film filmById = null;
                try
                {
                    filmById = ctx.FilmSet.Where(f => f.Id.Equals(id)).Single();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("[FILM DAO] Nothing Found under id:" + id);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("[FILM DAO] Film not found");
                }
                return filmById;
            }
        }

        private Film FindFilmByIdPrivate(int id, MovieNetModelContainer ctx)
        {
            
                Film filmById = null;
                try
                {
                    filmById = ctx.FilmSet.Where(f => f.Id.Equals(id)).Single();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("[FILM DAO] Nothing Found under id:" + id);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("[FILM DAO] Film not found");
                }
                return filmById;
            
        }
        // CRUD
        public Film CreateFilm(Film film) {
            using (var ctx = new MovieNetModelContainer())
            {
                ctx.FilmSet.Add(film);

                try
                {
                    ctx.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("[FILM DAO] Error executing query");
                    return null;
                }
                return film;
            }
        }

        // Methods with one return. It is advised to make only one return on the method.
        public Film UpdateFilm(Film film) {
            using (var ctx = new MovieNetModelContainer())
            {
                Film filmToUpdate = FindFilmByIdPrivate(film.Id, ctx);
                filmToUpdate.Id = film.Id;
                filmToUpdate.Title = film.Title;
                filmToUpdate.Genres = film.Genres;
                filmToUpdate.Synopsis = film.Synopsis;
                filmToUpdate.Score = film.Score;
                filmToUpdate.Year = film.Year;
                filmToUpdate.Director = film.Director;

                if (filmToUpdate != null)
                {
                    try
                    {
                        //ctx.Entry(filmToUpdate).State = EntityState.Modified;
                        int num = ctx.SaveChanges();
                        Console.WriteLine(num);
                    }
                    catch
                    {
                        Console.WriteLine("[FILM DAO] Error updating.");
                        filmToUpdate = null;
                    }
                }
                else
                {
                    filmToUpdate = null;
                    Console.WriteLine("Couldn't update the film " + film.Title);
                }
                return filmToUpdate;
            }
        }

        public Boolean DeleteFilm(Film deletedFilm) {
            using (var ctx = new MovieNetModelContainer())
            {
                Film filmToDelete = FindFilmByIdPrivate(deletedFilm.Id, ctx);
                filmToDelete.Id = deletedFilm.Id;
                filmToDelete.Title = deletedFilm.Title;
                filmToDelete.Genres = deletedFilm.Genres;
                filmToDelete.Synopsis = deletedFilm.Synopsis;
                filmToDelete.Score = deletedFilm.Score;
                filmToDelete.Year = deletedFilm.Year;
                filmToDelete.Director = deletedFilm.Director;
                Boolean isDeleted = false;

                if (filmToDelete != null)
                {
                    //ctx.FilmSet.Attach(filmToDelete);
                    ctx.FilmSet.Remove(filmToDelete);

                    try
                    {
                        ctx.SaveChanges();
                        isDeleted = true;
                    }
                    catch
                    {
                        Console.WriteLine("[FILM DAO] Couldn't delete the film " + filmToDelete.Title);

                    }
                }
                else
                {
                    Console.WriteLine("[FILM DAO] Couldn't find the film " + filmToDelete.Title);
                }

                return isDeleted;
            }
        }
    }
}
