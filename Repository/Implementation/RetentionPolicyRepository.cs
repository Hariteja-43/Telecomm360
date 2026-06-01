using Microsoft.EntityFrameworkCore;
using Telecom360.Data;
using Telecom360.Models;
using Telecom360.Repository.Interface;

namespace Telecom360.Repository.Implementation
{
    public class RetentionPolicyRepository : IRetentionPolicyRepository
    {
        private readonly AppDbContext _context;

        public RetentionPolicyRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL POLICIES
        public async Task<IEnumerable<RetentionPolicy>> GetAllRetentionPolicy()
        {
            return await _context.RetentionPolicies
                .AsNoTracking() // ✅ improves read performance
                .ToListAsync();
        }

        // ✅ GET POLICY BY ID
        public async Task<RetentionPolicy> GetRetentionPolicyById(int policyID)
        {
            return await _context.RetentionPolicies
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.PolicyID == policyID);
        }

        // ✅ CREATE POLICY
        public async Task<RetentionPolicy> CreateRetentionPolicy(RetentionPolicy policy)
        {
            await _context.RetentionPolicies.AddAsync(policy);
            await _context.SaveChangesAsync();

            return policy;
        }

        // ✅ UPDATE POLICY
        public async Task<RetentionPolicy> UpdateRetentionPolicy(RetentionPolicy policy)
        {
            var existing = await _context.RetentionPolicies
                .FirstOrDefaultAsync(r => r.PolicyID == policy.PolicyID);

            if (existing == null)
                return null;

            // ✅ Update fields
            existing.DataType = policy.DataType;
            existing.RetentionPeriod = policy.RetentionPeriod;
            existing.AppliedFrom = policy.AppliedFrom;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}