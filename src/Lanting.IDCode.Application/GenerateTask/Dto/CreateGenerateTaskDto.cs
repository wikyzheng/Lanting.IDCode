using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Lanting.IDCode.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lanting.IDCode.Application
{
    /// <summary>
    /// GenerateTask CreateDTO
    /// </summary>
    [AutoMapTo(typeof(GenerateTask))]
    public class CreateGenerateTaskDto :  EntityDto
    {
       
        public int UserId { get; set; }

        public string Remark { get; set; }

        
        public DateTime Created { get; set; }

       
        public bool IsSuccess { get; set; }

        public string FailReason { get; set; }

      
        public int ProductId { get; set; }

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


