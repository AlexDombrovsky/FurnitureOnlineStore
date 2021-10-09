using System.Threading.Tasks;

namespace Interfaces.Initializer
{
    public interface IDbInitializer
    {
        void Initialize();
        Task SeedData();
    }
}