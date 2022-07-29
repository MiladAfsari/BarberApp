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
                ("INSERT INTO HairDresser(Id,Name,Family,Age,TellNo,Email) VALUES (@Id,@name,@Family,@Age,@TellNo,@Email)",
                new { Name = barber.Name, Family = barber.Family, Age = barber.Age, Id = barber.Id, TellNo = barber.TellNo, Email = barber.Email });

            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteBarber(string Id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                ("Delete FROM HairDresser WHERE Id = @Id", new { Id = Id });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<HairDresser> GetBarber(string Id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var barber = await connection.QueryFirstOrDefaultAsync<HairDresser>
                ("SELECT * FROM HairDresser WHERE Id=@Id", new { Id = Id });

            if (barber == null)
            {
                return new HairDresser { Name = "NO NAME", Family = "NOTHING" };
            }
            return barber;
        }

        public async Task<bool> UpdateBarber(HairDresser barber)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE HairDresser SET Name=@Name, Family = @Family, Age = @Age,TellNo = @TellNo,Email=@Email WHERE Id = @Id",
                            new { Name = barber.Name, Family = barber.Family, Age = barber.Age, TellNo = barber.TellNo, Email = barber.Email });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
