
using AutoMapper;
using Domaine.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.Contexts;
using Services;
using Services.Abstraction;
using Services.Mapping.Products;
using Services.Specifications;
using Shared.ErrorModels;
using Store.Web.Extentions;
using Store.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();
             
            await app.ConfigureMiddlewares();


            app.Run();
        }
    }
}
