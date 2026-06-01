var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);
var sql = builder.AddSqlServer("sql", port: 14333, password: sqlPassword).WithLifetime(ContainerLifetime.Persistent);
var db = sql.AddDatabase("SqlLocalizerStarterDb");

builder.AddProject<Projects.SqlLocalizerStarter_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WaitFor(db)
    .WithReference(db);

builder.Build().Run();
