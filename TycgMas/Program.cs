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
    builder.Services.AddCors();//新增Cors
    builder.Services.AddControllersWithViews();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    #region angular畫面
    if (app.Environment.IsDevelopment())
    {
        app.UseHsts();//新增Angular
    }
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseStaticFiles();//新增Angular
    app.UseRouting();//新增Angular


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");//新增Angular

    app.MapFallbackToFile("index.html"); ;//新增Angular
    app.UseCors(builder =>//新增Cors
    {
        builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials();
    });
    app.UseAuthorization();//新增Angular
    app.MapControllers();//新增Angular
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

