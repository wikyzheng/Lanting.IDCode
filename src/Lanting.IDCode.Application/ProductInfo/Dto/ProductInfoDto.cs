using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    /// <summary>
    /// ProductInfo DTO
    /// </summary>
    [AutoMapFrom(typeof(ProductInfo))]
    public class ProductInfoDto : EntityDto
    {
        [Required]
        public int UserId { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string Remark { get; set; }
    }
}


