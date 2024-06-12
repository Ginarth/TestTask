using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs
{
    public class PatchedSupportOperator : IConverted<SupportOperator>
    {
        [Required(ErrorMessage = $"Missing")]
        [Range(0, 2147483647, ErrorMessage = "Value is out of range from 0 to 2147483647")]
        public int Id { get; set; } = 0;

        public bool IsWorking { get; set; } = false;

        public SupportOperator Convert() => new()
        {
            Id = Id,
            IsWorking = IsWorking
        };
    }
}
