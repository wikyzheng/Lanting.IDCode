using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    /// <summary>
    /// IdentityCode DTO
    /// </summary>
    [AutoMapFrom(typeof(IdentityCode))]
    public class IdentityCodeDto : EntityDto<long>
    {
        public string Code { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string AntiFakeCode { get; set; }

        [Required]
        public bool IsActived { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public int TaskId { get; set; }

        public string Remark { get; set; }
    }
}


