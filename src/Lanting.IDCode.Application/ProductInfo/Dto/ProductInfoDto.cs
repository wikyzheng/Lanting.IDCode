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

        public int UserId { get; set; }

        [Required]
        
        public string Code { get; set; }

        [Required]
        public string FullName { get; set; }


        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string HtmlContent { get; set; }
    }
}


