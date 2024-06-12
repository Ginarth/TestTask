using SupportOperatorsSalaryAPI.Data.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs.Attributes
{
    public class ValidNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && Parameter.Names.Contains(value);
        }
    }
}
