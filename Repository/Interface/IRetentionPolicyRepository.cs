using Telecom360.Models;

namespace Telecom360.Repository.Interface
{
    public interface IRetentionPolicyRepository
    {
        Task<IEnumerable<RetentionPolicy>> GetAllRetentionPolicy();
        Task<RetentionPolicy> GetRetentionPolicyById(int policyID);
        Task<RetentionPolicy> CreateRetentionPolicy(RetentionPolicy policy);
        Task<RetentionPolicy> UpdateRetentionPolicy(RetentionPolicy policy);
    }
}