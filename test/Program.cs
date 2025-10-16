using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IPostService, PostService>(client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
})
.AddPolicyHandler(HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(1)))
.AddPolicyHandler(HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(5)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
