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

    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach(var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Title}");
    }
}