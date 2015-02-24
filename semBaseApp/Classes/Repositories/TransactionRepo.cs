using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Transactions;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class TransactionRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new TransactionFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Transactions).Include(x => x.RecipientCountry).Include(x => x.Currency).Include(x => x.Question).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(TransactionFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Transactions).Include(x => x.RecipientCountry).Include(x => x.Currency).Include(x => x.Question).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData GetPaidAndCancelled(TransactionFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQueryToPending(db.Transactions).Include(x => x.RecipientCountry).Include(x => x.Currency).Include(x => x.Question).Include(x=>x.UpdatedBy).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData GetPersonal(TransactionFilter filter, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    //var data = filter.BuildQuery(db.Transactions.Where(x=>x.CreatedById == userId)).Include(x => x.RecipientCountry).Include(x => x.Currency).Include(x=>x.Question).ToList();
                    var data = db.Transactions.Where(q => q.Status == "Pending" && q.IsActive && !q.IsDeleted && q.CreatedById == userId).Include(x => x.RecipientCountry).Include(x => x.Currency).Include(x => x.Question).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData GetGetAllCountry(TransactionFilter filter, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var agent = db.AgentBranches.Include(x=>x.Branch.City.Country).FirstOrDefault(x => x.AgentId == userId);
                    var trans = agent != null ? db.Transactions.Where(x => x.RecipientCountryId == agent.Branch.City.CountryId) : db.Transactions;
                    
                    var data = filter.BuildQuery(trans).Include(x => x.RecipientCountry).Include(x => x.Currency).Include(x => x.Question).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData Insert(Transaction entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new Transaction
                    {
                        Date = entity.Date,
                        SenderName = entity.SenderName,
                        SenderPhoneNumber = entity.SenderPhoneNumber,
                        Amount = entity.Amount,
                        CurrencyId = entity.CurrencyId,
                        //UniqueCode = entity.UniqueCode,
                        //Pin = entity.Pin,
                        Fee = entity.Fee,
                        RecipientName = entity.RecipientName,
                        RecipientPhoneNumber = entity.RecipientPhoneNumber,
                        RecipientCountryId = entity.RecipientCountryId,
                        QuestionId = entity.QuestionId,
                        Answer = entity.Answer,
                        Rate = entity.Rate,
                        Total = entity.Total,
                        Status = "Pending",
                        Notes = entity.Notes,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.Transactions.Add(newData);
                    db.SaveChanges();

                    //generate codes
                    GenerateCodes(newData, db);

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public void GenerateCodes(Transaction entity, DataContext db)
        {
            var cn = db.Countries.First(x => x.Id == entity.RecipientCountryId);
            var trans = db.Transactions.First(x => x.Id == entity.Id);
            
            /*trans.UniqueCode = cn.Name.ToUpper().Substring(0, 3) + "-" + (1000 + trans.Id);
            trans.Pin = cn.Name.ToUpper().Substring(0, 3) + "-" + (1000 + trans.Id);*/
            trans.UniqueCode = cn.Name.ToUpper().Substring(0, 3) + "-" + DateTime.Now.ToFileTime();
            trans.Pin = cn.Name.ToUpper().Substring(0, 3) + "-" + (1000 + trans.Id);

            db.SaveChanges();
        }

        public JsonData Update(Transaction entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var transaction = db.Transactions.FirstOrDefault(x => x.Id == entity.Id);

                    if (transaction != null)
                    {
                        transaction.Date = entity.Date;
                        transaction.SenderName = entity.SenderName;
                        transaction.SenderPhoneNumber = entity.SenderPhoneNumber;
                        transaction.Amount = entity.Amount;
                        transaction.CurrencyId = entity.CurrencyId;
                        transaction.RecipientName = entity.RecipientName;
                        transaction.RecipientPhoneNumber = entity.RecipientPhoneNumber;
                        transaction.RecipientCountryId = entity.RecipientCountryId;
                        transaction.QuestionId = entity.QuestionId;
                        transaction.Answer = entity.Answer;
                        transaction.Rate = entity.Rate;
                        transaction.Total = entity.Amount + entity.Fee;
                        transaction.Status = entity.Status;
                        transaction.Notes = entity.Notes;
                        transaction.Updated = DateTime.Now;
                        transaction.UpdatedById = userId;
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

        public JsonData Pay(RecievePaymentModel entity, string userId)
        {
            try
            {
                /*using (var scope = new TransactionScope())
                {*/
                    using (var db = new DataContext())
                    {
                        if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                        var transaction = db.Transactions.FirstOrDefault(x => x.Id == entity.Id);

                        if (transaction != null)
                        {
                            if (transaction.CreatedById == userId)
                            {
                                return DataHelpers.ReturnJsonData(null, false,
                                    "You cannot pay a transaction that you created.", 1);
                            }

                            transaction.Status = "Recieved";
                            transaction.Updated = DateTime.Now;
                            transaction.UpdatedById = userId;
                        }
                        CreatePayment(entity, db, userId);
                        db.SaveChanges();
                        //scope.Complete();
                        return DataHelpers.ReturnJsonData(entity, true, "Updated successfully", 1);
                    }
                //}
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public void CreatePayment(RecievePaymentModel entity, DataContext db, string userId)
        {
            var newPayment = new Payment
            {
                TransactionId = entity.Id,
                AgentId = userId,
                Date = DateTime.Now,
                AmountPaid = entity.AmountInCedis,
                FullName = entity.PRecipientName,
                PhoneNumber = entity.PPhoneNumber,
                IdTypeId = entity.IdTypeId,
                IdNumber = entity.IdNumber,
                IdExpiryDate = entity.IdExpiryDate,
                Updated = DateTime.Now,
                Created = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                CreatedById = userId,
                UpdatedById = userId
            };
            db.Payments.Add(newPayment);
        }

        public JsonData Cancel(Transaction entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var transaction = db.Transactions.FirstOrDefault(x => x.Id == entity.Id);

                    if (transaction != null)
                    {
                        if (transaction.CreatedById != userId)
                        {
                            return DataHelpers.ReturnJsonData(null, false, "You cannot cancel this transaction because you did not create it.", 1);
                        }
                        transaction.Status = "Cancelled";
                        transaction.Updated = DateTime.Now;
                        transaction.UpdatedById = userId;
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
        public JsonData Reverse(Transaction entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var transaction = db.Transactions.FirstOrDefault(x => x.Id == entity.Id);

                    if (transaction != null)
                    {
                        if (transaction.Status == "Recieved")
                        {
                            DeletePayment(transaction, db, userId);
                        }
                        transaction.Status = "Pending";
                        transaction.Updated = DateTime.Now;
                        transaction.UpdatedById = userId;
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
        public void DeletePayment(Transaction entity, DataContext db, string userId)
        {
            var payment = db.Payments.FirstOrDefault(x => x.TransactionId == entity.Id);
            if (payment != null)
            {
                payment.IsDeleted = true;
                payment.Updated = DateTime.Now;
                payment.UpdatedById = userId;
            }
        }
        public JsonData Delete(long id, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (id <0) throw new ArgumentNullException("The record was" + " no passed");

                    var transaction = db.Transactions.FirstOrDefault(x => x.Id == id);

                    if (transaction != null)
                    {
                        transaction.IsDeleted = true;
                        transaction.Updated = DateTime.Now;
                        transaction.UpdatedById = userId;
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