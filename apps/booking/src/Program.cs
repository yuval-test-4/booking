using System.Reflection;
using Booking;
using Booking.APIs;
using Booking.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.RegisterServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseOpenApiAuthentication();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddCors(builder =>
{
    builder.AddPolicy(
        "MyCorsPolicy",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(["localhost"]).AllowCredentials();
        }
    );
});
builder.Services.AddDbContext<BookingDbContext>(
    (serviceProvider, opt) =>
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_URL");
        opt.UseNpgsql(connectionString);
    }
);
builder.Services.AddApiAuthentication();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
    db.Database.Migrate();
}
var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseStaticFiles();

    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/swagger.css");
    });

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
    }
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseApiAuthentication();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RolesManager.SyncRoles(services, app.Configuration);
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedDevelopmentData.SeedDevUser(services, app.Configuration);
}
app.Run();
