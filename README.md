# API C#
Ce projet est une API REST pour gérer les connaissances, projets et catégories d'une application interne à mon entreprise.

# Prérequis
.NET 5 SDK

# Installation
Cloner le projet
Ouvrir une ligne de commande à la racine du projet
Exécuter dotnet build pour construire le projet
Exécuter dotnet run pour lancer l'API

# Endpoints
## Connaissances
* PUT /CreateConnaissance : Crée une nouvelle connaissance
* GET /ReadConnaissance/{id} : Récupère une connaissance par ID
* GET /ReadAllConnaissance/ : Récupère toutes les connaissances
* POST /UpdateConnaissance : Met à jour une connaissance
* DELETE /DeleteConnaissance/{id} : Supprime une connaissance par ID

## Projets
* PUT /CreateProjet : Crée un nouveau projet
* GET /ReadProjet/{id} : Récupère un projet par ID
* GET /ReadAllProjet/ : Récupère tous les projets
* POST /UpdateProjet : Met à jour un projet
* DELETE /DeleteProjet/{id} : Supprime un projet par ID

## Catégories
* PUT /CreateCategorie : Crée une nouvelle catégorie
* GET /ReadCategorie/{id} : Récupère une catégorie par ID
* GET /ReadAllCategorie/ : Récupère toutes les catégories
* POST /UpdateCategorie : Met à jour une catégorie
* DELETE /DeleteCategorie/{id} : Supprime une catégorie par ID

# Swagger  
Vous pouvez explorer les endpoints de l'API en utilisant Swagger en accédant à l'URL https://localhost:[port]/swagger.

