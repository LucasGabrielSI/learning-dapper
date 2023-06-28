using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=G1jQn4O6I%Xg;Trusted_Connection=False;TrustServerCertificate=True;";

using(var connection = new SqlConnection(connectionString))
{
    Console.WriteLine("Conectado!");
    // ADO.NET
    // connection.Open();

    // using (var command = new SqlCommand())
    // {
    //     command.Connection = connection;
    //     command.CommandType = System.Data.CommandType.Text;
    //     command.CommandText = "SELECT [Id], [Title] FROM [Category]";

    //     var reader = command.ExecuteReader();
    //     while (reader.Read())
    //     {
    //         Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
    //     }
    // };
    ListCategories(connection);
    CreateCategory(connection);
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