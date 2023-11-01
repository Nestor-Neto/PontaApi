using Application.AutoMapper;
using Infra.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddDbContext<PostgrSqlContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionString"], opt =>
    {
        opt.CommandTimeout(180);
        opt.EnableRetryOnFailure(5);
    });
});
// Add services
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices().AddModels();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
});


// Add services to the Authentication.
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


})

            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API - Ponta Gestão Agropecuária", Version = string.Concat("V: ", builder.Configuration["Version"]), });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Autorização header esquema Bearer.\r\n\r\n Digite 'Bearer' coloque [espaço] e depois seu token na entrada de texto abaixo. \r\n\r\nExemplo: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                        }
                    });

});

builder.Services.AddControllersWithViews()
.AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["Observabilidade"], rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.With()
    .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PostgrSqlContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
