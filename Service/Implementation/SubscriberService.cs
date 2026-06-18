using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repository;
using Telecomm360.Service.Interface;

namespace Telecomm360.Service.Implementation
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _repository;

        public SubscriberService(ISubscriberRepository repository)
        {
            _repository = repository;
        }

       
        public async Task<SubscriberResponseDto> CreateSubscriberAsync(CreateSubscriberRequestDto dto)
        {
            var subscriber = new Subscriber
            {
                CustomerId = dto.CustomerId,
                MSISDN = dto.MSISDN,
                IMSI = dto.IMSI,
                DeviceId = dto.DeviceId,
                SIMStatus = Status.Active,
                Status = Status.Active
            };

            var created = await _repository.AddSubscriberAsync(subscriber);

            return new SubscriberResponseDto
            {
                SubscriberId = created.SubscriberId,
                CustomerId = created.CustomerId,
                MSISDN = created.MSISDN,
                IMSI = created.IMSI,
                DeviceId = created.DeviceId,
                SIMStatus = created.SIMStatus,
                Status = created.Status
            };
        }

        public async Task<List<SubscriberResponseDto>> GetAllSubscribersAsync()
        {
            var list = await _repository.GetAllSubscribersAsync();

            return list.Select(s => new SubscriberResponseDto
            {
                SubscriberId = s.SubscriberId,
                CustomerId = s.CustomerId,
                MSISDN = s.MSISDN,
                IMSI = s.IMSI,
                DeviceId = s.DeviceId,
                SIMStatus = s.SIMStatus,
                Status = s.Status
            }).ToList();
        }

      
        public async Task<SubscriberResponseDto?> GetSubscriberByIdAsync(int id)
        {
            var s = await _repository.GetSubscriberByIdAsync(id);

            if (s == null) return null;

            return new SubscriberResponseDto
            {
                SubscriberId = s.SubscriberId,
                CustomerId = s.CustomerId,
                MSISDN = s.MSISDN,
                IMSI = s.IMSI,
                DeviceId = s.DeviceId,
                SIMStatus = s.SIMStatus,
                Status = s.Status
            };
        }

        //  UPDATE
        public async Task<SubscriberResponseDto?> UpdateSubscriberAsync(int id, CreateSubscriberRequestDto dto)
        {
            var existing = await _repository.GetSubscriberByIdAsync(id);

            if (existing == null)
                return null;

            // ✅ Update fields
            existing.CustomerId = dto.CustomerId;
            existing.MSISDN = dto.MSISDN;
            existing.IMSI = dto.IMSI;
            existing.DeviceId = dto.DeviceId;

            await _repository.UpdateSubscriberAsync(existing);

            return new SubscriberResponseDto
            {
                SubscriberId = existing.SubscriberId,
                CustomerId = existing.CustomerId,
                MSISDN = existing.MSISDN,
                IMSI = existing.IMSI,
                DeviceId = existing.DeviceId,
                SIMStatus = existing.SIMStatus,
                Status = existing.Status
            };
        }

        // ✅ DELETE
        public async Task<bool> DeleteSubscriberAsync(int id)
        {
            var existing = await _repository.GetSubscriberByIdAsync(id);

            if (existing == null)
                return false;

            await _repository.DeleteSubscriberAsync(existing);
            return true;
        }

        public Task<SubscriberResponseDto?> UpdateSubscriberAsync(int subscriberId, UpdateSubscriberRequestDto request)
        {
            throw new NotImplementedException();
        }

        Task ISubscriberService.UpdateSubscriberAsync(int id, CreateSubscriberRequestDto dto)
        {
            return UpdateSubscriberAsync(id, dto);
        }
    }
}