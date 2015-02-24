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
    public class TransferFeeRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new TransferFeeFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.TransferFees).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(TransferFeeFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.TransferFees).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(TransferFee entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new TransferFee
                    {
                        MaximumAmount = entity.MaximumAmount,
                        MinimumAmount = entity.MinimumAmount,
                        Fee = entity.Fee,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.TransferFees.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(TransferFee entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var transferFee = db.TransferFees.FirstOrDefault(x => x.Id == entity.Id);

                    if (transferFee != null)
                    {
                        transferFee.MaximumAmount = entity.MaximumAmount;
                        transferFee.MinimumAmount = entity.MinimumAmount;
                        transferFee.Fee = entity.Fee;
                        transferFee.Updated = DateTime.Now;
                        transferFee.UpdatedById = userId;
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

                    var transferFee = db.TransferFees.FirstOrDefault(x => x.Id == id);

                    if (transferFee != null)
                    {
                        transferFee.IsDeleted = true;
                        transferFee.Updated = DateTime.Now;
                        transferFee.UpdatedById = userId;
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