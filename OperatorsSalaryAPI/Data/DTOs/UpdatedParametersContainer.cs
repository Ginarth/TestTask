using SupportOperatorsSalaryAPI.Data.DTOs.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs
{
    public class UpdatedParametersContainer
    {
        [Required]
        [MinLength(4), MaxLength(4)]
        [WeightsSum(1, ErrorMessage = "Sum of the weights must be equal 1.00")]
        [ValidNames(ErrorMessage = "Parameter names must be unique")]
        [ValidValuesSum(ErrorMessage = "Base value and normal value should not be the same")]
        public UpdatedParameter[] UpdatedParameters { get; set; } = new UpdatedParameter[4];
    }
}
