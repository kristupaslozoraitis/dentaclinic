using DentaClinic.Database;
using DentaClinic.Models;
using DentaClinic.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DentaClinic.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly WebDbContext _dbContext;

        public ServiceRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Service> Create(Service service)
        {
            _dbContext.Add(service);
            await _dbContext.SaveChangesAsync();

            return service;
        }

        public async Task Delete(Service service)
        {
            _dbContext.Services.Remove(service);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Service?> Get(int id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(service => service.Id == id);
        }

        public async Task<List<Service>> GetAll()
        {
            return await _dbContext.Services.ToListAsync();
        }

        public async Task<Service> Update(Service service)
        {
            _dbContext.Services.Update(service);
            await _dbContext.SaveChangesAsync();

            return service;
        }
    }

    public interface IServiceRepository
    {
        Task<List<Service>> GetAll();
        Task<Service?> Get(int id);
        Task<Service> Create(Service service);
        Task<Service> Update(Service service);
        Task Delete(Service service);
    }
}
