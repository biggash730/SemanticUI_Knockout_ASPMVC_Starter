using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace vls.Models
{
    public class JsonData
    {
        [Required]
        public Object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
    }

    public class HasId
    {
        [Key]
        public long Id { get; set; }
    }

    public class AuditFields : HasId
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        [Required, MaxLength(64)]
        public string CreatedById { get; set; }
        public virtual MyUser CreatedBy { get; set; }
        [Required, MaxLength(64)]
        public string UpdatedById { get; set; }
        public virtual MyUser UpdatedBy { get; set; }
    }

    public class LookUp : AuditFields
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string Description { get; set; }
    }

    public class Pager
    {
        [DefaultValue(25)]
        public int Size { get; set; }
        [DefaultValue(1)]
        public int Page { get; set; }

        public int Skip
        {
            get { return (Size*Page); }
        }
    }

    public class Settings
    {
        /*public string sendGridUsername { get; set; }
        public string sendGridPassword { get; set; }
        public string senderDomain { get; set; }
        public string email { get; set; }
        public string smtpUsername { get; set; }
        public string smtpPassword { get; set; }
        public string smtp { get; set; }
        public int port { get; set; }
        public string sms { get; set; }
        public string serviceUrl { get; set; }
        public string creditCheck { get; set; }
        public string apiKey { get; set; }
        public int dashboardMessageCount { get; set; }
        public int defaultEmails { get; set; }
        public int defaultSMS { get; set; }
        public bool smtpProtocol { get; set; }
        public int batchSize { get; set; }
        public string smsRequestString { get; set; }*/
    }
    public static class AppSettings
    {
        public static Settings Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/files/settings.json")));

    }

    public class DashboardTransactions
    {
        public int[] Recieved { get; set; }
        public int[] Pending { get; set; }
    }

    public class DashboardMonthlyTransactions
    {
        public int Pending { get; set; }
        public int Recieved { get; set; }
    }
    public class DashboardIssuesStats
    {
        public int[] Resolved { get; set; }
        public int[] Pending { get; set; }
    }

    public class DashboardPayIns
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }

    public class DashboardSalesExecs
    {
        public string FullName { get; set; }
        public double Amount { get; set; }
    }

    public class RecievePaymentModel
    {
        public long Id { get; set; }
        public string PRecipientName { get; set; }
        public string PPhoneNumber { get; set; }
        public long IdTypeId { get; set; }
        public string IdNumber { get; set; }
        public DateTime IdExpiryDate { get; set; }
        public double AmountInCedis { get; set; }
    }

    public class AgentTransactionSummary
    {
        public long Id { get; set; }
        public virtual MyUser Agent { get; set; }
        public virtual Branch Branch { get; set; }
        public double TotalSentTransactions { get; set; }
        public double SentTransactionFees { get; set; }
        //public double TotalPaidTransactions { get; set; }
    }
}