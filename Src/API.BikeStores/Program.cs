 using API.BikeStores.Managers;
using API.BikeStores.Services;

using API.Pitstop.Products;
using API.Pitstop.Products.Managers;
using API.Pitstop.Products.Middlewares;
using API.Pitstop.Products.RefitClients;
using API.Pitstop.Products.Services;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.OpenApi.Models;
using Refit;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
		//System.IO.InvalidDataException: 'Failed to load configuration from file 'D:\BikeStores-API\BikeStores-API\Src\API.BikeStores\appsettings.Development.json'.'


		// Add services to the container.

		builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "1",
                Title = "Template API",
                Description = "API for managing users"
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.AddSecurityDefinition(Constants.ApiKey, new OpenApiSecurityScheme
            {
                Description = $"{Constants.ApiKey} must appear in header",
                Type = SecuritySchemeType.ApiKey,
                Name = Constants.ApiKey,
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });
            var key = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = Constants.ApiKey
                },
                In = ParameterLocation.Header
            };
            var requirement = new OpenApiSecurityRequirement
                            {
                             { key, new List<string>() }
                            };
            options.AddSecurityRequirement(requirement);
        });



        builder.Services.AddApiVersioning(setupAction =>
        {
            setupAction.AssumeDefaultVersionWhenUnspecified = true;
            setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            setupAction.ReportApiVersions = true;
        });

        builder.Services.AddApplicationInsightsTelemetry();

        //Application services
        builder.Services.AddScoped<IProductsService, ProductsService>();
        builder.Services.AddTransient<IProductsManager, ProductsManager>();

        builder.Services.AddScoped<IBikeStoresService, BikeStoresService>();
        builder.Services.AddScoped<IBikeStoresManager , BikeStoresManager>();

        builder.Services.AddScoped<IStudentsService, StudentsService>();
        builder.Services.AddTransient<IStudentsManager, StudentsManager>();

		builder.Services.AddScoped<IOrderService, OrderService>();
		builder.Services.AddTransient<IOrderManager, OrderManager>();

        builder.Services.AddScoped<IStaffService , StaffService>();
        builder.Services.AddTransient<IStaffManager , StaffManager>();

        builder.Services.AddScoped<IStoresService, StoresService>();
        builder.Services.AddTransient<IStoresManager, StoresManager>();




		//Refit clients
		builder.Services.AddRefitClient<IStudentClient>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["BikeStoresService:BaseUrl"]);
                c.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration["BikeStoresService:Ocp-Apim-Subscription-Key"]);
            });


        builder.Services.AddCors(options =>
        {
            options.AddPolicy(Constants.PolicyAllowedAllOrigins,
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });
            app.UseSwaggerUI();
        }

        //Middlewares
        if (!app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
        app.UseMiddleware<ApiKeyMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}