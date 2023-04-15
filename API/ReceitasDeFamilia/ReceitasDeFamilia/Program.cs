using ReceitasDeFamilia;
using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories;
using ReceitasDeFamilia.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IFamiliaRepository, FamiliaRepository>();
builder.Services.AddSingleton<ICategoriaReceitaRepository, CategoriaReceitaRepository>();
builder.Services.AddSingleton<ILoginService, LoginService>();


var AllowSpecificOrigins = " _myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                        policy =>
                        {
                            //policy.WithOrigins("http://127.0.0.1:5500");
                            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        });
});


//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
JwtSettings.PopulateSettings(builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"], builder.Configuration["Jwt:Key"]);

//var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],//TODO: nao sei direito pra que serve ainda
        //ValidAudience = builder.Configuration["Jwt:Audience"],//TODO: nao sei direito pra que serve ainda
        //IssuerSigningKey = new SymmetricSecurityKey(key),//esse � o antigo pegando da classe estatica config
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = Debugger.IsAttached ? TimeSpan.Zero : TimeSpan.FromMinutes(5)
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
