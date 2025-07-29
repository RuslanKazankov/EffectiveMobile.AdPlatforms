namespace EffectiveMobile.AdPlatforms.Api;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();

        services.AddSwaggerGen();
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