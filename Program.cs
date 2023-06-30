using Hanjie.Contexts;
using Hanjie.Repositories;
using Hanjie.Services;
using Dapper;

// builder
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// init services
var policyName = "MyAllowSpecificOrigins";
services.AddCors(opts => {
    opts.AddPolicy(name: policyName,
        policy => {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure db
services.Configure<DbSettings>(builder.Configuration.GetSection("PostgresDbSettings"));

// custom services
services.AddControllers()
    .AddNewtonsoftJson();
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
app.UseCors(policyName);

app.MapControllers();

app.Run();
