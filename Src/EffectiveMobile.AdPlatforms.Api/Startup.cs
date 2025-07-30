using EffectiveMobile.AdPlatforms.Domain.Common;
using EffectiveMobile.AdPlatforms.Infrastructure.Common;

namespace EffectiveMobile.AdPlatforms.Api;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddControllers();

        services.AddDomainServices();
        services.AddInfrastructureServices();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseRouting();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseEndpoints(builder =>
        {
            if (env.IsDevelopment())
            {
                builder.MapOpenApi();
            }
            builder.MapControllers();
        });
        
        app.UseHttpsRedirection();
    }
}