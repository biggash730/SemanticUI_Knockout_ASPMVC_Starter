using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class RoleRepo
    {
        public JsonData Get(string id)
        {
            try
            {
                var filter = new RoleFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Roles).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(RoleFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Roles).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(IdentityRole entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                    //Create Roles if they do not exist
                    if (!roleManager.RoleExists(entity.Name))
                    {
                        roleManager.Create(new IdentityRole(entity.Name));
                    }
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(entity, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(IdentityRole entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                    //Update Role Roles if they do not exist
                    var role = db.Roles.FirstOrDefault(x => x.Id == entity.Id);
                    if (role != null)
                    {
                        role.Name = entity.Name;
                        //roleManager.Update(role);
                    }
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(entity, true, "Updated successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Delete(string id, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("The record was" + " no passed");

                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                    //Update Role Roles if they do not exist
                    var role = db.Roles.FirstOrDefault(x => x.Id == id);
                    roleManager.Delete(role);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(id, true, "Deleted successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
    }
}