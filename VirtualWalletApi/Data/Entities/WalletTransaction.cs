using System;
using System.ComponentModel.DataAnnotations;
using VirtualWalletApi.Data.Enums;

namespace VirtualWalletApi.Data.Entities
{
    public class WalletTransaction
    {
        [Key]
        public Guid Id { set; get; }
        public string TransReferenceId { set; get; }
        public EWalletTransactionType Type { set; get; }
        public string Description { set; get; }
        public DateTime TransDate { set; get; } = DateTime.UtcNow;
        public decimal BalanceBefore { set; get; }
        public decimal BalanceAfter { set; get; }
        public decimal Amount { set; get; }
        public Customer Customer { set; get; }
        public Guid CustomerId { set; get; }
        public string AccountNumber { set; get; }
        public EWalletTransactionStatus Status { set; get; }
    }
}
