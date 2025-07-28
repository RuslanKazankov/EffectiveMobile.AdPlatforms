namespace EffectiveMobile.AdPlatforms.Api;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseEndpoints(builder =>
            {
                builder.MapOpenApi();
            });
        }
        
        app.UseHttpsRedirection();
    }
}