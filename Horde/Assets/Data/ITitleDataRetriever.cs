using Data.Models;

namespace Data
{
    public interface ITitleDataRetriever
    {
        T GetTitleData<T>() where T : class, ISerializableData;
    }
}