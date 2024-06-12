using SupportOperatorsSalaryAPI.Data.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs.Attributes
{
    public class ValidNamesAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) 
                return false;

            var a = Array.ConvertAll(((Array)value).Cast<object>().ToArray(), x => x as Parameter);
            object[] array = ((Array)value).Cast<object>().ToArray();
            UpdatedParameter[] updatedParameters = Array.ConvertAll(array, obj => (UpdatedParameter)obj);
            bool unique = updatedParameters.ToList().Distinct().Count() == array.Length;

            return value.GetType().IsArray && unique;
        }
    }
}
