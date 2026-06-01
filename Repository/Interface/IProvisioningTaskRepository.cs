using Telecomm360.Model;

namespace Telecomm360.Repository
{
    public interface IProvisioningTaskRepository
    {
        Task<ProvisioningTask> AddTaskAsync(ProvisioningTask task);

        Task<List<ProvisioningTask>> GetAllTasksAsync();

        Task<ProvisioningTask?> GetTaskByIdAsync(int id);

        Task UpdateTaskAsync(ProvisioningTask task);

        Task DeleteTaskAsync(ProvisioningTask task);
    }
}