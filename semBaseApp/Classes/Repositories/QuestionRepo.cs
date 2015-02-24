using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vls.Classes.Helpers;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class QuestionRepo
    {
        public JsonData Get(long id)
        {
            try
            {
                var filter = new QuestionFilter { Id = id };
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Questions).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Get(QuestionFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.BuildQuery(db.Questions).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Insert(Question entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The new" + " record is null");

                    var newData = new Question
                    {
                        Description = entity.Description,
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedById = userId,
                        UpdatedById = userId
                    };

                    db.Questions.Add(newData);
                    db.SaveChanges();

                    return DataHelpers.ReturnJsonData(newData, true, "Saved successfully", 1);
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
        public JsonData Update(Question entity, string userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (entity == null) throw new ArgumentNullException("The record is" + " record is null");

                    var question = db.Questions.FirstOrDefault(x => x.Id == entity.Id);

                    if (question != null)
                    {
                        question.Description = entity.Description;
                        question.Updated = DateTime.Now;
                        question.UpdatedById = userId;
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

                    var question = db.Questions.FirstOrDefault(x => x.Id == id);

                    if (question != null)
                    {
                        question.IsDeleted = true;
                        question.Updated = DateTime.Now;
                        question.UpdatedById = userId;
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