using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEnd.Classes.Helpers;
using BackEnd.Models;

namespace BackEnd.Classes.Repositories
{
    public class ReportRepo
    {
        public JsonData Countries(CountryFilter filter)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = filter.ReportBuildQuery(db.Countries).OrderBy(x => x.Name).ToList();
                    return !data.Any() ? DataHelpers.ReturnJsonData(null, false, "No Data Found", 0) : DataHelpers.ReturnJsonData(data, true, "Loaded successfully", data.Count());
                }
            }
            catch (Exception e)
            {
                return DataHelpers.ExceptionProcessor(e);
            }
        }
    }
}