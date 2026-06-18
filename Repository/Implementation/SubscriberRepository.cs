using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.Model;

namespace Telecomm360.Repository
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly AppDbContext _context;

        public SubscriberRepository(AppDbContext context)
        {
            _context = context;
        }

      
        public async Task<Subscriber> AddSubscriberAsync(Subscriber subscriber)
        {
            _context.Subscribers.Add(subscriber);
            await _context.SaveChangesAsync();
            return subscriber;
        }

        
        public async Task<List<Subscriber>> GetAllSubscribersAsync()
        {
            return await _context.Subscribers.ToListAsync();
        }

        
        public async Task<Subscriber?> GetSubscriberByIdAsync(int id)
        {
            return await _context.Subscribers.FindAsync(id);
        }

      
        public async Task UpdateSubscriberAsync(Subscriber subscriber)
        {
            _context.Subscribers.Update(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscriberAsync(Subscriber subscriber)
        {
            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();
        }
    }
}