using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class DashboardRepo
    {
        public JsonData GetTransactionsForTheYear(string year)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = new DashboardTransactions
                    {
                        Recieved = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        Pending = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                    };

                    var transactions = db.Transactions.Where(x => x.Date.Year.ToString() == year && !x.IsDeleted && x.Status != "Cancelled").ToList();
                    foreach (var trans in transactions)
                    {
                        var month = trans.Date.Month;
                        if (trans.Status == "Recieved") data.Recieved[month - 1] = data.Recieved[month - 1] + 1;
                        else data.Pending[month - 1] = data.Pending[month - 1] + 1;
                    }

                    return DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData GetTransactionsForTheMonth(int month)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = new DashboardMonthlyTransactions
                    {
                        Recieved = 0,
                        Pending = 0
                    };

                    var transactions = db.Transactions.Where(x => !x.IsDeleted && x.Status != "Cancelled" && x.Date.Month == month && x.Created.Year == DateTime.Now.Year).ToList();

                    foreach (var trans in transactions)
                    {
                        if (trans.Status == "Pending") data.Pending = data.Pending + 1;
                        else data.Recieved = data.Recieved + 1;
                    }

                    return DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        public JsonData GetTransactions(string status)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var trans = db.Transactions.Where(x => x.IsActive && !x.IsDeleted && x.Status == status).Include(x => x.RecipientCountry).Include(x => x.Currency).OrderByDescending(x => x.Date).Take(5).ToList();
                    return DataHelpers.ReturnJsonData(trans, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }

        
    }
}