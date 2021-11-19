using System;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}
