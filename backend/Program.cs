using Microsoft.Net.Http.Headers;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ClothingStoreDBSettings>(builder.Configuration.GetSection("ClothingStoreDB"));
builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
            .WithMethods("POST")
            .WithHeaders(HeaderNames.AccessControlAllowOrigin, HeaderNames.ContentType)
            .AllowCredentials();
    });
});

// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(builder =>
//     {
//         builder.WithOrigins("http://127.0.0.1:5500")
//         .WithMethods("GET")
//         .WithHeaders(HeaderNames.AccessControlAllowOrigin, HeaderNames.ContentType)
//         .AllowCredentials();
//     });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
