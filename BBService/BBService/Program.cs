using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BorrowedBookService.Data;
using BorrowedBookService.Repository;
using System;
using System.Text;
using BorrowedBookService.Helpers;
using BorrowedBookService.Services;
using Microsoft.AspNetCore.Routing;
using System.Configuration;
using AutoMapper;
using BorrowedBookService.Helpers;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("connectionString")));


builder.Services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
builder.Services.AddScoped<IBorrowedBookService, BorrowedBookServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperHelper));


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
  //  var services = scope.ServiceProvider;
    //var context = services.GetRequiredService<AppDbContext>();
    //await context.Database.MigrateAsync();
//}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});


app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
