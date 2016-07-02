using PersoManagerLibrary.ModelLibrary.EntityFrameworkLib;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class UserPL
    {
        public static bool Save(User user, out string message)
        {
            try
            {
                if (UserDL.UserExists(user))
                {
                    message = string.Format("User with username: {0} exists already", user.Username);
                    return false;
                }
                else
                {
                    message = string.Empty;
                    if (UserDL.Save(user))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrieveUsers()
        {
            try
            {
                string eKey = System.Configuration.ConfigurationManager.AppSettings.Get("ekey");

                List<User> users = UserDL.RetrieveUsers();
                
                List<Object> returnedUsers = new List<Object>();
                
                foreach (User user in users)
                {
                    object userObj = new
                    {
                        ID = user.ID,
                        Lastname = user.Lastname,
                        Othernames = user.Othernames,
                        Gender = user.Gender,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Username = user.Username,
                        CreatedOn = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(user.CreatedOn)),
                        Role = new { ID = user.Role.ID, Name = user.Role.Name },
                        Branch = new { ID = user.Branch.ID, Name = user.Branch.Name }
                    };

                    returnedUsers.Add(userObj);
                }
                return returnedUsers;
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
                return UserDL.RetrieveUserByUsername(username);
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
                return UserDL.Update(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ChangePassword(string username, string password)
        {
            try
            {
                return UserDL.ChangePassword(username, password);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UserExists(string username, string password)
        {
            try
            {
                var user = new User();

                user = UserDL.AuthenticateUser(username, PasswordHash.MD5Hash(password));

                if (user == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserModel AuthenticateUser(string username, string password)
        {
            try
            {
                var userObj = new UserModel();
                var user = UserDL.AuthenticateUser(username, password);
                if (user != null)
                {
                    var userFunctions = new List<FunctionModel>();

                    foreach (RoleFunction roleFunction in user.Role.RoleFunctions)
                    {
                        var function = new FunctionModel
                        {
                            Name = roleFunction.Function.Name,
                            PageLink = roleFunction.Function.PageLink
                        };

                        userFunctions.Add(function);
                    }

                    userObj.ID = user.ID;
                    userObj.Username = user.Username;
                    userObj.Role = user.Role.Name;
                    userObj.Function = userFunctions;
                    userObj.Branch = new BranchModel { ID = user.Branch.ID, Code = user.Branch.Code, Name = user.Branch.Name };
                }
                return userObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserModel RetrieveUser()
        {
            try
            {
                var userObj = new UserModel();
                var user = UserDL.RetrieveUser();
                if (user != null)
                {
                    var userFunctions = new List<FunctionModel>();

                    foreach (RoleFunction roleFunction in user.Role.RoleFunctions)
                    {
                        var function = new FunctionModel
                        {
                            Name = roleFunction.Function.Name,
                            PageLink = roleFunction.Function.PageLink
                        };

                        userFunctions.Add(function);
                    }

                    userObj.ID = user.ID;
                    userObj.Username = user.Username;
                    userObj.Role = user.Role.Name;
                    userObj.Function = userFunctions;
                    userObj.Branch = new BranchModel { ID = user.Branch.ID, Code = user.Branch.Code, Name = user.Branch.Name };
                }
                return userObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
