using ASP_NET_API.Model;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Data Source=SQL8002.site4now.net;Initial Catalog=db_a89473_sem1234bd;User Id=db_a89473_sem1234bd_admin;Password=PASSworld93;TrustServerCertificate=True";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserRepository, UserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddTransient<IRagionRepository, RagionRepository>(provider => new RagionRepository(connectionString));
builder.Services.AddTransient<IHeadingRepository, HeadingRepository>(provider => new HeadingRepository(connectionString));
builder.Services.AddTransient<IAdvertisementRepository, AdvertisementRepository>(provider => new AdvertisementRepository(connectionString));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
