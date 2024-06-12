using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.DTOs.Attributes;
using SupportOperatorsSalaryAPI.Data.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs
{
    public class UpdatedParameter : IConverted<Parameter>
    {
        [Required(ErrorMessage = $"Missing")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Length is out of range from 0 to 50")]
        [ValidName(ErrorMessage = $"Invalid parameter name. Try these 'first_response_time', 'response_time', 'competency_assessment', 'politeness_assessment'")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = $"Missing")]
        [Range(0.00, 1.00, ErrorMessage = "Value is out of range from 0.00 to 1.00")]
        [WeightPrecision(3, 2, ErrorMessage = "Value does not follow the format 0.00")]
        public decimal Weight { get; set; } = 0.00M;

        [Required(ErrorMessage = $"Missing")]
        [Range(0, 32767, ErrorMessage = "Value is out of range from 0 to 32767")]
        public short BaseValue { get; set; } = 0;

        [Required(ErrorMessage = $"Missing")]
        [Range(0, 32767, ErrorMessage = "Value is out of range from 0 to 32767")]
        public short NormalValue { get; set; } = 0;

        public Parameter Convert() => new()
        {
            Name = Name,
            Weight = Weight,
            BaseValue = BaseValue,
            NormalValue = NormalValue
        };
    }
}
