using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;

namespace Telecomm360.Repositories.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(SearchDtos searchDtos)
        {
            var query = _context.Notifications.AsQueryable();
            if (!string.IsNullOrEmpty(searchDtos.SearchTerm))
            {
                query = query.Where(n => n.Message.Contains(searchDtos.SearchTerm) || n.Channel.Contains(searchDtos.SearchTerm));
            }
            return await query.Skip((searchDtos.PageNumber - 1) * searchDtos.PageSize).Take(searchDtos.PageSize).ToListAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(long notificationId)
        {
            return await _context.Notifications.FindAsync(notificationId);
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(Notification notification)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}