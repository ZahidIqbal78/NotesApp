using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NotesApp.API.Data;
using NotesApp.API.DTOs.Note;
using NotesApp.API.DTOs.User;
using NotesApp.API.Helpers;
using NotesApp.API.Middlewares;
using NotesApp.API.Services.Dashboard;
using NotesApp.API.Services.Note;
using NotesApp.API.Services.User;
using NotesApp.API.Validators.Note;
using NotesApp.API.Validators.User;
using Serilog;
using Serilog.Exceptions;
using System.Net;

Log.Logger = new LoggerConfiguration()
    .Enrich.WithClientIp()
    .Enrich.WithClientAgent()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console(Serilog.Events.LogEventLevel.Debug,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{ClientIp}] [{ClientAgent}] {Message:lj}{NewLine}{Exception}")
    .CreateBootstrapLogger();

Log.Information("Starting application...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors();

    builder.Services.AddAutoMapper(typeof(Program));

    #region DBContext
    Log.Information("Setting up in memory database context service");
    builder.Services.AddDbContext<NotesAppDbContext>(options =>
        options.UseInMemoryDatabase("NotesAppInMemDb")
    );
    Log.Information("Finished setting up in memory database context service");
    #endregion

    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
        options.HttpsPort = 7088; //443;
    });

    builder.Host.UseSerilog();
    builder.Services.AddHttpContextAccessor();
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    #region ServicesAndHelpers
    builder.Services.AddScoped<IJwtUtility, JwtUtility>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<INoteService, NoteService>();
    builder.Services.AddScoped<IUserDashboardService, UserDashboardService>();
    #endregion

    #region Validators
    builder.Services.AddScoped<IValidator<UserLoginRequestDto>, UserLoginRequestValidator>();
    builder.Services.AddScoped<IValidator<UserRegisterRequestDto>, UserRegisterRequestValidator>();
    builder.Services.AddScoped<IValidator<AddNoteRequestDto>, NoteAddRequestValidator>();
    #endregion


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(x => x
        .WithOrigins("https://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader());

    #region APP-SerilogRequestLogging
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "Handled [{RequestMethod}] : [{RequestPath}] ; Responded with {StatusCode}";
        
        if(app.Environment.IsDevelopment())
        {
            options.GetLevel = (httpContext, elapsed, ex) => Serilog.Events.LogEventLevel.Debug;
        }
        else
        {
            options.GetLevel = (httpContext, elapsed, ex) => Serilog.Events.LogEventLevel.Information;
        }
    });
    #endregion

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<JwtMiddleware>();

    app.Run();
}
catch (Exception ex)
{
    Log.Error("Failed to start the application", ex);
    throw;
}
finally
{
    Log.Information("Shutting down application...");
    Log.Information("Shutdown complete");
    Log.CloseAndFlush();
}

