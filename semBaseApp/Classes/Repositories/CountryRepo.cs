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
    public class CountryRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new CountryFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Countries).Include(x => x.Currency).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(CountryFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Countries).Include(x=>x.Currency).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(Country entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new Country
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        ShortCode = entity.ShortCode,
                        CurrencyId = entity.CurrencyId,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.Countries.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(Country entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var country = db.Countries.FirstOrDefault(x => x.Id == entity.Id);

                    if (country != null)
                    {
                        country.Name = entity.Name;
                        country.Description = entity.Description;
                        country.ShortCode = entity.ShortCode;
                        country.CurrencyId = entity.CurrencyId;
                        country.Updated = DateTime.Now;
                        country.UpdatedById = userId;
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

                    var country = db.Countries.FirstOrDefault(x => x.Id == id);

                    if (country != null)
                    {
                        country.IsDeleted = true;
                        country.Updated = DateTime.Now;
                        country.UpdatedById = userId;
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