using Telecom360.DTO.Retention;

namespace Telecom360.Service.Interface
{
    public interface IRetentionPolicyService
    {
        Task<IEnumerable<RetentionPolicyResponseDto>> GetAllRetentionPolicy();
        Task<RetentionPolicyResponseDto> GetRetentionPolicyById(int policyID);
        Task<RetentionPolicyResponseDto> CreateRetentionPolicy(CreateRetentionPolicyRequestDto request);
        Task<RetentionPolicyResponseDto> UpdateRetentionPolicy(int policyID, UpdateRetentionPolicyRequestDto request);
    }
}