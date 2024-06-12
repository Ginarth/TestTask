using Microsoft.EntityFrameworkCore;
using SupportOperatorsSalaryAPI.Data.Database;
using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.Repositories.Interfaces;

namespace SupportOperatorsSalaryAPI.Data.Repositories
{
    public class BaseRateRepository(DatabaseContext context) : IBaseRateRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<IEnumerable<BaseRate>> Read()
        {
            return await _context.BaseRates.AsNoTracking().ToListAsync();
        }
    }
}
