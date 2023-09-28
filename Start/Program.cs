using PlanBee.University_portal.backend.Domain.Utils;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Repositories.Implementations;
using PlanBee.University_portal.backend.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var appConfig = AppConfigUtil.Config;
builder.Services.AddRepositories(appConfig);
builder.Services.AddServices(appConfig);
builder.Services.AddHandlers();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();