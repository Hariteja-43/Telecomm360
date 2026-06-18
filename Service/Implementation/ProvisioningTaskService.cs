using Telecomm360.DTO;

using Telecomm360.Model;
using Telecomm360.Repository;   // keep only ONE repository namespace 
using Telecomm360.Service.Interface;
// using System.Linq;
// using System.Collections.Generic;
// using System.Threading.Tasks;

namespace Telecomm360.Service
{
    public class ProvisioningTaskService : IProvisioningTaskService
    {
        private readonly IProvisioningTaskRepository _repository;

        public ProvisioningTaskService(IProvisioningTaskRepository repository)
        {
            _repository = repository;
        }

       
        public async Task<ProvisioningTaskResponseDto> CreateTaskAsync(CreateProvisioningTaskRequestDto dto)
        {
            var task = new ProvisioningTask
            {
                OrderId = dto.OrderId,
                SubscriberId = dto.SubscriberId,
                MSISDN = dto.MSISDN,
                ResourceType = dto.ResourceType,
                Status = Status.Active
            };

            var created = await _repository.AddTaskAsync(task);

            return new ProvisioningTaskResponseDto
            {
                TaskId = created.TaskId,
                OrderId = created.OrderId,
                SubscriberId = created.SubscriberId,
                MSISDN = created.MSISDN,
                ResourceType = created.ResourceType,
                Status = created.Status
            };
        }

        
        public async Task<List<ProvisioningTaskResponseDto>> GetAllTasksAsync()
        {
            var list = await _repository.GetAllTasksAsync();

            return list.Select(t => new ProvisioningTaskResponseDto
            {
                TaskId = t.TaskId,
                OrderId = t.OrderId,
                SubscriberId = t.SubscriberId,
                MSISDN = t.MSISDN,
                ResourceType = t.ResourceType,
                Status = t.Status
            }).ToList();
        }

       
        public async Task<ProvisioningTaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                return null;

            return new ProvisioningTaskResponseDto
            {
                TaskId = task.TaskId,
                OrderId = task.OrderId,
                SubscriberId = task.SubscriberId,
                MSISDN = task.MSISDN,
                ResourceType = task.ResourceType,
                Status = task.Status
            };
        }

        
        public async Task<ProvisioningTaskResponseDto?> UpdateTaskAsync(int id, CreateProvisioningTaskRequestDto dto)
        {
            var existing = await _repository.GetTaskByIdAsync(id);

            if (existing == null)
                return null;

            existing.OrderId = dto.OrderId;
            existing.SubscriberId = dto.SubscriberId;
            existing.MSISDN = dto.MSISDN;
            existing.ResourceType = dto.ResourceType;

            await _repository.UpdateTaskAsync(existing);

            return new ProvisioningTaskResponseDto
            {
                TaskId = existing.TaskId,
                OrderId = existing.OrderId,
                SubscriberId = existing.SubscriberId,
                MSISDN = existing.MSISDN,
                ResourceType = existing.ResourceType,
                Status = existing.Status
            };
        }

       
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var existing = await _repository.GetTaskByIdAsync(id);

            if (existing == null)
                return false;

            await _repository.DeleteTaskAsync(existing);

            return true;
        }
    }
}