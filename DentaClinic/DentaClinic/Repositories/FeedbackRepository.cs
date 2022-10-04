using DentaClinic.Database;
using DentaClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentaClinic.Repositories
{
    public interface IFeedbackRepository
    {
        Task<List<VisitFeedback>> GetAll(int visitId);
        Task<VisitFeedback> Get(int id);
        Task<VisitFeedback> Create(VisitFeedback patientCard);
        Task<VisitFeedback> Update(VisitFeedback patientCard);
        Task Delete(VisitFeedback patientCard);
    }
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly WebDbContext _dbContext;

        public FeedbackRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VisitFeedback> Create(VisitFeedback feedback)
        {
            _dbContext.Add(feedback);
            await _dbContext.SaveChangesAsync();

            return feedback;
        }

        public async Task Delete(VisitFeedback feedback)
        {
            _dbContext.VisitFeedbacks.Remove(feedback);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<VisitFeedback> Get(int id)
        {
            return await _dbContext.VisitFeedbacks.FirstOrDefaultAsync(feedback => feedback.Id == id);
        }

        public async Task<List<VisitFeedback>> GetAll(int visitId)
        {
            return await _dbContext.VisitFeedbacks.Where(feedback => feedback.VisitId == visitId).ToListAsync();
        }

        public async Task<VisitFeedback> Update(VisitFeedback feedback)
        {
            _dbContext.VisitFeedbacks.Update(feedback);
            await _dbContext.SaveChangesAsync();

            return feedback;
        }
    }
}
