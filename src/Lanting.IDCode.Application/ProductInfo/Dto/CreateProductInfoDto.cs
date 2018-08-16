using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Lanting.IDCode.Entity;
using Abp.Application.Services.Dto;

namespace Lanting.IDCode.Application
{
    /// <summary>
    /// ProductInfo CreateDTO
    /// </summary>
    [AutoMapTo(typeof(ProductInfo))]
    public class CreateProductInfoDto : EntityDto
    {
        public int UserId { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "[页面代码]只能输入字母、数字！")]
        [MaxLength(10, ErrorMessage = "[页面代码]长度不能超10位")]
        public string Code { get; set; }

        [Required]
        public string FullName { get; set; }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string HtmlContent { get; set; }

        public string LabelContent { get; set; }
    }
}


