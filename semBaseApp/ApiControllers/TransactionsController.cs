using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using vls.Classes.Repositories;
using vls.Models;

namespace vls.ApiControllers
{
    [Authorize]
    public class TransactionsController : ApiController
    {
        [HttpPost]
        [Route("home/transactions/getall")]
        public JsonData Get(int page, int size)
        {
            var filter = new TransactionFilter { Pager = { Page = page, Size = size } };
            return new TransactionRepo().Get(filter);
        }

        [HttpPost]
        [Route("home/transactions/getallcountry")]
        public JsonData GetAllCountry(int page, int size)
        {
            var filter = new TransactionFilter { Pager = { Page = page, Size = size }, Status = "Pending" };
            return new TransactionRepo().GetGetAllCountry(filter, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("home/transactions/getallpersonal")]
        public JsonData GetPersonal(int page, int size)
        {
            var filter = new TransactionFilter { Pager = { Page = page, Size = size }, Status = "Pending"};
            return new TransactionRepo().GetPersonal(filter, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("home/transactions/GetPaidAndCancelled")]
        public JsonData GetPaidAndCancelled(FormDataCollection formData)
        {
            var page = int.Parse(formData["Page"] ?? "0");
            var size = int.Parse(formData["Size"] ?? "0");
            var ai = formData["AgentId"];
            var s = formData["Status"];
            var uc = formData["UniqueCode"];
            var df = DateTime.Parse(formData["DateFrom"]);
            var dt = DateTime.Parse(formData["DateTo"]);
            var af = long.Parse(formData["AmountFrom"] ?? "0");
            var at = long.Parse(formData["AmountTo"] ?? "0");
            var filter = new TransactionFilter { Pager = { Page = page, Size = size }, DateFrom = df, DateTo = dt, AmountFrom = af, AmountTo = at, UniqueCode = uc, AgentId = ai, Status = s};
            return new TransactionRepo().GetPaidAndCancelled(filter);
        }

        [HttpPost]
        [Route("home/transactions/get")]
        public JsonData GetOne(long id)
        {
            return new TransactionRepo().Get(id);
        }
        
        [HttpPost]
        [Route("home/transactions/insert")]
        public JsonData Insert(Transaction data)
        {
            return new TransactionRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("home/transactions/update")]
        public JsonData Update(Transaction data)
        {
            return new TransactionRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("home/transactions/pay")]
        public JsonData Pay(RecievePaymentModel data)
        {
            return new TransactionRepo().Pay(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("home/transactions/cancel")]
        public JsonData Cancel(Transaction data)
        {
            return new TransactionRepo().Cancel(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("home/transactions/reverse")]
        public JsonData Reverse(Transaction data)
        {
            return new TransactionRepo().Reverse(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("transactions/delete")]
        public JsonData Delete(long id)
        {
            return new TransactionRepo().Delete(id, User.Identity.GetUserId());
        }
    }
}
