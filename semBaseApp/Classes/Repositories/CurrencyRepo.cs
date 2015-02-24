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
    public class CurrencyRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new CurrencyFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Currencies).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(CurrencyFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data =
                        filter.BuildQuery(db.Currencies).ToList()
                            .OrderByDescending(x => x.Name);
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(Currency entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");


                    var newData = new Currency
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Symbol = entity.Symbol,
                        Rate = entity.Rate,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.Currencies.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(Currency entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var currency = db.Currencies.FirstOrDefault(x => x.Id == entity.Id);

                    if (currency != null)
                    {
                        currency.Name = entity.Name;
                        currency.Description = entity.Description;
                        currency.Rate = entity.Rate;
                        currency.Symbol = entity.Symbol;
                        currency.Updated = DateTime.Now;
                        currency.UpdatedById = userId;
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

                    var currency = db.Currencies.FirstOrDefault(x => x.Id == id);

                    if (currency != null)
                    {
                        currency.IsDeleted = true;
                        currency.Updated = DateTime.Now;
                        currency.UpdatedById = userId;
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