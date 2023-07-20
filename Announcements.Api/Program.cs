using Announcements.Test.Application.Extensions;
using Announcements.Test.Application.Options;
using Announcements.Test.Infrastructure.Extensions;
using Announcements.Test.Infrastructure.Options;
using Announcements.Test.Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
// Add services to the container.

services.Configure<AnnouncementOptions>(configuration.GetSection(AnnouncementOptions.Position));
services.Configure<LocalFileStorageOptions>(configuration.GetSection(LocalFileStorageOptions.Position));

services.AddApplicationLayer();
services.AddInfrastructureLayer();
services.AddPersistenceLayer(configuration);

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
