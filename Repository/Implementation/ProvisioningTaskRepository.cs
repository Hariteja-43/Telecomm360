using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.Model;

namespace Telecomm360.Repository
{
    public class ProvisioningTaskRepository : IProvisioningTaskRepository
    {
        private readonly AppDbContext _context;

        public ProvisioningTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProvisioningTask> AddTaskAsync(ProvisioningTask task)
        {
            _context.ProvisioningTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<ProvisioningTask>> GetAllTasksAsync()
        {
            return await _context.ProvisioningTasks.ToListAsync();
        }

        public async Task<ProvisioningTask?> GetTaskByIdAsync(int id)
        {
            return await _context.ProvisioningTasks.FindAsync(id);
        }

        public async Task UpdateTaskAsync(ProvisioningTask task)
        {
            _context.ProvisioningTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(ProvisioningTask task)
        {
            _context.ProvisioningTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}