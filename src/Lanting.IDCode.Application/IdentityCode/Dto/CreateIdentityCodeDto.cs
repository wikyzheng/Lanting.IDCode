using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Lanting.IDCode.Entity;
using Abp.Application.Services.Dto;

namespace Lanting.IDCode.Application
{
    /// <summary>
    /// IdentityCode CreateDTO
    /// </summary>
    [AutoMapTo(typeof(IdentityCode))]
    public class CreateIdentityCodeDto : EntityDto<long>
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


