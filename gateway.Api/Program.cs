using Ocelot.DependencyInjection; //For Dependency Injection
using Ocelot.Middleware; //For middleware
using Ocelot.Cache.CacheManager; //For middleware
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.AddJsonFile($"ocelot.{env}.json");
builder.Host.ConfigureLogging(logging =>logging.AddConsole());

// builder.Host.ConfigureLogging(logging =>
// {
//     logging.ClearProviders();
//     logging.AddConsole();
// });

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// builder.Services.AddOcelot();
builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

var secretKey = "asdwda1d8a4sd8w4das8d*w8d*asd@#";
var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddAuthentication(options=>
            {
	            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>{
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
	            options.TokenValidationParameters =  new TokenValidationParameters{
	                ValidateIssuer = false,
                    ValidateAudience = false,
                    // ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // ValidIssuer = Configuration["Authentication:Issuer"],
                    // ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey =  new SymmetricSecurityKey(key)	
	            };
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    // app.UseSwagger();
    // app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication(); //JwtBearer

app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

app.Run();
