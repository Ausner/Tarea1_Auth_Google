using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddDbContext...
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

        }).AddCookie()
        .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = "259707643668-hgbam22n7jl3hrmfnfdgs25q90saccqp.apps.googleusercontent.com";
            options.ClientSecret = "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";
            options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");

        });
        services.AddMvc(options =>
        {
            options.EnableEndpointRouting = false;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=index}/{id?}"
            );
        });
    }
}