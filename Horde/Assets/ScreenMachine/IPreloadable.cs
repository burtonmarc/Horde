using System.Threading.Tasks;

namespace ScreenMachine
{
    public interface IPreloadable
    {
        Task Preload();
    }
}