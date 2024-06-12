namespace SupportOperatorsSalaryAPI.Data.DTOs.Interfaces
{
    public interface IConverted<T> where T : class
    {
        public T Convert();
    }
}
