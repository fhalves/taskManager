using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Domain.Events
{
    public class DomainNotification
    {
        public DomainNotification(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
