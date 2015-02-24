using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using vls.Models;

namespace vls.Classes.Helpers
{
    public class SecurityHelpers
    {
        public async Task<MyUser> GetUser(string userId)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
            
            var currentUser = await userMan.FindByIdAsync(userId);

            return currentUser;
        }

        public MyUser GetUserWithId(string userId)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
             var currentUser = userMan.FindById(userId);

            return currentUser;
        }

        public async Task<MyUser> GetUserByName(string name)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
            
            var currentUser = await userMan.FindByNameAsync(name);

            return currentUser;
        }

        public MyUser GetUserWithName(string name)
        {
            var db = new DataContext();
            var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
            
            var currentUser = userMan.FindByName(name);

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
            //var theUser = await UserManager.FindByNameAsync(userName);
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

        public MyUser GetUserByUserName(string username, DataContext db)
        {
            return db.Users.FirstOrDefault(p => p.UserName == username);
        }

        /*public void Dispose()
        {
            if (UserManager == null) return;
            try
            {
                UserManager.Dispose();
            }
            catch { }
        }*/
    }
}