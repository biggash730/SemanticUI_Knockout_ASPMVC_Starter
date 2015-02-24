using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using vls.Classes.Repositories;
using vls.Models;

namespace vls.ApiControllers
{
    public class SettingsController : ApiController
    {
        #region Role Controller
        [HttpPost]
        [Route("Roles/getall")]
        public JsonData Get(RoleFilter filter)
        {
            return new RoleRepo().Get(filter);
        }

        [HttpPost]
        [Route("Roles/get")]
        public JsonData GetOneRole(string id)
        {
            return new RoleRepo().Get(id);
        }

        [HttpPost]
        [Route("Roles/insert")]
        public JsonData Insert(IdentityRole data)
        {
            return new RoleRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Roles/update")]
        public JsonData Update(IdentityRole data)
        {
            return new RoleRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Roles/delete")]
        public JsonData DeleteRole(string id)
        {
            return new RoleRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion

        #region Country Controller
        [HttpPost]
        [Route("home/countries/getall")]
        [Route("countries/getall")]
        public JsonData Get(CountryFilter filter)
        {
            return new CountryRepo().Get(filter);
        }

        [HttpPost]
        [Route("countries/get")]
        public JsonData GetOne(long id)
        {
            return new CountryRepo().Get(id);
        }

        [HttpPost]
        [Route("countries/insert")]
        public JsonData Insert(Country data)
        {
            return new CountryRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("countries/update")]
        public JsonData Update(Country data)
        {
            return new CountryRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("countries/delete")]
        public JsonData Delete(long id)
        {
            return new CountryRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion

        #region City Controller
        [HttpPost]
        [Route("home/cities/getall")]
        [Route("cities/getall")]
        public JsonData GetCity(CityFilter filter)
        {
            return new CityRepo().Get(filter);
        }

        [HttpPost]
        [Route("cities/get")]
        public JsonData GetOneCity(long id)
        {
            return new CityRepo().Get(id);
        }

        [HttpPost]
        [Route("cities/insert")]
        public JsonData Insert(City data)
        {
            return new CityRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("cities/update")]
        public JsonData Update(City data)
        {
            return new CityRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("cities/delete")]
        public JsonData DeleteCity(long id)
        {
            return new CityRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion

        #region Currency Controller
        [HttpPost]
        [Route("home/currencies/getall")]
        [Route("currencies/getall")]
        public JsonData GetCurrency(CurrencyFilter filter)
        {
            return new CurrencyRepo().Get(filter);
        }

        [HttpPost]
        [Route("currencies/get")]
        public JsonData GetOneCurrency(long id)
        {
            return new CurrencyRepo().Get(id);
        }

        [HttpPost]
        [Route("currencies/insert")]
        public JsonData Insert(Currency data)
        {
            return new CurrencyRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("currencies/update")]
        public JsonData Update(Currency data)
        {
            return new CurrencyRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("currencies/delete")]
        public JsonData DeleteCurrency(long id)
        {
            return new CurrencyRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion

        #region TransferFee Controller
        [HttpPost]
        [Route("transferfees/getall")]
        [Route("home/transferfees/getall")]
        public JsonData Get(TransferFeeFilter filter)
        {
            return new TransferFeeRepo().Get(filter);
        }

        [HttpPost]
        [Route("transferfees/get")]
        public JsonData GetOneTransferFee(long id)
        {
            return new TransferFeeRepo().Get(id);
        }

        [HttpPost]
        [Route("transferfees/insert")]
        public JsonData Insert(TransferFee data)
        {
            return new TransferFeeRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("transferfees/update")]
        public JsonData Update(TransferFee data)
        {
            return new TransferFeeRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("transferfees/delete")]
        public JsonData DeleteTransferFee(long id)
        {
            return new TransferFeeRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion

        #region Question Controller
        [HttpPost]
        [Route("Questions/getall")]
        [Route("home/Questions/getall")]
        public JsonData Get(QuestionFilter filter)
        {
            return new QuestionRepo().Get(filter);
        }

        [HttpPost]
        [Route("Questions/get")]
        public JsonData GetOneQuestion(long id)
        {
            return new QuestionRepo().Get(id);
        }

        [HttpPost]
        [Route("Questions/insert")]
        public JsonData Insert(Question data)
        {
            return new QuestionRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Questions/update")]
        public JsonData Update(Question data)
        {
            return new QuestionRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Questions/delete")]
        public JsonData DeleteQuestion(long id)
        {
            return new QuestionRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion
        #region Id Types Controller
        [HttpPost]
        [Route("home/idtypes/getall")]
        [Route("idtypes/getall")]
        public JsonData Get(IdTypeFilter filter)
        {
            return new IdTypeRepo().Get(filter);
        }

        [HttpPost]
        [Route("idtypes/get")]
        public JsonData GetIdType(long id)
        {
            return new IdTypeRepo().Get(id);
        }

        [HttpPost]
        [Route("idtypes/insert")]
        public JsonData Insert(IdType data)
        {
            return new IdTypeRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("idtypes/update")]
        public JsonData Update(IdType data)
        {
            return new IdTypeRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("idtypes/delete")]
        public JsonData DeleteIdType(long id)
        {
            return new IdTypeRepo().Delete(id, User.Identity.GetUserId());
        }
        #endregion
    }
}
