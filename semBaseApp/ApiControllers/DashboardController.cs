using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vls.Classes.Repositories;
using vls.Models;

namespace vls.ApiControllers
{
    public class DashboardController : ApiController
    {
        [HttpPost]
        [Route("dashboard/GetTransactionsForTheYear")]
        [Route("home/dashboard/GetTransactionsForTheYear")]
        public JsonData GetTransactionsForTheYear(string year)
        {
            return new DashboardRepo().GetTransactionsForTheYear(year);
        }

        [HttpPost]
        [Route("dashboard/gettransactionsformonth")]
        [Route("home/dashboard/gettransactionsformonth")]
        public JsonData GetTransactionsForTheMonth(int month)
        {
            return new DashboardRepo().GetTransactionsForTheMonth(month);
        }

        [HttpGet]
        [Route("dashboard/transactions")]
        [Route("home/dashboard/transactions")]
        public JsonData GetTransactions(string status)
        {
            return new DashboardRepo().GetTransactions(status);
        }
    }
}
