using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using System;
using System.Xml.Linq;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

////routing to a Localhost port return Hello World!
app.MapGet("/", () => "Hello World!");

////return an json object
//app.MapGet("/", () =>
//{
//    return new
//    {
//        Name = "John Doe",
//        Age = 30,
//        Occupation = "Software Developer"
//    };
//});


//basic route parameter
app.MapGet("/person/{name}", (string name) =>
{
    return $"hello {name}";
});


// Multiple Route Parameters 
app.MapGet("/person/{name}/{age}", (string name, int age) =>
{
    return new { Name = name, Age = age };
});


// Route Constraints (Type Enforcement)
app.MapGet("/product/{id:int}", (int id) =>
{
    return $"Product ID: {id}";
});

//// A 32-bit signed integer reserves 1 bit for sign and 31 bits for value, so it can represent up to 2³¹−1. When a decimal number is converted to binary, it must fit within those 31 value bits. The digit count is irrelevant; only the binary size matters.
//// Although ASCII needs only 7 bits, characters are stored byte-aligned in memory. So in a 32-bit block (4 bytes), only 4 ASCII characters can fit.


//// Optional Route Parameter
app.MapGet("/hello/{name?}", (string? name) =>
{
    return $"Hello {name ?? "Guest"}";
});

//// Combine Route + Query Parameter 
///  /order/10?status=shipped 
app.MapGet("/order/{id}", (int id, string? status) =>
{
    return new { OrderId = id, Status = status };
});

////1. Basic POST That Accepts JSON
///
///Step 1: Create a Model
//record Person(string Name, int Age);  // after app.Run() 

//Step 2: Add a POST Endpoint

app.MapPost("/persons", (Person person) =>
{
    return Results.Ok(new
    {
        Message = "Persons received",
        Data = person
    });
});

///calling
//curl - X POST https://localhost:5001/person \
//-H "Content-Type: application/json" \
//-d "{\"name\":\"Alice\",\"age\":25}"


////Proper REST Pattern Example
//app.MapGet("/person/{name}", (string name) =>
//{
//    return Results.Ok(new Person(name, 25));
//});

//app.MapPost("/person", (Person person) =>
//{
//    return Results.Created($"/person/{person.Name}", person);
//});
app.Run();

record Person(string Name, int Age);