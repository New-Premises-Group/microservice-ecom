using IW.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => builder
    .WithOrigins("https://studio.apollographql.com", "http://localhost:3000", "http://localhost:8080") // Add your allowed origins here
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
//app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapGraphQL();

app.Run();
