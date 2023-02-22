using DataAccess.Core;
using DataAccess.Core.Repositories;
using DataAccess.Core.UnitOfWorks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
        b => b.MigrationsAssembly("DataAccess.Core"));

});



builder.Services.AddTransient<INutrientRepository, NutrientRepository>();
builder.Services.AddTransient<INutritionRepository, NutritionRepository>();
builder.Services.AddTransient<IRecipeCategoryRepository, RecipeCategoryRepository>();
builder.Services.AddTransient<IRecipeRepository, RecipeRepository>();
builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<RecipeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Repository API V1"));
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
