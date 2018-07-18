using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    /// <summary>
    /// GenerateTask DTO
    /// </summary>
    [AutoMapFrom(typeof(GenerateTask))]
    public class GenerateTaskDto : EntityDto
    {
        public GenerateTaskDto()
        {
            this.Product = new ProductInfoDto();
        }
      
        public int UserId { get; set; }

        public string Remark { get; set; }

      
        public DateTime Created { get; set; }

       
        public bool IsSuccess { get; set; }

        public string FailReason { get; set; }

        public DateTime? Completed { get; set; }

        [Required]
        public int ProductId { get; set; }

        public ProductInfoDto Product { get; set; }

        [Required]
        public int GenerateCount { get; set; }

        
        public int TaskStatu { get; set; }

        public string DataFilePath { get; set; }

        
        public bool IsAntiFake { get; set; }

        [MinLength(4)]
        [MaxLength(8)]
        public int? AFCodeLength { get; set; }

       
        public int AntiFackCodeType { get; set; }

       
        public Int64 StartOne { get; set; }

       
        public Int64 EndOne { get; set; }
    }
}


