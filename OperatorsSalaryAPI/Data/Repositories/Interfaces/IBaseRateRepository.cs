using SupportOperatorsSalaryAPI.Data.Database.Entities;

namespace SupportOperatorsSalaryAPI.Data.Repositories.Interfaces
{
    public interface IBaseRateRepository
    {
        public Task<IEnumerable<BaseRate>> Read();
    }
}
