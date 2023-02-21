using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}/Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    Log.Information("Starting web host.");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors();//�s�WCors
    builder.Services.AddControllersWithViews();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    #region angular�e��
    if (app.Environment.IsDevelopment())
    {
        app.UseHsts();//�s�WAngular
    }
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseStaticFiles();//�s�WAngular
    app.UseRouting();//�s�WAngular


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");//�s�WAngular

    app.MapFallbackToFile("index.html"); ;//�s�WAngular
    app.UseCors(builder =>//�s�WCors
    {
        builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials();
    });
    app.UseAuthorization();//�s�WAngular
    app.MapControllers();//�s�WAngular
    #endregion


    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

