using Barber.API.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Barber.API.Repositories
{
    public class BarberRepository : IBarberRepository
    {
        private readonly IConfiguration _configuration;

        public BarberRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateBarber(HairDresser barber)
        {
            using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                ("INSERT INTO HairDresser(Name,Family,Age,MeliNo,TellNo,Email) VALUES (@name,@Family,@Age,@MeliNo,@TellNo,@Email)",
                new { Name = barber.Name, Family = barber.Family, Age = barber.Age, MeliNo = barber.MeliNo, TellNo = barber.TellNo, Email = barber.Email });

            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public Task<bool> DeleteBarber(string MeliNo)
        {
            throw new NotImplementedException();
        }

        public Task<HairDresser> GetBarber(string MeliNo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBarber(HairDresser barber)
        {
            throw new NotImplementedException();
        }
    }
}
