using Domaine.Contracts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorModels;
using Store.Web.Middlewares;

namespace Store.Web.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
             
            services.AddWebServices();

            services.AddInfrastructureServices(configuration);


            services.AddApplicationServices(configuration);


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Any())
                                              .Select(e => new ValidationError()
                                              {
                                                  Field = e.Key,
                                                  Errors = e.Value.Errors.Select(er => er.ErrorMessage)
                                              }).ToList();

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });


            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }



        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
             
            // Ask CLR To Create Scope To Get Object From IDbIntializer To Intialize Db
            #region Intializar Db
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


            app.UseMiddleware<GlobalErrorHandlingMidleware>();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();
 
            app.MapControllers();

             

            return app;
        }
    }    
}
