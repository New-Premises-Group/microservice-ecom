using IW.Models.DTOs;
using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
