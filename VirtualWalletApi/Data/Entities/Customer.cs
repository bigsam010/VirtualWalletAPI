using System;
using System.ComponentModel.DataAnnotations;
using VirtualWalletApi.Data.Enums;

namespace VirtualWalletApi.Data.Entities
{
    public class Customer : AuditableEntity
    {
        [Key]
        public Guid Id { set; get; }
        public string Email { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Password { set; get; }
        public ECustomerStatus Status { set; get; } = ECustomerStatus.ACTIVE;
    }
}
