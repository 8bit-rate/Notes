using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Notes.WebApi
{
	public class ConfigureSwaggerOption : IConfigureOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		public ConfigureSwaggerOption(IApiVersionDescriptionProvider provider) => _provider = provider;

		public void Configure(SwaggerGenOptions options)
		{
			foreach (var description in _provider.ApiVersionDescriptions)
			{
				var apiVersion = description.ApiVersion.ToString();
				options.SwaggerDoc(description.GroupName,
					new OpenApiInfo
					{
						// TODO: Добавить что-то в информацию
						Version = apiVersion,
						Title = $"Notes API {apiVersion}",
						Description =
							"A simple ASP NET Core Web API application.",
						Contact = new OpenApiContact
						{
							Name = "Danil",
							Email = "sherlokDP@gmail.com"
						},
						License = new OpenApiLicense
						{
							Name = "License",

						}
					});
				options.AddSecurityDefinition($"AuthToken {apiVersion}",
					new OpenApiSecurityScheme
					{
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.Http,
						BearerFormat = "JWT",
						Scheme = "bearer",
						Name = "Authorization",
						Description = "Authorization token"
					});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = $"AuthToken {apiVersion}",
							}
						},
						new string[] { }
					}
				});
				options.CustomOperationIds(apiDescription =>
					apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
						? methodInfo.Name
						: null);
			}
		}
	}
}
