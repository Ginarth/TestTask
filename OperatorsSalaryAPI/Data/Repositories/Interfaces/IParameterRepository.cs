using SupportOperatorsSalaryAPI.Data.Database.Entities;

namespace SupportOperatorsSalaryAPI.Data.Repositories.Interfaces
{
    public interface IParameterRepository
    {
        public Task<IEnumerable<Parameter>> Read();
        public Task<Parameter?> Read(string name);
        public Task<Parameter?> Update(Parameter parameter);
    }
}
