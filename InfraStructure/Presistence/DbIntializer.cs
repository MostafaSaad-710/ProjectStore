using Domaine.Contracts;
using Domaine.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbIntializer(StoreDbContext _context) : IDbIntializer
    {
        public async Task IntializeAsync()
        {
            //1. Create Database
            //2. Update Database
            if(_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }

            //3. data seeding

            //3.1 ProductBrands 
            if(!_context.ProductBrands.Any())
            {
                //1. Read All Data from jason file 
                //  \InfraStructure\Presistence\Data\DataSeeding\brands.json
                var ProductBrands =await File.ReadAllTextAsync(@"..\InfraStructure\Presistence\Data\DataSeeding\brands.json");

                // Convert data from Jason to list<>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrands);


                // Add List to Db
                if(brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                }

            }

            //3.2 ProductTypes 
            if(!_context.ProductTypes.Any())
            {
                //1. Read all detafrom json
                var ProductTypes = await File.ReadAllTextAsync(@"..\InfraStructure\Presistence\Data\DataSeeding\types.json");

                //2. Convert data from json to list
                var Types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypes);

                //3. Add data to the Db
                if(Types is not null && Types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(Types);
                }
            }

            //3.3 Product  
            if(!_context.Products.Any())
            {
                // 1. Read All data from JSon
                var pruducts = await File.ReadAllTextAsync(@"..\InfraStructure\Presistence\Data\DataSeeding\products.json");

                // 2. Convert data from Json To list
                var product = JsonSerializer.Deserialize<List<Product>>(pruducts);

                // 3. Add data to the Db
                if(product is not null && product.Count > 0)
                {
                    await _context.Products.AddRangeAsync(product);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
