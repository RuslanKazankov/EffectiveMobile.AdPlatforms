namespace EffectiveMobile.AdPlatforms.Api;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseRouting();

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