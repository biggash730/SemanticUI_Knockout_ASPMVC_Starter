using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class ReportRepo
    {
        public JsonData Fees()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.TransferFees.Include(x => x.CreatedBy).Include(x => x.UpdatedBy).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData AllTransactions(TransactionFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.ReportBuildQuery(db.Transactions.Where(q => q.Status != "Cancelled" && q.IsActive && !q.IsDeleted), db).Include(x => x.Currency).Include(x => x.RecipientCountry).Include(x => x.CreatedBy).Include(x => x.UpdatedBy).OrderBy(x=>x.Date).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData CancelledTransactions(TransactionFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.ReportBuildQuery(db.Transactions.Where(q => q.Status == "Cancelled" && q.IsActive && !q.IsDeleted), db).Include(x => x.Currency).Include(x => x.RecipientCountry).Include(x => x.CreatedBy).Include(x => x.UpdatedBy).OrderBy(x => x.Date).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData Branches(BranchFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.ReportBuildQuery(db.Branches).Include(x => x.City.Country.Currency).OrderBy(x => x.Name).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData AgentsSummary(AgentSummaryFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.ReportBuildQuery(db.Transactions, db).Include(x => x.Currency).ToList();
                    if (!data.Any()) return DataHelpers.ReturnJsonData(null, false, "No Data Found", 0);

                    var ag = db.AgentBranches.Include(x=>x.Agent).Include(x=>x.Branch).ToDictionary(x=>x.AgentId);
                    var rData = new List<AgentTransactionSummary>();
                    foreach (var a in ag)
                    {
                        var totalAmount = 0.0;
                        var totalFees = 0.0;
                        foreach (var tran in data)
                        {
                            if (tran.CreatedById == a.Key)
                            {
                                totalAmount = totalAmount + (tran.Amount*tran.Rate);
                                totalFees = totalFees + (tran.Fee * tran.Rate);
                            }
                        }
                        rData.Add(new AgentTransactionSummary{Agent = a.Value.Agent, Branch = a.Value.Branch, SentTransactionFees = totalFees, TotalSentTransactions = totalAmount});
                    }

                    return DataHelpers.ReturnJsonData(rData, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
    }
}