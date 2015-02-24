using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using vls.Classes.Repositories;
using vls.Models;

namespace vls.ApiControllers
{
    public class ReportsController : ApiController
    {
        [HttpPost]
        [Route("fees/report")]
        public JsonData Fees()
        {
            return new ReportRepo().Fees();
        }

        [HttpPost]
        [Route("alltransactions/report")]
        public JsonData AllTransactions(FormDataCollection formData)
        {
            var datefrom = DateTime.Parse(formData["DateFrom"]);
            var dateto = DateTime.Parse(formData["DateTo"]);
            var amountfrom = long.Parse(formData["AmountFrom"] ?? "0");
            var amountto = long.Parse(formData["AmountTo"] ?? "0");
            var name = formData["Name"];
            var uniquecode = formData["UniqueCode"];
            var status = formData["Status"];
            var sendercountryid = long.Parse(formData["SenderCountryId"] ?? "0");
            var recipientcountryid = long.Parse(formData["RecipientCountryId"] ?? "0");
            var currencyid = long.Parse(formData["CurrencyId"] ?? "0");

            var filter = new TransactionFilter { Pager = { Page = 0, Size = 0 }, DateFrom = datefrom, DateTo = dateto, AmountFrom = amountfrom, AmountTo = amountto, Name = name, UniqueCode = uniquecode, Status = status, SenderCountryId = sendercountryid, RecipientCountryId = recipientcountryid, CurrencyId = currencyid };
            return new ReportRepo().AllTransactions(filter);
        }

        [HttpPost]
        [Route("cancelledtransactions/report")]
        public JsonData CancelledTransactions(FormDataCollection formData)
        {
            var datefrom = DateTime.Parse(formData["DateFrom"]);
            var dateto = DateTime.Parse(formData["DateTo"]);
            var amountfrom = long.Parse(formData["AmountFrom"] ?? "0");
            var amountto = long.Parse(formData["AmountTo"] ?? "0");
            var name = formData["Name"];
            var uniquecode = formData["UniqueCode"];
            var sendercountryid = long.Parse(formData["SenderCountryId"] ?? "0");
            var recipientcountryid = long.Parse(formData["RecipientCountryId"] ?? "0");
            var currencyid = long.Parse(formData["CurrencyId"] ?? "0");

            var filter = new TransactionFilter { Pager = { Page = 0, Size = 0 }, DateFrom = datefrom, DateTo = dateto, AmountFrom = amountfrom, AmountTo = amountto, Name = name, UniqueCode = uniquecode, Status = "Cancelled", SenderCountryId = sendercountryid, RecipientCountryId = recipientcountryid, CurrencyId = currencyid };
            return new ReportRepo().CancelledTransactions(filter);
        }

        [HttpPost]
        [Route("branches/report")]
        public JsonData Branches(FormDataCollection formData)
        {
            var name = formData["Name"];
            var email = formData["Email"];
            var countryid = long.Parse(formData["CountryId"] ?? "0");
            var cityid = long.Parse(formData["CityId"] ?? "0");

            var filter = new BranchFilter { Pager = { Page = 0, Size = 0 }, Name = name, Email = email, CityId = cityid, CountryId = countryid};
            return new ReportRepo().Branches(filter);
        }

        [HttpPost]
        [Route("AgentsSummaries/report")]
        public JsonData AgentsSummary(FormDataCollection formData)
        {
            var datefrom = DateTime.Parse(formData["DateFrom"]);
            var dateto = DateTime.Parse(formData["DateTo"]);
            var agentid = formData["AgentId"];
            var branchid = long.Parse(formData["BranchId"] ?? "0");

            var filter = new AgentSummaryFilter { Pager = { Page = 0, Size = 0 }, DateFrom = datefrom, DateTo = dateto, AgentId = agentid, BranchId = branchid};
            return new ReportRepo().AgentsSummary(filter);
        }
    }
}
