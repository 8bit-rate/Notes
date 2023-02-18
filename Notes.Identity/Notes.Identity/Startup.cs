using IdentityServer4.Models;

namespace Notes.Identity
{
	public class Startup
	{
		public IConfiguration AppConfiguration { get; }

		public Startup(IConfiguration configuration) =>
			AppConfiguration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityServer()
				.AddInMemoryApiResources(new List<ApiResource>())
				.AddInMemoryIdentityResources(new List<IdentityResource>())
				.AddInMemoryApiScopes(new List<ApiScope>())
				.AddInMemoryClients(new List<Client>())
				.AddDeveloperSigningCredential();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseIdentityServer();
			app.UseIdentityServer();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}