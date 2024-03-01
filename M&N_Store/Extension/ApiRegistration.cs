using M_N_Store.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace M_N_Store.Extension
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services)
        {

            //Configure Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Configure IFileProvider
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
