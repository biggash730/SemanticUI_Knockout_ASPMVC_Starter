using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.Annotations;

namespace vls.Models
{
    public class Country : LookUp
    {
        [Required, MaxLength(128)]
        public string ShortCode { get; set; }
        [Required]
        public long CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
    }
    public class City : LookUp
    {
        [Required]
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
    public class IdType : LookUp
    {
    }

    public class Branch : LookUp
    {
        [MaxLength(64)]
        public string Telephone { get; set; }
        [MaxLength(512)]
        public string PostalAddress { get; set; }
        [MaxLength(512)]
        public string ResidentialAddress { get; set; }
        [MaxLength(64)]
        public string Email { get; set; }
        [Required]
        public long CityId { get; set; }
        public virtual City City { get; set; }
    }
    public class Question : AuditFields
    {
        [MaxLength(512)]
        public string Description { get; set; }
    }
    public class AgentBranch
    {
        [Key]
        public long Id { get; set; }
        
        [Required, MaxLength(64)]
        public string AgentId { get; set; }
        public virtual MyUser Agent { get; set; }
        [Required]
        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
    public class Currency : LookUp
    {
        [Required, MaxLength(1)]
        public string Symbol { get; set; }
        [Required, DefaultValue(1)]
        public double Rate { get; set; }
    }

    public class TransferFee : AuditFields
    {
        /*[Required]
        public long CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }*/
        [Required]
        public double MaximumAmount { get; set; }
        [Required]
        public double MinimumAmount { get; set; }
        [Required]
        public double Fee { get; set; }
    }

    public class Transaction : AuditFields
    {
        [Required]
        public DateTime Date { get; set; }
        [Required, MaxLength(128)]
        public string SenderName { get; set; }
        [Required, MaxLength(128)]
        public string SenderPhoneNumber { get; set; }
        [Required]
        public double Amount { get; set; }
        public double Fee { get; set; }
        [Required]
        public long CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        [MaxLength(64)]
        public string UniqueCode { get; set; }
        [MaxLength(64)]
        public string Pin { get; set; }
        [Required, MaxLength(128)]
        public string RecipientName { get; set; }
        [Required, MaxLength(128)]
        public string RecipientPhoneNumber { get; set; }
        [Required]
        public long RecipientCountryId { get; set; }
        public virtual Country RecipientCountry { get; set; }
        [Required]
        public string Status { get; set; }
        public string Notes { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public long QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [Required, MaxLength(128)]
        public string Answer { get; set; }
    }

    public class TransactionViewModel
    {
        public long Id { get; set; }
        public string SenderName { get; set; }
        public string SenderPhoneNumber { get; set; }
        public double Amount { get; set; }
        public string UniqueCode { get; set; }

        public string Pin { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public long RecipientCountryId { get; set; }
        public virtual Country RecipientCountry { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Payment : AuditFields
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string AgentId { get; set; }
        public virtual MyUser Agent { get; set; }
        [Required]
        public long TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
        [Required]
        public long IdTypeId { get; set; }
        public virtual IdType IdType { get; set; }
        [Required, MaxLength(128)]
        public string IdNumber { get; set; }
        [Required]
        public DateTime IdExpiryDate { get; set; }
        [Required, MaxLength(128)]
        public string FullName { get; set; }
        [MaxLength(128)]
        public string PhoneNumber { get; set; }
        [Required]
        public double AmountPaid { get; set; }
    }
}