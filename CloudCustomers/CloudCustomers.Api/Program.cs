using CloudCustomers.Api.Config;
using CloudCustomers.Api.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
ConfigureService(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureService(IServiceCollection services)
{
    services.Configure<UserApiOptions>(builder.Configuration.GetSection("UserApiOptions"));  
    //Singleton - Lives for the lifetime of the application
    //AddScoped - Traditionally scopped to http request itself
    //Transient - At anypoint if we require the new instance of UserService it will be created
    services.AddTransient<IUserService, UserService>();
    services.AddHttpClient<IUserService, UserService>();
    
}
