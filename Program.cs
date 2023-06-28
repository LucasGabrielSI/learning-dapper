﻿using System.Data;
using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=G1jQn4O6I%Xg;Trusted_Connection=False;TrustServerCertificate=True;";

using(var connection = new SqlConnection(connectionString))
{
    CreateCategory(connection);
    UpdateCategory(connection);
    DeleteCategory(connection);
    CreateManyCategories(connection);
    ListCategories(connection);
    ExecuteProcedure(connection);
}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach(var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var category = new Category
    {
        Id = Guid.NewGuid(),
        Title = "Azure",
        Url = "azure.com",
        Summary = "Azure",
        Order = 8,
        Description = "Serviços Azure",
        Featured = true
    };

    var insertSql = @"INSERT INTO [Category] VALUES (
        @Id, 
        @Title, 
        @Url, 
        @Summary,
        @Order, 
        @description, 
        @featured
    )";

    var rows = connection.Execute(insertSql, new {
        category.Id, 
        category.Title, 
        category.Url, 
        category.Summary, 
        category.Order, 
        category.Description, 
        category.Featured
    });

    Console.WriteLine($"{rows} linha(s) inseridas");
}

static void UpdateCategory(SqlConnection connection)
{
    var updateSql = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
    var rows = connection.Execute(updateSql, new {
        id="10ac5463-9d0e-4c5c-a444-ddf340ca7ab4",
        title="Google Cloud"
    });

    Console.WriteLine($"{rows} registro(s) atualizado(s)");
}

static void DeleteCategory(SqlConnection connection)
{
    var deleteSql = "DELETE FROM [Category] WHERE [Id]=@id";
    var rows = connection.Execute(deleteSql, new {
        id="10ac5463-9d0e-4c5c-a444-ddf340ca7ab4"
    });

    Console.WriteLine($"{rows} registro(s) afetado(s)");
}

static void CreateManyCategories(SqlConnection connection)
{
    var category = new Category
    {
        Id = Guid.NewGuid(),
        Title = "Meta Projects",
        Url = "meta-projects",
        Summary = "Meta",
        Order = 8,
        Description = "Meta Projects",
        Featured = true
    };

    var category2 = new Category
    {
        Id = Guid.NewGuid(),
        Title = "Categoria 2",
        Url = "categoria-dois",
        Summary = "categoria",
        Order = 8,
        Description = "Categoria 2",
        Featured = false
    };

    var insertSql = @"INSERT INTO [Category] VALUES (
        @Id, 
        @Title, 
        @Url, 
        @Summary,
        @Order, 
        @description, 
        @featured
    )";

    var rows = connection.Execute(insertSql, new[] {
        new {
            category.Id, 
            category.Title, 
            category.Url, 
            category.Summary, 
            category.Order, 
            category.Description, 
            category.Featured
        },
        new {
            category2.Id, 
            category2.Title, 
            category2.Url, 
            category2.Summary, 
            category2.Order, 
            category2.Description, 
            category2.Featured
        }
    });

    Console.WriteLine($"{rows} linha(s) inserida(s)");
}

static void ExecuteProcedure(SqlConnection connection)
{
    var Procedure = "[spDeleteStudent]"; 

    var pars = new { StudentId = "aee0caa7-53f3-4566-93a5-66c7be5032d7" };
    
    var affectedRows = connection.Execute(
        Procedure, 
        pars, 
        commandType: CommandType.StoredProcedure
    );

    Console.WriteLine($"{affectedRows} registro(s) afetado(s)");
}
