using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoAdmin.Core.Models
{
    [Table("CustomerCustomerDemo")]
    public partial class CustomerCustomerDemo
    {
        [Column("CustomerID")]
        [StringLength(5)]
        public string CustomerId { get; set; }
        [Column("CustomerTypeID")]
        [StringLength(10)]
        public string CustomerTypeId { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("CustomerCustomerDemoes")]
        public Customer Customer { get; set; }
        [ForeignKey("CustomerTypeId")]
        [InverseProperty("CustomerCustomerDemoes")]
        public CustomerDemographic CustomerType { get; set; }
    }
}
