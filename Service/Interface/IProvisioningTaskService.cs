using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{
    public interface IProvisioningTaskService
    {
        // Method to create a new provisioning task we will call this method in the controller when we want to create a new provisioning task
        Task<ProvisioningTaskResponseDto> CreateTaskAsync(CreateProvisioningTaskRequestDto dto);
        // Method to get all provisioning tasks
        Task<List<ProvisioningTaskResponseDto>> GetAllTasksAsync();

        // Method to get a provisioning task by its ID
        Task<ProvisioningTaskResponseDto?> GetTaskByIdAsync(int id);

        Task<ProvisioningTaskResponseDto?> UpdateTaskAsync(int id, CreateProvisioningTaskRequestDto dto);

        Task<bool> DeleteTaskAsync(int id);
    }
}