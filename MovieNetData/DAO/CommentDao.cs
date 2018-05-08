using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieNetData;

namespace MovieNetData.DAO
{
    public class CommentDao
    {
        private static MovieNetModelContainer ctx = new MovieNetModelContainer();

        // Find All
        public List<Comment> FindAllComments
        {
            get
            {
                List<Comment> allComments = new List<Comment>();

                try
                {
                    allComments = ctx.CommentSet.ToList();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Could not found any comment");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error executing query.");
                }

                return allComments;
            }
        }

        // Find by Id
        public Comment FindCommentById(int id)
        {
            Comment CommentById = null;

            try
            {
                CommentById = ctx.CommentSet.Where(c => c.Id == id).Single();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error executing query.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error: could not find Comment with id " + id + ".");
            }

            return CommentById;
        }

        // Find by Criteria
        public List<Comment> FindCommentsByUserId(int userId)
        {
            List<Comment> CommentsByUserId = new List<Comment>();

            try
            {
                CommentsByUserId = ctx.CommentSet.Where(c => c.User.Id == userId).ToList();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error executing query.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error: could not find Comments with UserId " + userId + ".");
            }

            return CommentsByUserId;
        }

        public List<Comment> FindCommentsByFilmId(int filmId)
        {
            List<Comment> CommentsByFilmId = new List<Comment>();

            try
            {
                CommentsByFilmId = ctx.CommentSet.Where(c => c.Film.Id == filmId).ToList();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error executing query.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error: could not find Comments with FilmId " + filmId + ".");
            }

            return CommentsByFilmId;
        }
        // ...

        // CRUD
        public Comment CreateComment(Comment createdComment)
        {
            ctx.CommentSet.Add(createdComment);

            try
            {
                ctx.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Error creating Comment.");
                return null;
            }
            
            return createdComment;
        }

        public Comment UpdateComment(Comment updatedComment)
        {
            Comment CommentToUpdate = FindCommentById(updatedComment.Id);
            updatedComment.ModificationDate = DateTime.Now;

            if (CommentToUpdate != null)
            {
                CommentToUpdate = updatedComment;

                try
                {
                    ctx.SaveChanges();
   
                }
                catch
                {
                    Console.WriteLine("Error updating Comment.");
                    CommentToUpdate = null;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find Comment to update with ID " + updatedComment.Id);
                 CommentToUpdate = null;
            }
            return CommentToUpdate;
        }

        public Boolean DeleteComment(Comment deletedComment)
        {
            Comment CommentToDelete = FindCommentById(deletedComment.Id);

            if (CommentToDelete != null)
            {
                ctx.CommentSet.Remove(CommentToDelete);

                try
                {
                    ctx.SaveChanges();
                    return true;
                }
                catch
                {
                    Console.WriteLine("Error deleting Comment.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find Comment to delete with ID " + deletedComment.Id);
                return false;
            }
        }
    }
}
