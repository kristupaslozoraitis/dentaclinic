using DentaClinic.Database;
using DentaClinic.Models;
using DentaClinic.Models.Dtos;
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
            return await _dbContext.FreeVisits.Where(freeVisit => !_dbContext.Visits.Any(visit => visit.FreeVisit.Id == freeVisit.Id)).ToListAsync();
        }

        public async Task<List<FreeVisit>> GetAllForOdontologist(string userId)
        {
            return await _dbContext.FreeVisits.Where(freeVisit => freeVisit.UserId == userId).ToListAsync();
        }

        public async Task<FreeVisitForOdontologistDto> GetAllOrderedVisitsForOdontologist(int visitId)
        {
            return await _dbContext.Visits.Join(
                _dbContext.PatientCards,
                visit => visit.PatientCard.Id,
                patientCard => patientCard.Id,
                (visit, patientCard) => new FreeVisitForOdontologistDto
                {
                    Id = visit.FreeVisit.Id,
                    Date = visit.Date,
                    Time = visit.Time,
                    UserId = visit.UserId,
                    DoctorFullName = string.Format("{0} {1}", visit.DoctorName, visit.DoctorSurname),
                    Service = visit.Service,
                    Patient = String.Format("{0} {1}", patientCard.Name, patientCard.Surname)
                }
            ).Where(x => x.Id == visitId).FirstOrDefaultAsync();
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
        Task<List<FreeVisit>> GetAllForOdontologist(string userId);
        Task<FreeVisit?> Get(int id);
        Task<FreeVisit> Create(FreeVisit freeVisit);
        Task<FreeVisit> Update(FreeVisit freeVisit);
        Task Delete(FreeVisit freeVisit);
        Task<FreeVisitForOdontologistDto> GetAllOrderedVisitsForOdontologist(int visitId);
    }
}
