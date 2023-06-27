using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BoardDb>(opt => opt.UseInMemoryDatabase("Boards"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/board/{id}", async (string id, BoardDb db) => 
    await db.Boards.FindAsync(id)
        is Board board
            ? Results.Ok(board)
            : Results.NotFound());

app.MapPost("/board", async (Board board, BoardDb db) => 
{
    db.Boards.Add(board);
    await db.SaveChangesAsync();

    return Results.Created($"/board/{board.Id}", new { Board = "" });
})
.WithName("CreateBoard")
.WithOpenApi();

app.Run();
