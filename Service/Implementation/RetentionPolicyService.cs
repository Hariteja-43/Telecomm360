using Telecom360.Services.Interface;
using Telecom360.Repository.Interface;
using Telecom360.DTO.Retention;
using Telecom360.Models;

namespace Telecom360.Services.Implementation
{
    public class RetentionPolicyService : IRetentionPolicyService
    {
        private readonly IRetentionPolicyRepository _repo;

        public RetentionPolicyService(IRetentionPolicyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RetentionPolicyResponseDto>> GetAllRetentionPolicy()
        {
            var policies = await _repo.GetAllRetentionPolicy();

            return policies.Select(p => new RetentionPolicyResponseDto
            {
                PolicyID = p.PolicyID,
                DataType = p.DataType,
                RetentionPeriod = p.RetentionPeriod,
                AppliedFrom = p.AppliedFrom
            });
        }

        public async Task<RetentionPolicyResponseDto> GetRetentionPolicyById(int policyID)
        {
            var policy = await _repo.GetRetentionPolicyById(policyID);
            if (policy == null) return null;

            return new RetentionPolicyResponseDto
            {
                PolicyID = policy.PolicyID,
                DataType = policy.DataType,
                RetentionPeriod = policy.RetentionPeriod,
                AppliedFrom = policy.AppliedFrom
            };
        }

        public async Task<RetentionPolicyResponseDto> CreateRetentionPolicy(CreateRetentionPolicyRequestDto request)
{
    var policy = new RetentionPolicy
    {
        // ❌ DO NOT SET PolicyID here
        DataType = request.DataType,
        RetentionPeriod = request.RetentionPeriod,
        AppliedFrom = request.AppliedFrom
    };

    var created = await _repo.CreateRetentionPolicy(policy);

    return new RetentionPolicyResponseDto
    {
        PolicyID = created.PolicyID, // ✅ DB-generated value
        DataType = created.DataType,
        RetentionPeriod = created.RetentionPeriod,
        AppliedFrom = created.AppliedFrom
    };
}

        public async Task<RetentionPolicyResponseDto> UpdateRetentionPolicy(int policyID, UpdateRetentionPolicyRequestDto request)
        {
            var existing = await _repo.GetRetentionPolicyById(policyID);
            if (existing == null) return null;

            existing.DataType = request.DataType ?? existing.DataType;
            existing.RetentionPeriod = request.RetentionPeriod != 0 ? request.RetentionPeriod : existing.RetentionPeriod;

            var updated = await _repo.UpdateRetentionPolicy(existing);

            return new RetentionPolicyResponseDto
            {
                PolicyID = updated.PolicyID,
                DataType = updated.DataType,
                RetentionPeriod = updated.RetentionPeriod,
                AppliedFrom = updated.AppliedFrom
            };
        }
    }
}