using TaskManager.Domain.Models.Request;
using TaskManager.Domain.Models.Response;

namespace TaskManager.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        AuthResponse Get(AuthRequest model);
    }
}
