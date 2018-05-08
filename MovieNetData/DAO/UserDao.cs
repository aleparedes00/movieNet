using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNetData.DAO
{
    // Ref: https://www.tutorialspoint.com/entity_framework/entity_framework_dbcontext.htm
    public class UserDao : INotifyPropertyChanged 
    {
        private static MovieNetModelContainer ctx = new MovieNetModelContainer();

        public event PropertyChangedEventHandler PropertyChanged;

        // Find All
        public List<User> FindAllUsers()
        {
            List<User> allUsers = new List<User>();

            try
            {
                allUsers = ctx.UserSet.ToList();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("No user found.");
            }

            return allUsers;
        }

        // Find by Id
        public User FindUserById(int id)
        {
            User userById = null;

            try
            {
                userById = ctx.UserSet.Where(u => u.Id == id).Single();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("No user found.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error excuting the query");
            }

            return userById;
        }

        // Find by Criteria
        public User FindUserByUsername(string username)
        {
            User userByUsername = null;

            try
            {
                userByUsername = ctx.UserSet.Where(u => u.Username == username).Single();
            }
            catch (InvalidOperationException)
            {   
                Console.WriteLine("Error executing query.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error: could not find user with username " + username + ".");
            }

            return userByUsername;
        }
        // ...

        // CRUD
        public User CreateUser(User createdUser)
        {
            ctx.UserSet.Add(createdUser);

            try
            {
                ctx.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Error creating user.");
                return null;
            }

            return createdUser;
        }

        public User UpdateUser(User updatedUser)
        {
            User userToUpdate = FindUserById(updatedUser.Id);

            if (userToUpdate != null)
            {
                userToUpdate = updatedUser;

                try
                {
                    ctx.SaveChanges();
                    return updatedUser;
                }
                catch
                {
                    Console.WriteLine("Error updating user.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find User to update with ID " + updatedUser.Id);
                return null;
            }
        }

        public Boolean DeleteUser(User deletedUser)
        {
            User userToDelete = FindUserById(deletedUser.Id);

            if (userToDelete != null)
            {
                ctx.UserSet.Remove(userToDelete);

                try
                {
                    ctx.SaveChanges();
                    return true;
                }
                catch
                {
                    Console.WriteLine("Error deleting user.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find User to delete with ID " + deletedUser.Id);
                return false;
            }
        }

    }
}
