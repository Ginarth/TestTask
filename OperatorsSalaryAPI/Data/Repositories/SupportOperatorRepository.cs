using Microsoft.EntityFrameworkCore;
using SupportOperatorsSalaryAPI.Data.Database;
using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.Repositories.Interfaces;

namespace SupportOperatorsSalaryAPI.Data.Repositories
{
    public class SupportOperatorRepository(DatabaseContext context) : ISupportOperatorRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<SupportOperator?> Create(SupportOperator supportOperator)
        {
            await _context.SupportOperators.AddAsync(supportOperator);
            await _context.SaveChangesAsync();
            return supportOperator;
        }

        public async Task<IEnumerable<SupportOperator>> Read()
        {
            return await _context.SupportOperators.AsNoTracking().ToListAsync();
        }

        public async Task<SupportOperator?> Read(int id)
        {
            return await _context.SupportOperators.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<SupportOperator?> Update(SupportOperator supportOperator)
        {
            SupportOperator? existingSupportOperator = await Read(supportOperator.Id);

            if (existingSupportOperator is not null)
            {
                _context.SupportOperators.Update(supportOperator);
                await _context.SaveChangesAsync();
                return supportOperator;
            }

            return null;
        }

        public async Task<SupportOperator?> Patch(int id, bool isWorking)
        {
            SupportOperator? existingSupportOperator = await Read(id);

            if (existingSupportOperator is not null)
            {
                existingSupportOperator.IsWorking = isWorking;
                _context.SupportOperators.Update(existingSupportOperator);
                await _context.SaveChangesAsync();
                return existingSupportOperator;
            }
            
            return null;
        }
    }
}
