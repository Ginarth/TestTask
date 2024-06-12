using Microsoft.EntityFrameworkCore;
using SupportOperatorsSalaryAPI.Data.Database;
using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.Repositories.Interfaces;

namespace SupportOperatorsSalaryAPI.Data.Repositories
{
    public class ParameterRepository(DatabaseContext context) : IParameterRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<IEnumerable<Parameter>> Read()
        {
            return await _context.Parameters.AsNoTracking().ToListAsync();
        }

        public async Task<Parameter?> Read(string name)
        {
            return await _context.Parameters.AsNoTracking().FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<Parameter?> Update(Parameter parameter)
        {
            Parameter? existingParameter = await Read(parameter.Name);

            if (existingParameter is not null)
            {
                _context.Parameters.Update(parameter);
                await _context.SaveChangesAsync();
                return parameter;
            }

            return null;
        }
    }
}
