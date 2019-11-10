using CIS174_TestCoreApp.Models;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Services
{
    public interface IPersonManagerService
    {
        Task<UserManagerUpdateCommandModel> FindUserByName(string username);
        Task<bool> UpdateUser(UserManagerUpdateCommandModel model);
    }
}
