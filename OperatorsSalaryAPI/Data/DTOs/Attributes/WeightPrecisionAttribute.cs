using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs.Attributes
{
    public class WeightPrecisionAttribute(int allLength, int fractionalPartLegth) : ValidationAttribute
    {
        private readonly int _allLength = allLength;
        private readonly int _fractionalPartLegth = fractionalPartLegth;

        public override bool IsValid(object? value)
        {
            return value != null && BitConverter.GetBytes(decimal.GetBits(Convert.ToDecimal(value))[_allLength])[_fractionalPartLegth] < _allLength;
        }
    }
}
