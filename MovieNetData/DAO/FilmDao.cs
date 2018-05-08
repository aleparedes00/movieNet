using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNetData.DAO
{
    public class FilmDao
    {
        private static MovieNetModelContainer ctx = new MovieNetModelContainer();

        // Find All
        public List<Film> findAllFilms() {

            List<Film> allFilms = new List<Film>();

            try
            {
                allFilms = ctx.FilmSet.ToList();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("[FILM DAO] Nothing found." );
            }
            return allFilms;
        }

        // Find by criteria
        public List<Film> findByGenre(string genre) 
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

        public List<Film> findByTitle(string title)
        {
            List<Film> filmsByTitle = new List<Film>();
            try
            {
                filmsByTitle = ctx.FilmSet.Where(f => f.Titre.Contains(title)).ToList();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("[FILM DAO] Nothing Found under title :" + title);
            }
            return filmsByTitle;
        }

        public Film findById(int id)
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
        public Film createFilm(Film film) {
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

        // Methods with one return. It is advised to make only one return on the method.
        public Film upadteFilm(Film film) {
            Film filmToUpdate = findById(film.Id);

            if (filmToUpdate != null)
            {
                filmToUpdate = film;
                try
                {
                    ctx.SaveChanges();
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
                Console.WriteLine("Couldn't update the film " + film.Titre);
            }
            return filmToUpdate;
        }

        public Boolean deleteFilm(int idFilm) {
            Film filmToDelete = findById(idFilm);
            Boolean isDeleted = false;

            if (filmToDelete != null)
            {
                ctx.FilmSet.Remove(filmToDelete);

                try
                {
                    ctx.SaveChanges();
                    isDeleted = true;
                }
                catch
                {
                    Console.WriteLine("[FILM DAO] Couldn't delete the film " + filmToDelete.Titre);

                }
            }
            else
            {
                Console.WriteLine("[FILM DAO] Couldn't find the film " + filmToDelete.Titre);
            }

            return isDeleted;
        }


    }
}
