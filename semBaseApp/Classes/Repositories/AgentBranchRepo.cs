using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class AgentBranchRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new AgentBranchFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.AgentBranches).Include(x => x.Agent).Include(x => x.Branch).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(AgentBranchFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.AgentBranches).Include(x=>x.Agent).Include(x=>x.Branch).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(AgentBranch entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new AgentBranch
                    {
                        BranchId = entity.BranchId,
                        AgentId = entity.AgentId,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.AgentBranches.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(AgentBranch entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var agentBranch = db.AgentBranches.FirstOrDefault(x => x.Id == entity.Id);

                    if (agentBranch != null)
                    {
                        agentBranch.AgentId = entity.AgentId;
                        agentBranch.BranchId = entity.BranchId;
                        agentBranch.Updated = DateTime.Now;
                        agentBranch.UpdatedById = userId;
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
                    if (id < 0) throw new ArgumentNullException("The record was" + " no passed");

                    var agentBranch = db.Cities.FirstOrDefault(x => x.Id == id);

                    if (agentBranch != null)
                    {
                        agentBranch.IsDeleted = true;
                        agentBranch.Updated = DateTime.Now;
                        agentBranch.UpdatedById = userId;
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