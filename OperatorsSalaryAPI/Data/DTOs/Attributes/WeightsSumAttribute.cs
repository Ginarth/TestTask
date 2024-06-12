using SupportOperatorsSalaryAPI.Data.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs.Attributes
{
    public class WeightsSumAttribute(double sum) : ValidationAttribute
    {
        private readonly double _sum = sum;

        public override bool IsValid(object? value)
        {
            if (value is null)
                return false;

            var a = Array.ConvertAll(((Array)value).Cast<object>().ToArray(), x => x as Parameter);
            object[] array = ((Array)value).Cast<object>().ToArray();
            UpdatedParameter[] updatedParameters = Array.ConvertAll(array, obj => (UpdatedParameter)obj);
            decimal sum = updatedParameters.Sum(p => p.Weight);
            bool equal = sum == new decimal(_sum);

            return value != null && value.GetType().IsArray && equal;
        }
    }
}
