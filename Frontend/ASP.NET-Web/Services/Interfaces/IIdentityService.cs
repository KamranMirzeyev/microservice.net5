using ASP.NET_Web.Models;
using IdentityModel.Client;
using Shared.DTO;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Responce<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
