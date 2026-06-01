using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Models;
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

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(SearchDto searchDto)
        {
            var query = _context.Notifications.AsQueryable();
            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                query = query.Where(n => n.Message.Contains(searchDto.SearchTerm) || n.Channel.Contains(searchDto.SearchTerm));
            }
            return await query.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize).ToListAsync();
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