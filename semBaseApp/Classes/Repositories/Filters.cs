using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using vls.Models;

namespace vls.Classes.Repositories
{
    public class Pager
    {
        [DefaultValue(1)]
        public int Page { get; set; }
        [DefaultValue(25)]
        public int Size { get; set; }

        public int Skip()
        {
            return Page * Size;
        }
    }

    public class UserFilter
    {
        public Pager Pager = new Pager();
        public string UserId;
        public string Email;
        //public long CountryId;

        public IQueryable<MyUser> BuildQuery(IQueryable<MyUser> query)
        {

            if (!string.IsNullOrWhiteSpace(UserId)) query = query.Where(q => q.Id == UserId);
            if (!string.IsNullOrWhiteSpace(Email)) query = query.Where(q => q.Email.Contains(Email));
            //if (CountryId > 0) query = query.Where(q => q.CountryId == CountryId);
            //query = query.Where(q => q.IsActive);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class CountryFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Name;
        public string ShortCode;
        public long CurrencyId;

        public IQueryable<Country> BuildQuery(IQueryable<Country> query)
        {

            if (Id>0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            if (!string.IsNullOrWhiteSpace(ShortCode)) query = query.Where(q => q.ShortCode.Contains(ShortCode));
            if (CurrencyId > 0) query = query.Where(q => q.CurrencyId == CurrencyId);
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class CityFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Name;
        public long CountryId;

        public IQueryable<City> BuildQuery(IQueryable<City> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            if (CountryId > 0) query = query.Where(q => q.CountryId == CountryId);
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class BranchFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Name;
        public string Email;
        public long CountryId;
        public long CityId;

        public IQueryable<Branch> BuildQuery(IQueryable<Branch> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            if (CityId > 0) query = query.Where(q => q.CityId == CityId);
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
        public IQueryable<Branch> ReportBuildQuery(IQueryable<Branch> query)
        {
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            if (!string.IsNullOrWhiteSpace(Email)) query = query.Where(q => q.Email.Contains(Email));
            if (CountryId > 0) query = query.Where(q => q.City.CountryId == CountryId);
            if (CityId > 0) query = query.Where(q => q.CityId == CityId);

            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    /*public class AgentBranchFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string AgentId;
        public long BranchId;

        public IQueryable<AgentBranch> BuildQuery(IQueryable<AgentBranch> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(AgentId)) query = query.Where(q => q.AgentId.Contains(AgentId));
            if (BranchId > 0) query = query.Where(q => q.BranchId == BranchId);
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }*/

    public class CurrencyFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Name;

        public IQueryable<Currency> BuildQuery(IQueryable<Currency> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class TransferFeeFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public double MaximumAmountFrom;
        public double MaximumAmountTo;
        public double MinimumAmountFrom;
        public double MinimumAmountTo;

        public IQueryable<TransferFee> BuildQuery(IQueryable<TransferFee> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (MaximumAmountFrom > 0) query = query.Where(x => x.MaximumAmount >= MaximumAmountFrom);
            if (MaximumAmountTo > 0) query = query.Where(x => x.MaximumAmount <= MaximumAmountTo);
            if (MinimumAmountFrom > 0) query = query.Where(x => x.MinimumAmount >= MinimumAmountFrom);
            if (MinimumAmountTo > 0) query = query.Where(x => x.MinimumAmount <= MinimumAmountTo);
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Created).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class TransactionFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Name;
        public string Status;
        public string UniqueCode;
        public string Pin;
        public long SenderCountryId;
        public long RecipientCountryId;
        public long CurrencyId;
        public DateTime DateFrom;
        public DateTime DateTo;
        public double AmountFrom;
        public double AmountTo;
        public string AgentId;

        public IQueryable<Transaction> BuildQueryToPending(IQueryable<Transaction> query)
        {
            query = query.Where(q =>q.Status!="Pending" && q.IsActive && !q.IsDeleted);
            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.RecipientName.Contains(Name) || q.SenderName.Contains(Name));
            if (!string.IsNullOrWhiteSpace(Status)) query = query.Where(q => q.Status == Status);
            if (!string.IsNullOrWhiteSpace(UniqueCode)) query = query.Where(q => q.UniqueCode.Contains(UniqueCode));
            if (!string.IsNullOrWhiteSpace(Pin)) query = query.Where(q => q.Pin == Pin);
            if (!string.IsNullOrWhiteSpace(AgentId)) query = query.Where(q => q.UpdatedById.Contains(AgentId));
            if (RecipientCountryId > 0) query = query.Where(q => q.RecipientCountryId == RecipientCountryId);
            if (CurrencyId > 0) query = query.Where(q => q.CurrencyId == CurrencyId);
            if (AmountFrom > 0) query = query.Where(x => x.Amount >= AmountFrom);
            if (AmountTo > 0) query = query.Where(x => x.Amount <= AmountTo);
            if (DateFrom.Year > 1) query = query.Where(x => x.Date >= DateFrom);
            if (DateTo.Year > 1) query = query.Where(x => x.Date <= DateTo);

            if (Pager.Size > 0) query = query.OrderByDescending(x => x.Created).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }

        public IQueryable<Transaction> BuildQuery(IQueryable<Transaction> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.RecipientName.Contains(Name) || q.SenderName.Contains(Name));
            if (!string.IsNullOrWhiteSpace(Status)) query = query.Where(q => q.Status == Status);
            //if (!string.IsNullOrWhiteSpace(UniqueCode)) query = query.Where(q => q.UniqueCode == UniqueCode);
            if (!string.IsNullOrWhiteSpace(Pin)) query = query.Where(q => q.Pin == Pin);
            if (RecipientCountryId > 0) query = query.Where(q => q.RecipientCountryId == RecipientCountryId);
            if (CurrencyId > 0) query = query.Where(q => q.CurrencyId == CurrencyId);
            if (AmountFrom > 0) query = query.Where(x => x.Amount >= AmountFrom);
            if (AmountTo > 0) query = query.Where(x => x.Amount <= AmountTo);
            if (DateFrom.Year > 1) query = query.Where(x => x.Date >= DateFrom);
            if (DateTo.Year > 1) query = query.Where(x => x.Date <= DateTo);
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderByDescending(x => x.Created).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }

        public IQueryable<Transaction> ReportBuildQuery(IQueryable<Transaction> query, DataContext db)
        {
            
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.RecipientName.Contains(Name) || q.SenderName.Contains(Name));
            if (!string.IsNullOrWhiteSpace(Status)) query = query.Where(q => q.Status == Status);
            if (!string.IsNullOrWhiteSpace(UniqueCode)) query = query.Where(q => q.UniqueCode.Contains(UniqueCode));
            if (RecipientCountryId > 0) query = query.Where(q => q.RecipientCountryId == RecipientCountryId);
            if (CurrencyId > 0) query = query.Where(q => q.CurrencyId == CurrencyId);
            if (AmountFrom > 0) query = query.Where(x => x.Amount >= AmountFrom);
            if (AmountTo > 0) query = query.Where(x => x.Amount <= AmountTo);
            if (DateFrom.Year > 1) query = query.Where(x => x.Date >= DateFrom);
            if (DateTo.Year > 1) query = query.Where(x => x.Date <= DateTo);

            if (SenderCountryId > 0)
            {
                var agents =
                    db.AgentBranches.Where(x => x.Branch.City.CountryId == SenderCountryId)
                        .Select(x => x.AgentId)
                        .ToList();
                query = query.Where(q => agents.Contains(q.CreatedBy.Id));
            }

            if (Pager.Size > 0) query = query.OrderByDescending(x => x.Created).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class RoleFilter
    {
        public Pager Pager = new Pager();
        public string Id;
        public string Name;

        public IQueryable<IdentityRole> BuildQuery(IQueryable<IdentityRole> query)
        {

            if (!string.IsNullOrWhiteSpace(Id)) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            if (Pager.Size > 0) query = query.OrderBy(x => x.Id).Skip(Pager.Skip()).Take(Pager.Size);

            return query;
        }
    }

    public class QuestionFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Description;

        public IQueryable<Question> BuildQuery(IQueryable<Question> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Description)) query = query.Where(q => q.Description.Contains(Description));
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class IdTypeFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string Name;

        public IQueryable<IdType> BuildQuery(IQueryable<IdType> query)
        {

            if (Id > 0) query = query.Where(q => q.Id == Id);
            if (!string.IsNullOrWhiteSpace(Name)) query = query.Where(q => q.Name.Contains(Name));
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (Pager.Size > 0) query = query.OrderBy(x => x.Updated).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }

    public class AgentSummaryFilter
    {
        public Pager Pager = new Pager();
        public long Id;
        public string AgentId;
        public long BranchId;
        public DateTime DateFrom;
        public DateTime DateTo;

        public IQueryable<Transaction> ReportBuildQuery(IQueryable<Transaction> query, DataContext db)
        {
            query = query.Where(q => q.IsActive && !q.IsDeleted);
            if (!string.IsNullOrWhiteSpace(AgentId)) query = query.Where(q => q.CreatedById.Contains(AgentId));
            if (DateFrom.Year > 1) query = query.Where(x => x.Date >= DateFrom);
            if (DateTo.Year > 1) query = query.Where(x => x.Date <= DateTo);

            if (BranchId > 0)
            {
                var agents =
                    db.AgentBranches.Where(x => x.BranchId == BranchId).Select(x=>x.AgentId).ToList();
                query = query.Where(q => agents.Contains(q.CreatedBy.Id));
            }

            if (Pager.Size > 0) query = query.OrderByDescending(x => x.Date).Skip(Pager.Skip()).Take(Pager.Size);
            return query;
        }
    }
}