using System.Collections.Generic;
using TaskManager.Domain.Events;

namespace TaskManager.Domain.Interfaces.Services
{
    public interface IServiceBase
    {
        void AddNotification(string value);

        bool HasNotifications();

        List<DomainNotification> GetNotifications();
    }
}
