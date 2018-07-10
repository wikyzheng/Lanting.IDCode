using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lanting.IDCode.Entity
{
    [Table("identity_code")]
    public class IdentityCode : Abp.Domain.Entities.Entity<long>
    {
        public string Code { get; set; }
        public int ProductId { get; set; }
        public string AntiFakeCode { get; set; }
        public bool IsActived { get; set; }
        public DateTime Created { get; set; }
        public int TaskId { get; set; }
    }
}
