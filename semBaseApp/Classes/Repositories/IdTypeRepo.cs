using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class IdTypeRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new IdTypeFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.IdTypes).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(IdTypeFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.IdTypes).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(IdType entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new IdType
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.IdTypes.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(IdType entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var idType = db.IdTypes.FirstOrDefault(x => x.Id == entity.Id);

                    if (idType != null)
                    {
                        idType.Name = entity.Name;
                        idType.Description = entity.Description;
                        idType.Updated = DateTime.Now;
                        idType.UpdatedById = userId;
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
                    if (id <0) throw new ArgumentNullException("The record was" + " no passed");

                    var idType = db.IdTypes.FirstOrDefault(x => x.Id == id);

                    if (idType != null)
                    {
                        idType.IsDeleted = true;
                        idType.Updated = DateTime.Now;
                        idType.UpdatedById = userId;
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