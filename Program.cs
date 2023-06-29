using Hanjie.Operations;
using Hanjie.Contexts;
using Hanjie.Models;
using Hanjie.Repositories;
using Hanjie.Services;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// init services
services.AddCors();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure db
services.Configure<DbSettings>(builder.Configuration.GetSection("PostgresDbSettings"));

// custom services
services.AddScoped<IDataContext, PostgreSqlDataContext>();
services.AddScoped<IHanjieRepository, HanjiePostgresRepository>();
services.AddScoped<IHanjieService, HanjieService>();

// custom other
SqlMapper.AddTypeHandler(new GenericMultiArrayHandler<int>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/board/{id}", async (string id, IHanjieService hanjiService) => 
    await hanjiService.GetBoard(id) 
        is PostgresBoard board 
        ? Results.Ok(board)
        : Results.NotFound()
);

app.MapPost("/board", async (BoardCreationOptions opts, IHanjieService hanjiService) => 
{
    // create board
    Board newBoard = BoardFactory.CreateRandomBoard(opts);

    // try save board in db
    // await hanjiService.TrySaveBoard(newBoard);

    return Results.Created($"/board/{newBoard.Id}", new { Board = newBoard });
})
.WithName("CreateBoard")
.WithOpenApi();

app.Run();
