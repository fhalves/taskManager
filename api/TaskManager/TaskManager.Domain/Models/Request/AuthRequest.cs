using System;
using System.Text;

namespace TaskManager.Domain.Models.Request
{
    public class AuthRequest
    {
        public string User { get; set; }

        public string Password { get; set; }
    }
}
