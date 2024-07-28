using API;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;
public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

}