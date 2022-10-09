using DentaClinic.Database;
using DentaClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentaClinic.Repositories
{
    public interface IVisitRepository
    {
        Task<List<Visit>> GetAll(int patientCardId);
        Task<Visit> Get(int id);
        Task<Visit> Create(Visit patientCard);
        Task<Visit> Update(Visit patientCard);
        Task Delete(Visit patientCard);
    }
    public class VisitRepository : IVisitRepository
    {
        private readonly WebDbContext _dbContext;

        public VisitRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Visit> Create(Visit visit)
        {
            _dbContext.Add(visit);
            await _dbContext.SaveChangesAsync();

            return visit;
        }

        public async Task Delete(Visit visit)
        {
            _dbContext.Visits.Remove(visit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Visit> Get(int id)
        {
            return await _dbContext.Visits.FirstOrDefaultAsync(visit => visit.Id == id);
        }

        public async Task<List<Visit>> GetAll(int patientCardId)
        {
            return await _dbContext.Visits.Where(visit => visit.PatientCard.Id == patientCardId).ToListAsync();
        }

        public async Task<Visit> Update(Visit visit)
        {
            _dbContext.Visits.Update(visit);
            await _dbContext.SaveChangesAsync();

            return visit;
        }
    }
}
