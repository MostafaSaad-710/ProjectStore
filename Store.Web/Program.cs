
using AutoMapper;
using Domaine.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.Contexts;
using Services;
using Services.Abstraction;
using Services.Mapping.Products;
using System.Threading.Tasks;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));

            });

            builder.Services.AddScoped<IDbIntializer, DbIntializer>();
            builder.Services.AddScoped<IUnitofwork, Unitofwork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile(builder.Configuration)));

            var app = builder.Build();

            app.UseStaticFiles();

            #region Intializar
            using var Scope = app.Services.CreateScope();
            var dbIntializer = Scope.ServiceProvider.GetRequiredService<IDbIntializer>(); // Ask CLr Create Object From IDbIntializer
            await dbIntializer.IntializeAsync();

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
