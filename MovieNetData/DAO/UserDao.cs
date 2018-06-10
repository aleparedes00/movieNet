using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNetData.DAO
{
    // Ref: https://www.tutorialspoint.com/entity_framework/entity_framework_dbcontext.htm
    public class UserDao
    {
        // Find All
        public List<User> FindAllUsers()
        {
            using (var ctx = new MovieNetModelContainer())
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
        }

        // Find by Id
        public User FindUserById(int id)
        {
            using (var ctx = new MovieNetModelContainer())
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
        }

        // Find by Criteria
        public User FindUserByUsername(string username)
        {
            using (var ctx = new MovieNetModelContainer())
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
        }

        public User UserValidation(String username, String password) {
            User user = null;
            using (var ctx = new MovieNetModelContainer())
                try
            {
                user = ctx.UserSet.Where(u => u.Username == username && u.Password == password).Single();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Context problems");
            }
            return user;
        }
        // ...

        // CRUD
        public User CreateUser(User createdUser)
        {
            using (var ctx = new MovieNetModelContainer())
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
        }

        public User UpdateUser(User updatedUser)
        {
            using (var ctx = new MovieNetModelContainer())
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
        }

        public Boolean DeleteUser(User deletedUser)
        {
            using (var ctx = new MovieNetModelContainer())
            {
                User userToDelete = FindUserById(deletedUser.Id);

                if (userToDelete != null)
                {
                    ctx.UserSet.Attach(userToDelete);
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
}
