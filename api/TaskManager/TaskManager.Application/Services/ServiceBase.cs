using System.Collections.Generic;
using TaskManager.Domain.Events;
using TaskManager.Domain.Interfaces.Services;

namespace TaskManager.Application.Services
{
    public abstract class ServiceBase : IServiceBase
    {
        private readonly List<DomainNotification> _notifications;

        protected ServiceBase()
        {
            _notifications = new List<DomainNotification>();
        }

        public void AddNotification(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            _notifications.Add(new DomainNotification(value));
        }

        public bool HasNotifications()
        {
            return _notifications.Count > 0;
        }

        public List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }
    }
}
