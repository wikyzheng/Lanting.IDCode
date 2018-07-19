using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lanting.IDCode.Entity
{
    [Table("generate_task")]
    public class GenerateTask : Abp.Domain.Entities.Entity
    {
     
        public int UserId { get; set; }
        public string Remark { get; set; }
        public DateTime Created { get; set; }
        public bool IsSuccess { get; set; }
        public string FailReason { get; set; }
        public int ProductId { get; set; }
        public int GenerateCount { get; set; }
        public TaskStatu TaskStatu { get; set; }
        public string DataFilePath { get; set; }
        public bool IsAntiFake { get; set; }
        [MinLength(4)]
        [MaxLength(8)]
        public int? AFCodeLength { get; set; }
        public AntiFackCodeType AntiFackCodeType { get; set; }
        public long StartOne { get; set; }
        public long EndOne { get; set; }
    }

    public enum TaskStatu
    {
        [Description("未执行")]
        Init,
        [Description("进行中")]
        Running,
        [Description("已完成")]
        Completed,
    }

    public enum AntiFackCodeType
    {
        Number,
        Letter,
        NumberAndLetter,
        Url
    }
}
