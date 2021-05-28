using Tesis.Entities;

namespace Tesis.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
