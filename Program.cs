using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using XefiAcademyAPI.Model;


var builder = WebApplication.CreateBuilder(args); string MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; // Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://172.16.2.98:5500")
                            .AllowAnyHeader()
                            .AllowAnyMethod(); 
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); var app = builder.Build(); app.UseCors(MyAllowSpecificOrigins); 
app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () =>
{
    return "HELLOOOOOOOOOOOOOOOOw";
})
.WithName("HelloWord")
.WithOpenApi();

// -------------------------- CONNAISSANCES -------------------------------------------------------------------------------------------

// CREATE Connaissance
app.MapPut("/CreateConnaissance", (ConnaissancesForeCastEntitity fc) =>
{

    var ok = new ConnaissancesForeCastRepo(builder.Configuration).CreateConnaissance(fc);
    fc.IdConnaissance = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Connaissances"); 

// READ Connaissance
app.MapGet("ReadConnaissance/{id:int}", (int id) =>
{  
    var fc = new ConnaissancesForeCastRepo(builder.Configuration).GetConnaissance(id);
    return fc.IdConnaissance == 0 ? Results.NotFound() : Results.Ok(fc) ;

}).WithTags("Connaissances");

// READ ALL Connaissance
app.MapGet("ReadAllConnaissance/", () =>
{
    return new ConnaissancesForeCastRepo(builder.Configuration).GetAllConnaissance();

}).WithTags("Connaissances");


// UPDATE Connaissance
app.MapPost("/UpdateConnaissance", (ConnaissancesForeCastEntitity fc) => {
   
   var ok =  new ConnaissancesForeCastRepo(builder.Configuration).UpdateConnaissances(fc);
   return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
   
}).WithTags("Connaissances");

//Delete Connaissance
app.MapDelete("/DeleteConnaissance/{id:int}", (int id) =>
{

var ok = new ConnaissancesForeCastRepo(builder.Configuration).DeleteConnaissance(id);
return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Connaissances");

// -------------------------- PROJETS -----------------------------------------------------------------------------------------------------

//Create Projet
app.MapPut("/CreateProjet", (ProjetsForeCastEntitity fc) =>
{

    var ok = new ProjetsForeCastRepo(builder.Configuration).CreateProjet(fc);
    fc.IdProjet = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Projets");


// READ ALL Projets
app.MapGet("ReadAllProjet/", () =>
{
    return new ProjetsForeCastRepo(builder.Configuration).GetAllProjets();


}).WithTags("Projets");

// READ Projet
app.MapGet("/ReadProjet/{id:int}", (int id) => {

    var fc = new ProjetsForeCastRepo(builder.Configuration).GetProjet(id);
    return fc.IdProjet == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Projets");

// UPDARE Projet
app.MapPost("/UpdateProjet", (ProjetsForeCastEntitity fc) => {

    var ok = new ProjetsForeCastRepo(builder.Configuration).UpdateProjet(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Projets");

//Delete Projet
app.MapDelete("/DeleteProjet/{id:int}", (int id) =>
{

    var ok = new ProjetsForeCastRepo(builder.Configuration).DeleteProjet(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Projets");

// ------------------------------------------------------ CATEGORIES --------------------------------------------------------------------------------------

//Create Categorie
app.MapPut("/CreateCategorie", (CategoriesForeCastEntitity fc) =>
{

    var ok = new CategoriesForeCastRepo(builder.Configuration).CreateCategorie(fc);
    fc.IdCategorie = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Categories");

//READ Categorie
app.MapGet("/ReadCategorie/{id:int}", (int id) => {

    var fc = new CategoriesForeCastRepo(builder.Configuration).GetCategorie(id);
    return fc.IdCategorie == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Categories");

//READ ALL Categories
app.MapGet("/ReadAllCategories", () => {

    return new CategoriesForeCastRepo(builder.Configuration).GetAllCategories();

}).WithTags("Categories");

// UPDATE Categorie
app.MapPost("/UpdateCategorie", (CategoriesForeCastEntitity fc) => {

    var ok = new CategoriesForeCastRepo(builder.Configuration).UpdateProjet(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Categories");

//Delete Categorie
app.MapDelete("/DeleteCategorie/{id:int}", (int id) =>
{

    var ok = new CategoriesForeCastRepo(builder.Configuration).DeleteCategorie(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Categories");

// ------------------------------------------------- Types Ressources ------------------------------------------------------------------------------------


//Create Type ressource
app.MapPut("/CreateTypeRessource", (TypesRessourcesForeCastEntitity fc) =>
{

    var ok = new TypesRessourcesForeCastRepo(builder.Configuration).CreateTypeRessource(fc);
    fc.IdTypeRessource = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Types de ressource"); 

// READ Type Ressource
app.MapGet("ReadTypeRessource/{id:int}", (int id) =>
{
    var fc = new TypesRessourcesForeCastRepo(builder.Configuration).GetTypeRessource(id);
    return fc.IdTypeRessource == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Types de ressource"); 

//READ ALL Types Ressource
app.MapGet("/ReadAllTypeRessource", () => {

    return new TypesRessourcesForeCastRepo(builder.Configuration).GetAllTypeRessource();

}).WithTags("Types de ressource");

// UPDARE Type Ressource
app.MapPost("/UpdateTypeRessource", (TypesRessourcesForeCastEntitity fc) => {

    var ok = new TypesRessourcesForeCastRepo(builder.Configuration).UpdateTypeRessource(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Types de ressource");

//Delete Type Ressource
app.MapDelete("/DeleteTypeRessource/{id:int}", (int id) =>
{

    var ok = new CategoriesForeCastRepo(builder.Configuration).DeleteCategorie(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Types de ressource");


//---------------------------------------------------- Ressources -----------------------------------------------------------------

//Create Ressource
app.MapPut("/CreateRessource", (RessourcesForeCastEntitity fc) =>
{

    var ok = new RessourcesForeCastRepo(builder.Configuration).CreateRessource(fc);
    fc.IdTypeRessource = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Ressources");

// READ Ressource
app.MapGet("ReadRessources/{id:int}", (int id) =>
{
    var fc = new RessourcesForeCastRepo(builder.Configuration).GetRessource(id);
    return fc.IdRessource == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Ressources");

//READ ALL Ressources
app.MapGet("/ReadAllRessources", () => {

    return new RessourcesForeCastRepo(builder.Configuration).GetAllRessources();

}).WithTags("Ressources");

// UPDATE Ressource
app.MapPost("/UpdateRessource", (RessourcesForeCastEntitity fc) => {

    var ok = new RessourcesForeCastRepo(builder.Configuration).UpdateRessource(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Ressources");

//Delete Ressources
app.MapDelete("/DeleteRessource/{id:int}", (int id) =>
{

    var ok = new RessourcesForeCastRepo(builder.Configuration).DeleteRessource(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Ressources");

app.Run();





