using SupportOperatorsSalaryAPI.Data.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SupportOperatorsSalaryAPI.Data.DTOs.Attributes
{
    public class ValidValuesSum : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null)
                return false;

            var a = Array.ConvertAll(((Array)value).Cast<object>().ToArray(), x => x as Parameter);
            object[] array = ((Array)value).Cast<object>().ToArray();
            UpdatedParameter[] updatedParameters = Array.ConvertAll(array, obj => (UpdatedParameter) obj);
            
            foreach (UpdatedParameter updatedParameter in updatedParameters)
            {
                if (updatedParameter.BaseValue == updatedParameter.NormalValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
