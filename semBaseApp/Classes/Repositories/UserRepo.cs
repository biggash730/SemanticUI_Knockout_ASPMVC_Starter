using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using vls.Classes.Helpers;
using vls.Models;
using WebGrease.Css.Extensions;

namespace vls.Classes.Repositories
{
    public class UserRepo
    {
        public JsonData Get(string userId)
        {
            try
            {
                var filter = new UserFilter { UserId = userId };
                using (var db = new DataContext())
                {
                    var roles = db.Roles.ToDictionary(x => x.Id);

                    var data = filter.BuildQuery(db.Users).Include(x => x.Roles).First();

                    if (data == null) return DataHelpers.ReturnJsonData(null, false, "No Data Found", 0);
                    var activeUser = new UserViewModel
                    {
                        UserName = data.UserName,
                        FullName = data.FullName,
                        Created = data.Created,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        DateOfBirth = data.DateOfBirth,
                        Id = data.Id,
                        IsActive = data.IsActive,
                        Updated = data.Updated,
                        Roles = roles.First(x => x.Key == data.Roles.First().RoleId).Value.Name
                    };
                    return DataHelpers.ReturnJsonData(activeUser, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData GetUsers(UserFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var roles = db.Roles.ToDictionary(x => x.Id);
                    var data = filter.BuildQuery(db.Users).Include(x => x.Roles).ToList().OrderBy(x => x.Updated);
                    if (!data.Any()) return DataHelpers.ReturnJsonData(null, false, "No Data Found", 0);
                    var users = data.Select(myUser => new UserViewModel
                    {
                        UserName = myUser.UserName,
                        FullName = myUser.FullName,
                        Created = myUser.Created,
                        Email = myUser.Email,
                        PhoneNumber = myUser.PhoneNumber,
                        DateOfBirth = myUser.DateOfBirth,
                        Id = myUser.Id,
                        IsActive = myUser.IsActive,
                        Updated = myUser.Updated,
                        Roles = roles.First(x => x.Key == myUser.Roles.First().RoleId).Value.Name
                    }).Where(x => !x.Roles.Contains("Administrator")).ToList();

                    return users.Any() ? DataHelpers.ReturnJsonData(users, true, "Loaded successfully", users.Count()) : DataHelpers.ReturnJsonData(users, false, "No Data Found", 0);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData Update(UserViewModel newRecord, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (newRecord == null) throw new ArgumentNullException("The data" + " record is null");

                    var oRecord = db.Users.FirstOrDefault(p => p.Id == newRecord.Id);
                    if (oRecord == null) throw new Exception("User was not found");
                    oRecord.FullName = newRecord.FullName;
                    oRecord.PhoneNumber = newRecord.PhoneNumber;
                    oRecord.Email = newRecord.Email;
                    oRecord.DateOfBirth = newRecord.DateOfBirth;
                    oRecord.Updated = DateTime.Now;

                    var roles = db.Roles.Select(x => x.Name).ToArray();
                    var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
                    foreach (var role in roles)
                    {
                        userMan.RemoveFromRole(oRecord.Id, role);
                    }
                    userMan.AddToRole(oRecord.Id, newRecord.Roles);

                    db.SaveChanges();
                    return DataHelpers.ReturnJsonData(null, true, "Updated successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData Deactivate(string id, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var oRecord = db.Users.FirstOrDefault(p => p.Id == id);
                    if (oRecord == null) throw new Exception("User was not found");
                    oRecord.IsActive = !oRecord.IsActive;
                    oRecord.Updated = DateTime.Now;
                    db.SaveChanges();
                    return DataHelpers.ReturnJsonData(null, true, "Updated successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public MyUser GetUser(string userId)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));

            var currentUser = userMan.FindById(userId);
            return currentUser;
        }

        public async Task<IdentityResult> ChangePassword(MyUser theUser, ChangePasswordModel model)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
            userMan.UserValidator = new UserValidator<MyUser>(userMan)
            {
                AllowOnlyAlphanumericUserNames =
                    false
            };
            var result = await userMan.ChangePasswordAsync(theUser.Id, model.OldPassword, model.NewPassword);
            return result;
        }

        public async Task<List<string>> GetRoles(string userId)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
            var roles = await userMan.GetRolesAsync(userId);

            return roles.ToList();
        }

        public MyUser GetUserByEmail(string email, DataContext db)
        {
            return db.Users.FirstOrDefault(p => p.Email == email);
        }

        public MyUser GetUserByName(string username, DataContext db)
        {
            return db.Users.FirstOrDefault(p => p.UserName == username);
        }
    }
}