using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public interface IDataGateway
    {
        Task UpdateUserData<T>(T data) where T : IModelData;
    }
}