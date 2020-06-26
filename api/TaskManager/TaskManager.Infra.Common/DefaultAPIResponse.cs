using System.Collections.Generic;
using TaskManager.Domain.Events;

namespace TaskManager.Infra.Common
{
    public class DefaultAPIResponse
    {
        public bool Success { get; set; }

        public dynamic Data { get; set; }

        public List<DomainNotification> Notifications { get; set; }
    }
}
