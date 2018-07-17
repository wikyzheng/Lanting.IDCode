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
        [Required]
        public int UserId { get; set; }

        public string Remark { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public bool IsSuccess { get; set; }

        public string FailReason { get; set; }

        public DateTime? Completed { get; set; }

        [Required]
        public int ProductId { get; set; }

        public ProductInfoDto Product { get; set; }

        [Required]
        public int GenerateCount { get; set; }

        [Required]
        public int TaskStatu { get; set; }

        public string DataFilePath { get; set; }

        [Required]
        public bool IsAntiFake { get; set; }

        public int? AFCodeLength { get; set; }

        [Required]
        public int AntiFackCodeType { get; set; }

        [Required]
        public Int64 StartOne { get; set; }

        [Required]
        public Int64 EndOne { get; set; }
    }
}


