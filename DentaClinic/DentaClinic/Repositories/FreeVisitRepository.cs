using DentaClinic.Database;
using DentaClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentaClinic.Repositories
{
    public class FreeVisitRepository : IFreeVisitRepository
    {
        private readonly WebDbContext _dbContext;

        public FreeVisitRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<FreeVisit> Create(FreeVisit freeVisit)
        {
            _dbContext.Add(freeVisit);
            await _dbContext.SaveChangesAsync();

            return freeVisit;
        }

        public async Task Delete(FreeVisit freeVisit)
        {
            _dbContext.FreeVisits.Remove(freeVisit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FreeVisit?> Get(int id)
        {
            return await _dbContext.FreeVisits.FirstOrDefaultAsync(freeVisit => freeVisit.Id == id);
        }

        public async Task<List<FreeVisit>> GetAll()
        {
            return await _dbContext.FreeVisits.ToListAsync();
        }

        public async Task<FreeVisit> Update(FreeVisit freeVisit)
        {
            _dbContext.FreeVisits.Update(freeVisit);
            await _dbContext.SaveChangesAsync();

            return freeVisit;
        }
    }

    public interface IFreeVisitRepository
    {
        Task<List<FreeVisit>> GetAll();
        Task<FreeVisit?> Get(int id);
        Task<FreeVisit> Create(FreeVisit freeVisit);
        Task<FreeVisit> Update(FreeVisit freeVisit);
        Task Delete(FreeVisit freeVisit);
    }
}
