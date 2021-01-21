using System;
using System.ComponentModel.DataAnnotations;
using VirtualWalletApi.Data.Enums;

namespace VirtualWalletApi.Data.Entities
{
    public class Wallet : AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public EWalletCurrency Currency { set; get; } = EWalletCurrency.NAIRA;
        public string AccountNumber { set; get; }
        public Guid CustomerId { set; get; }
        public Customer Owner { set; get; }
        public decimal Balance { set; get; }
        public EWalletStatus Status { set; get; } = EWalletStatus.ACTIVE;
        public string Label { set; get; }
        public bool IsDeleted { set; get; } = false;
        public DateTime? DeletedAt { set; get; }

    }
}
