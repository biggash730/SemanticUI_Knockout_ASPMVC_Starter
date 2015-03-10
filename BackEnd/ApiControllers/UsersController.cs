using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BackEnd.Classes.Helpers;
using BackEnd.Classes.Repositories;
using BackEnd.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace BackEnd.ApiControllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("users/get")]
        public JsonData Get()
        {
            return new UserRepo().Get(User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Users/getall")]
        public JsonData GetUsers(int page, int size)
        {
            var filter = new UserFilter { Pager = { Page = page, Size = size } };
            return new UserRepo().GetUsers(filter);
        }

        [HttpPost]
        [Route("Users/Update")]
        public JsonData Update(UserViewModel data)
        {
            return new UserRepo().Update(data, User.Identity.GetUserId());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("Users/Deactivate")]
        [Route("users/users/deactivate")]
        public JsonData Deactivate(string id)
        {
            return new UserRepo().Deactivate(id, User.Identity.GetUserId());
        }

        [HttpPost]
        [Authorize]
        [Route("users/Change")]
        public async Task<JsonData> ChangePassword(ChangePasswordModel changePass)
        {
            try
            {
                var db = new DataContext();
                var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
                userMan.UserValidator = new UserValidator<MyUser>(userMan)
                {
                    AllowOnlyAlphanumericUserNames =
                        false
                };

                var user = await userMan.FindByIdAsync(User.Identity.GetUserId());
                if (user == null) throw new Exception("please check your old password");

                var newPassword = changePass.NewPassword;
                var result = await userMan.RemovePasswordAsync(user.Id);
                if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors));
                var result2 = await userMan.AddPasswordAsync(user.Id, newPassword);
                if (!result2.Succeeded) throw new Exception(string.Join(", ", result2.Errors));
                return DataHelpers.ReturnJsonData(null, true, "Password changed successful");
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        // GET security/signin
        [Route("users/login")]
        [Route("home/users/login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonData> Login(LoginModel model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));

                    //get the user by the email address
                    var us = new UserRepo().GetUserByName(model.UserName, db);
                    if (us == null) throw new Exception("Please check the login details");

                    var user = await userMan.FindAsync(us.UserName, model.Password);
                    if (user == null) throw new Exception("Please check the login details");

                    if (!user.IsActive) throw new Exception("You can not login because your user is not active");

                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    var identity = await userMan.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, identity);

                    var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                    var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

                    return DataHelpers.ReturnJsonData(user, true, token);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        [Authorize(Roles = "Administrator")]
        [Route("users/register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonData> Register(UserViewModel model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
                    userMan.UserValidator = new UserValidator<MyUser>(userMan)
                    {
                        AllowOnlyAlphanumericUserNames =
                            false
                    };

                    var user = new MyUser
                    {
                        UserName = model.UserName.Trim(),
                        FullName = model.FullName,
                        IsActive = model.IsActive,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        DateOfBirth = model.DateOfBirth,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    var result = await userMan.CreateAsync(user, model.Password.Trim());

                    if (!result.Succeeded)
                    {
                        var msg = string.Join(", ", result.Errors);
                        msg = msg.Replace("Name", "User Name");
                        msg = msg.Replace("is already taken.", "is aready in use.");
                        throw new Exception(msg);
                    }

                    userMan.AddToRole(user.Id, model.Roles);

                    db.SaveChanges();
                    return DataHelpers.ReturnJsonData(user.Id, true, "Registration was successful. Please Verify your account");
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        [HttpPost]
        [Route("users/Reset")]
        public async Task<JsonData> Reset(UserViewModel model)
        {
            try
            {
                var db = new DataContext();
                var userMan = new UserManager<MyUser>(new UserStore<MyUser>(db));
                userMan.UserValidator = new UserValidator<MyUser>(userMan)
                {
                    AllowOnlyAlphanumericUserNames =
                        false
                };
                var user = await userMan.FindByEmailAsync(model.Email);
                if (user == null) throw new Exception("please check the email address");
                //todo: generate a unique password and email it to the user
                var newPassword = user.FullName.Substring(2, 3) + user.PasswordHash.Substring(0, 5);
                var result = await userMan.RemovePasswordAsync(user.Id);
                if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors));
                var result2 = await userMan.AddPasswordAsync(user.Id, newPassword);
                if (!result2.Succeeded) throw new Exception(string.Join(", ", result2.Errors));
                //todo: Email the new password to the user
                return DataHelpers.ReturnJsonData(null, true, "A new password has been emailed to your email address");
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        #region Role Controller
        [HttpPost]
        [Route("Roles/getall")]
        public JsonData Get(RoleFilter filter)
        {
            return new RoleRepo().Get(filter);
        }

        [HttpPost]
        [Route("Roles/get")]
        public JsonData GetOneRole(string id)
        {
            return new RoleRepo().Get(id);
        }

        [HttpPost]
        [Route("Roles/insert")]
        public JsonData Insert(IdentityRole data)
        {
            return new RoleRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Roles/update")]
        public JsonData Update(IdentityRole data)
        {
            return new RoleRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Roles/delete")]
        public JsonData DeleteRole(string id)
        {
            return new RoleRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion

    }
}
