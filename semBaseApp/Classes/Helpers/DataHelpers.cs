using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;
using Newtonsoft.Json;
using NLog;
using vls.Models;

namespace vls.Classes.Helpers
{
    public class DataHelpers
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static JsonData ReturnJsonData(object data = null, bool success = false, string message = "", int total = 0)
        {
            return new JsonData
            {
                Data = data,
                Success = success,
                Message = message,
                Total = total,
            };
        }

        public static string GenerateCode(int length,
            string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var charArray = charset.ToCharArray();
            var charLength = charArray.Length;
            var output = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                output.Append(charArray[rand.Next(charLength)]);
            }
            return output.ToString();
        }

        public static JsonData ExceptionProcessor(Exception exception)
        {
            //if(exception.HResult == HResult.)

            var text = "";
            var validationException = exception as DbEntityValidationException;
            if (validationException != null)
            {
                var lines = validationException.EntityValidationErrors.Select(
                    x => new
                    {
                        name = x.Entry.Entity.GetType().Name.Split('_')[0],
                        errors = x.ValidationErrors.Select(y => y.PropertyName + ":" + y.ErrorMessage)
                    })
                                               .Select(x => string.Format("{0} => {1}", x.name, string.Join(",", x.errors)));
                text = string.Join("\r\n", lines);

                Logger.Error(text);

                return ReturnJsonData(null, false, text);
            }

            //for any other exception, just get the full message
            text = GetErrorMessages(exception).Aggregate((a, b) => a + "\r\n" + b);
            Logger.Error(text);
            return ReturnJsonData(null, false, text);
        }

        private void HandleNullReferenceException(Exception ex)
        {
            
        }

        private void HandleServerException(Exception ex)
        {

        }

        private void HandleGeneralFaultsException(Exception ex)
        {

        }

        private static IEnumerable<string> GetErrorMessages(Exception exception)
        {
            if (exception.InnerException != null)
                foreach (var msg in GetErrorMessages(exception.InnerException))
                    yield return msg;
            yield return exception.Message;
        }
    }
}