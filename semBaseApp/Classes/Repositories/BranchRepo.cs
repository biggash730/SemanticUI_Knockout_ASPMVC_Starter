using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class BranchRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new BranchFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Branches).Include(x => x.City.Country).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(BranchFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Branches).Include(x => x.City.Country).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(Branch entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new Branch
                    {
                        CityId = entity.CityId,
                        Name = entity.Name,
                        Description = entity.Description,
                        Telephone = entity.Telephone,
                        Email = entity.Email,
                        PostalAddress = entity.PostalAddress,
                        ResidentialAddress = entity.ResidentialAddress,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.Branches.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(Branch entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var branch = db.Branches.FirstOrDefault(x => x.Id == entity.Id);

                    if (branch != null)
                    {
                        branch.CityId = entity.CityId;
                        branch.Name = entity.Name;
                        branch.Description = entity.Description;
                        branch.Telephone = entity.Telephone;
                        branch.Email = entity.Email;
                        branch.PostalAddress = entity.PostalAddress;
                        branch.ResidentialAddress = entity.ResidentialAddress;
                        branch.Updated = DateTime.Now;
                        branch.UpdatedById = userId;
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
        public JsonData Delete(long id, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (id < 0) throw new ArgumentNullException("The record was" + " no passed");

                    var branch = db.Cities.FirstOrDefault(x => x.Id == id);

                    if (branch != null)
                    {
                        branch.IsDeleted = true;
                        branch.Updated = DateTime.Now;
                        branch.UpdatedById = userId;
                    }

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