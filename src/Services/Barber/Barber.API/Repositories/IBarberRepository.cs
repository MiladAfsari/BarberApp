using Barber.API.Entity;
using System.Threading.Tasks;

namespace Barber.API.Repositories
{
    public interface IBarberRepository
    {
        Task<HairDresser> GetBarber(string Id);
        Task<bool> CreateBarber(HairDresser barber);
        Task<bool> UpdateBarber(HairDresser barber);
        Task<bool> DeleteBarber(string Id);

    }
}
