using Application.Files;
using Contract.Repos;
using Contract.Services;
using Infrustructure.PersistanceEf;
using Infrustructure.PersistanceMinio.Repos;
using Microsoft.EntityFrameworkCore;
using Minio;
var builder = WebApplication.CreateBuilder(args);

// Register MinIO client
builder.Services.AddSingleton<MinioClient>(sp =>
{
    var minioSettings = builder.Configuration.GetSection("Minio");
    return (MinioClient)new MinioClient()
        .WithEndpoint(minioSettings["Endpoint"])
        .WithCredentials(minioSettings["AccessKey"], minioSettings["SecretKey"])
        .WithSSL(bool.Parse(minioSettings["Secure"]))
        .Build();
});

builder.Services.AddDbContext<FileDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetSection("SqlServer:ConnectionString").Value));

// Register CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register repos and services
builder.Services.AddScoped<IFileRepo, FileRepo>();
builder.Services.AddScoped<IfileServices, UserFileService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Swagger UI (enable in dev or always)
app.UseSwagger();
app.UseSwaggerUI();

// Middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

// Map controllers
app.MapControllers();
app.MapStaticAssets();

app.Run();