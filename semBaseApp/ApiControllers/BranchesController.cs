using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using vls.Classes.Repositories;
using vls.Models;

namespace vls.ApiControllers
{
    public class BranchesController : ApiController
    {
        [HttpPost]
        [Route("Branches/getall")]
        public JsonData Get(int page, int size)
        {
            var filter = new BranchFilter { Pager = { Page = page, Size = size } };
            return new BranchRepo().Get(filter);
        }

        [HttpPost]
        [Route("Branches/get")]
        public JsonData GetOne(long id)
        {
            return new BranchRepo().Get(id);
        }

        [HttpPost]
        [Route("Branches/insert")]
        public JsonData Insert(Branch data)
        {
            return new BranchRepo().Insert(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Branches/update")]
        public JsonData Update(Branch data)
        {
            return new BranchRepo().Update(data, User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("Branches/delete")]
        public JsonData Delete(long id)
        {
            return new BranchRepo().Delete(id, User.Identity.GetUserId());
        }
    }
}
