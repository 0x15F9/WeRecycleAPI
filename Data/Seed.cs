using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task Seeding(DataContext context){
            // If there are users => no need to seed
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/AdminSeed.json");
            var users = JsonSerializer.Deserialize<List<Admin>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1234"));

                await context.Admins.AddAsync(user);
            }

            var binData = await System.IO.File.ReadAllTextAsync("Data/BinSeed.json");
            var bins = JsonSerializer.Deserialize<List<Bin>>(binData);

            foreach (var bin in bins)
            {
                await context.Bins.AddAsync(bin);
            }

            await context.SaveChangesAsync();
        }        
    }
}