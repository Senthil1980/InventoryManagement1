using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.ApplicationCore.Entities;

namespace InventoryManagement.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task  SeedAsync(AppDbContext appdbcontext)
        {
            if (!appdbcontext.CommandTypes.Any())
            {
                appdbcontext.CommandTypes.AddRange(GetCommandType());            
                await appdbcontext.SaveChangesAsync();
            }

        }
        static IEnumerable<Command> GetCommandType()
        {
            return new List<Command>()
            {
                new Command() { Type = "Create"},
                new Command() { Type = "Delete" },
                new Command() { Type = "UpdateBuy" },
                new Command() { Type = "UpdateSell" },
                new Command() { Type = "UpdateSellPrice" }
            };
        }
    }
}
