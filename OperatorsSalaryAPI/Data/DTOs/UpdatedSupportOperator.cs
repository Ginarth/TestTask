using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs
{
    public class UpdatedSupportOperator : IConverted<SupportOperator>
    {
        [Required(ErrorMessage = $"Missing")]
        [Range(0, 2147483647, ErrorMessage = "Value is out of range from 0 to 2147483647")]
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = $"Missing")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Length is out of range from 0 to 50")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = $"Missing")]
        public bool IsWorking { get; set; } = false;

        [Required(ErrorMessage = $"Missing")]
        [Range(0, 32767, ErrorMessage = "Value is out of range from 0 to 32767")]
        public short FirstResponseTime { get; set; } = 0;

        [Required(ErrorMessage = $"Missing")]
        [Range(0, 32767, ErrorMessage = "Value is out of range from 0 to 32767")]
        public short ResponseTime { get; set; } = 0;

        [Required(ErrorMessage = $"Missing")]
        [Range(0, 32767, ErrorMessage = "Value is out of range from 0 to 32767")]
        public short CompetencyAssessment { get; set; } = 0;

        [Required(ErrorMessage = $"Missing")]
        [Range(0, 32767, ErrorMessage = "Value is out of range from 0 to 32767")]
        public short PolitenessAssessment { get; set; } = 0;

        public SupportOperator Convert() => new()
        {
            Id = Id,
            Name = Name,
            IsWorking = IsWorking,
            FirstResponseTime = FirstResponseTime,
            ResponseTime = ResponseTime,
            CompetencyAssessment = CompetencyAssessment,
            PolitenessAssessment = PolitenessAssessment
        };
    }
}
