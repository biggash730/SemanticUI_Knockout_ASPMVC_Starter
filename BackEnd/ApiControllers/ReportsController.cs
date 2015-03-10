using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using BackEnd.Classes.Repositories;
using BackEnd.Models;

namespace BackEnd.ApiControllers
{
    [EnableCors("*", "*", "*")]
    public class ReportsController : ApiController
    {
        [HttpPost]
        [Route("report/countries")]
        public JsonData Countries(Country filter)
        {
            return new ReportRepo().Countries(new CountryFilter{Name = filter.Name, ShortCode = filter.ShortCode});
        }
    }
}
