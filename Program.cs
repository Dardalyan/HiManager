using System.Text;
using HiManager.Connfiguration.db;
using HiManager.Repository.role;
using HiManager.Repository.user;
using HiManager.Service.current_user;
using HiManager.Service.jwt;
using HiManager.Service.role;
using HiManager.Service.user;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args); 
// Add JWT Authentication configuration ---------------------------------------------------------------------------------
builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
                .GetBytes(Environment.GetEnvironmentVariable("jwt_secret_key")!)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
}); 


// Add Http Context Accesor to Get Authenticated Current User -----------------------------------------------------------
builder.Services.AddHttpContextAccessor();

// Add Custom ApplicationDbContext to the container ---------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>();

// Add Repositories to the container ------------------------------------------------------------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();


// Add Services to the container ----------------------------------------------------------------------------------------
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddScoped<ITokenService, TokenService>();


// Add Controllers to the container -------------------------------------------------------------------------------------
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddSwaggerGen();

var app = builder.Build();
var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}
 */

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
