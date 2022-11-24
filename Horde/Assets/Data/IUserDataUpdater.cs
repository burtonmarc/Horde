using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public interface IUserDataUpdater
    {
        Task UpdateUserData<T>(T data) where T : IUserData;
    }
}