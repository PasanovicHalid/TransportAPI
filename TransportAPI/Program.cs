using Application;
using Infrastructure;
using Presentation;
using TransportAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .SetupPresentationLayer()
                .SetupInfrastrucutureLayer(builder.Configuration)
                .SetupApplicationLayer()
                .AddSwaggerGen()
                .AddControllers();

//builder.Services.SetupDependencies();
//builder.Services.SetupAuthentication(builder.Configuration);
//builder.Services.SetupDBs(builder.Configuration);
//builder.Services.SetupSettings(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Transport Application");
    });
}

await app.InitializeDB();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
