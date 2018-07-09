using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lanting.IDCode.Entity
{
    [Table("product_info")]
    public class ProductInfo : Abp.Domain.Entities.Entity
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
