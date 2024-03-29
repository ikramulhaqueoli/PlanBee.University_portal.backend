using PlanBee.University_portal.backend.Domain.Enums;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Utils;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Infrastructure;
using PlanBee.University_portal.backend.Repositories.Implementations;
using PlanBee.University_portal.backend.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var appConfig = AppConfigUtil.Config;
builder.Services.AddRepositories(appConfig);
builder.Services.AddInsfrastructure();
builder.Services.AddServices(appConfig);
builder.Services.AddHandlers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new EnumToStringArrayConverter<UserRole>());
    });

builder.Services.AddSwaggerGen(options => options.ConfigureEnumMappings());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.GetRequiredService<ISeedDataManager>().SaveToDbAsync().Wait();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.UseHttpsRedirection();

app.Run();