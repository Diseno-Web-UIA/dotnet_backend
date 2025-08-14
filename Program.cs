using Microsoft.EntityFrameworkCore;
using backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Se agrega el Entity Framework
builder.Services.AddDbContext<lastprojectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type =>
    {
        if (type.Namespace != null && type.Namespace.Contains("backend.DTO"))
        {
            var nsParts = type.Namespace.Split('.');
            var module = nsParts.Last(); 
            return $"{module} {char.ToUpper(type.Name.ToLower()[0])+ type.Name.ToLower()[1..].ToLower()}";
        }

        return type.Name; 
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems["url"] = "https://api.server.asralabs.com/swagger/v1/swagger.json";
    });
}

    //app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
