using SupportOperatorsSalaryAPI.Data.Database.Entities;

namespace SupportOperatorsSalaryAPI.Data.Repositories.Interfaces
{
    public interface ISupportOperatorRepository
    {
        public Task<SupportOperator?> Create(SupportOperator supportOperator);
        public Task<IEnumerable<SupportOperator>> Read();
        public Task<SupportOperator?> Read(int id);
        public Task<SupportOperator?> Update(SupportOperator supportOperator);
        public Task<SupportOperator?> Patch(int id, bool isWorking);
    }
}
