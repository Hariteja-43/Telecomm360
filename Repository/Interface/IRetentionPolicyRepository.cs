using Telecom360.Model;

namespace Telecom360.Repository.Interface
{
    public interface IRetentionPolicyRepository
    {
        Task<IEnumerable<RetentionPolicy>> GetAllRetentionPolicy();
        Task<RetentionPolicy> GetRetentionPolicyById(int retentionPeriodId);
        Task<RetentionPolicy> CreateRetentionPolicy(RetentionPolicy policy);
        Task<RetentionPolicy> UpdateRetentionPolicy(RetentionPolicy policy);
    }
}