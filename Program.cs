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
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod()
); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () =>
{
    return "Route non définit";
})
.WithName("HelloWord")
.WithOpenApi();

// -------------------------- AUTH -------------------------------------------------------------------------------------------

// Get Auth
app.MapPut("/Login", (IConfiguration _c, AuthEntitity fc) =>
{
    var auth = new Auth(_c);
    var utilisateur = auth.GetUtilisateur(fc.Email, fc.MotDePasse);

    if (utilisateur.MotDePasse != null)
    {
        return Results.Created($"/Login/", utilisateur);
    }
    else
    {
        return Results.Problem(new ProblemDetails { Detail = "L'authentification a échoué", Status = 401 });
    }
})
.WithTags("Auth");

// UPDATE Token
app.MapPost("/UpdateToken", (AuthEntitity fc) => {

    var ok = new Auth(builder.Configuration).UpdateToken(fc.IdUtilisateur, fc.Token);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Auth");


// Check Token
app.MapPost("/CheckToken", (AuthEntitity fc) => {

    return new Auth(builder.Configuration).CheckToken(fc.IdUtilisateur, fc.Token);

}).WithTags("Auth");
// -------------------------- CONNAISSANCES -------------------------------------------------------------------------------------------

// CREATE Connaissance
app.MapPut("/CreateConnaissance", (IConfiguration _c,ConnaissancesEntitity fc) =>
{

    var ok = new Connaissances(_c).CreateConnaissance(fc);
    fc.IdConnaissance = (int)ok;
    return (ok != -1) ? Results.Created($"ReadConnaissance/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Connaissances");

// NUMBER Connaissance
app.MapGet("/ReadNumberConnaissance", () =>
{
    return new Connaissances(builder.Configuration).ReadNumberConnaissances();

}).WithTags("Connaissances");

// READ Connaissance
app.MapGet("ReadConnaissance/{id:int}", (int id) =>
{  
    var fc = new Connaissances(builder.Configuration).GetConnaissance(id);
    return fc.IdConnaissance == 0 ? Results.NotFound() : Results.Ok(fc) ;

}).WithTags("Connaissances");

// READ ALL Connaissance
app.MapGet("ReadAllConnaissance/", () =>
{
    return new Connaissances(builder.Configuration).GetAllConnaissance();

}).WithTags("Connaissances");


// UPDATE Connaissance
app.MapPost("/UpdateConnaissance", (ConnaissancesEntitity fc) => {
   
   var ok =  new Connaissances(builder.Configuration).UpdateConnaissances(fc);
   return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
   
}).WithTags("Connaissances");

//Delete Connaissance
app.MapDelete("/DeleteConnaissance/{id:int}", (int id) =>
{

var ok = new Connaissances(builder.Configuration).DeleteConnaissance(id);
return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Connaissances");

// -------------------------- PROJETS -----------------------------------------------------------------------------------------------------

//Create Projet
app.MapPut("/CreateProjet", (ProjetsEntitity fc) =>
{

    var ok = new Projets(builder.Configuration).CreateProjet(fc);
    fc.IdProjet = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Projets");


// READ ALL Projets
app.MapGet("ReadAllProjet/", () =>
{
    return new Projets(builder.Configuration).GetAllProjets();


}).WithTags("Projets");

// READ Projet
app.MapGet("/ReadProjet/{id:int}", (int id) => {

    var fc = new Projets(builder.Configuration).GetProjet(id);
    return fc.IdProjet == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Projets");

// UPDARE Projet
app.MapPost("/UpdateProjet", (ProjetsEntitity fc) => {

    var ok = new Projets(builder.Configuration).UpdateProjet(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Projets");

//Delete Projet
app.MapDelete("/DeleteProjet/{id:int}", (int id) =>
{

    var ok = new Projets(builder.Configuration).DeleteProjet(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Projets");

// ------------------------------------------------------ CATEGORIES --------------------------------------------------------------------------------------

//Create Categorie
app.MapPut("/CreateCategorie", (CategoriesEntitity fc) =>
{

    var ok = new Categories(builder.Configuration).CreateCategorie(fc);
    fc.IdCategorie = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Categories");

//READ Categorie
app.MapGet("/ReadCategorie/{id:int}", (int id) => {

    var fc = new Categories(builder.Configuration).GetCategorie(id);
    return fc.IdCategorie == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Categories");

//READ ALL Categories
app.MapGet("/ReadAllCategories", () => {

    return new Categories(builder.Configuration).GetAllCategories();

}).WithTags("Categories");

//READ Number Catégories
app.MapGet("/ReadNumberCategorie", () => {

    return new Categories(builder.Configuration).ReadNumberCategories();

}).WithTags("Categories");

// UPDATE Categorie
app.MapPost("/UpdateCategorie", (CategoriesEntitity fc) => {

    var ok = new Categories(builder.Configuration).UpdateProjet(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Categories");

//Delete Categorie
app.MapDelete("/DeleteCategorie/{id:int}", (int id) =>
{

    var ok = new Categories(builder.Configuration).DeleteCategorie(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Categories");

// ------------------------------------------------- Types Ressources ------------------------------------------------------------------------------------


//Create Type ressource
app.MapPut("/CreateTypeRessource", (TypesRessourcesEntitity fc) =>
{

    var ok = new TypesRessources(builder.Configuration).CreateTypeRessource(fc);
    fc.IdTypeRessource = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Types de ressource"); 

// READ Type Ressource
app.MapGet("ReadTypeRessource/{id:int}", (int id) =>
{
    var fc = new TypesRessources(builder.Configuration).GetTypeRessource(id);
    return fc.IdTypeRessource == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Types de ressource"); 

//READ ALL Types Ressource
app.MapGet("/ReadAllTypeRessource", () => {

    return new TypesRessources(builder.Configuration).GetAllTypeRessource();

}).WithTags("Types de ressource");

// UPDARE Type Ressource
app.MapPost("/UpdateTypeRessource", (TypesRessourcesEntitity fc) => {

    var ok = new TypesRessources(builder.Configuration).UpdateTypeRessource(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Types de ressource");

//Delete Type Ressource
app.MapDelete("/DeleteTypeRessource/{id:int}", (int id) =>
{

    var ok = new Categories(builder.Configuration).DeleteCategorie(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Types de ressource");


//---------------------------------------------------- Ressources -----------------------------------------------------------------

//Create Ressource
app.MapPut("/CreateRessource", (RessourcesEntitity fc) =>
{

    var ok = new Ressources(builder.Configuration).CreateRessource(fc);
    fc.IdTypeRessource = ok;
    return (ok != -1) ? Results.Created($"/{ok}", fc) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });

}).WithTags("Ressources");

// READ Ressource
app.MapGet("ReadRessources/{id:int}", (int id) =>
{
    var fc = new Ressources(builder.Configuration).GetRessource(id);
    return fc.IdRessource == 0 ? Results.NotFound() : Results.Ok(fc);

}).WithTags("Ressources");

//READ ALL Ressources
app.MapGet("/ReadAllRessources", () => {

    return new Ressources(builder.Configuration).GetAllRessources();

}).WithTags("Ressources");

// UPDATE Ressource
app.MapPost("/UpdateRessource", (RessourcesEntitity fc) => {

    var ok = new Ressources(builder.Configuration).UpdateRessource(fc);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });

}).WithTags("Ressources");

//Delete Ressources
app.MapDelete("/DeleteRessource/{id:int}", (int id) =>
{

    var ok = new Ressources(builder.Configuration).DeleteRessource(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete  n'a pas marché", Status = 500 });


}).WithTags("Ressources");

// NUMBER Connaissance
app.MapGet("/ReadNumberRessources", () =>
{
    return new Ressources(builder.Configuration).ReadNumberRessources();

}).WithTags("Ressources");

app.Run();





