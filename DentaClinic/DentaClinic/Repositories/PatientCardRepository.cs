using DentaClinic.Database;
using DentaClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentaClinic.Repositories
{
    public interface IPatientCardRepository
    {
        Task<List<PatientCard>> GetAll();
        Task<PatientCard?> Get(int id);
        Task<PatientCard> Create(PatientCard patientCard);
        Task<PatientCard> Update(PatientCard patientCard);
        Task Delete(PatientCard patientCard);
    }
    public class PatientCardRepository : IPatientCardRepository
    {
        private readonly WebDbContext _dbContext;

        public PatientCardRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PatientCard> Create(PatientCard patientCard)
        {
            _dbContext.Add(patientCard);
            await _dbContext.SaveChangesAsync();

            return patientCard;
        }

        public async Task Delete(PatientCard patientCard)
        {
            _dbContext.PatientCards.Remove(patientCard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PatientCard?> Get(int id)
        {
            return await _dbContext.PatientCards.FirstOrDefaultAsync(card => card.Id == id);
        }

        public async Task<List<PatientCard>> GetAll()
        {
            return await _dbContext.PatientCards.ToListAsync();
        }

        public async Task<PatientCard> Update(PatientCard patientCard)
        {
            _dbContext.PatientCards.Update(patientCard);
            await _dbContext.SaveChangesAsync();

            return patientCard;
        }
    }
}
