﻿using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class UserDL
    {
        public UserDL()
        {

        }

        public static bool Save(User user)
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UserExists(User user)
        {
            try
            {
                var existingUser = new User();
                using (var context = new PersoDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.Username.Equals(user.Username))
                                    .FirstOrDefault();
                }

                if (existingUser == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User RetrieveUserByUsername(string username)
        {
            try
            {
                var existingUser = new User();
                using (var context = new PersoDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.Username.Equals(username))
                                    .Include(x => x.Branch)
                                    .FirstOrDefault();
                }

                return existingUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User RetrieveUser()
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var existingUser = context.Users
                                    .Include("Branch")
                                    .Include("Role.RoleFunctions.Function")
                                    .OrderByDescending(u => u.ID)
                                    .Take(1);

                    return existingUser.Any() ? existingUser.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ChangePassword(string username, string newHashedPassword)
        {
            try
            {
                User existingUser = new User();
                using (var context = new PersoDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.Username == username)
                                    .FirstOrDefault();
                }

                if (existingUser != null)
                {
                    existingUser.HashedPassword = newHashedPassword;
                    existingUser.FirstTime = false;
                    using (var context = new PersoDBEntities())
                    {
                        context.Entry(existingUser).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<User> RetrieveUsers()
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var users = context.Users
                                        .Include("Branch")
                                        .Include("Role.RoleFunctions.Function")
                                        .ToList();

                    return users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User AuthenticateUser(string username, string hashedPassword)
        {
            try
            {
                using (var context = new PersoDBEntities())
                {
                    var users = context.Users
                                        .Include("Branch")
                                        .Include("Role.RoleFunctions.Function")
                                        .Where(f => f.Username == username && f.HashedPassword == hashedPassword);

                    return users.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(User user)
        {
            try
            {
                User existingUser = new User();
                using (var context = new PersoDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.ID == user.ID)
                                    .FirstOrDefault();
                }

                if (existingUser != null)
                {
                    existingUser.Email = user.Email;
                    existingUser.Gender = user.Gender;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Lastname = user.Lastname;
                    existingUser.Othernames = user.Othernames;                    
                    existingUser.UserRole = user.UserRole;
                    existingUser.UserBranch = user.UserBranch;

                    using (var context = new PersoDBEntities())
                    {
                        context.Entry(existingUser).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
