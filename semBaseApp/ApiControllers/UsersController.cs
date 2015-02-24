using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using vls.Classes.Helpers;
using vls.Classes.Repositories;
using vls.Models;

namespace vls.ApiControllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [HttpPost]
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
                    if (model.Roles.Contains("Agent"))
                    {
                        CreateAgentBranch(user, model.BranchId, db);
                    }
                    

                    db.SaveChanges();
                    return DataHelpers.ReturnJsonData(user.Id, true, "Registration was successful. Please Verify your account");
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public void CreateAgentBranch(MyUser user, long branchId, DataContext db)
        {
            db.AgentBranches.Add(new AgentBranch
            {
                AgentId = user.Id,
                BranchId = branchId
            });
        }

        [HttpPost]
        [Route("Home/Agents/get")]
        [Route("Agents/get")]
        public JsonData GetAgents()
        {
            return new UserRepo().GetAgents();
        }
    }
}
