using ASP.NET_Web.Models;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
