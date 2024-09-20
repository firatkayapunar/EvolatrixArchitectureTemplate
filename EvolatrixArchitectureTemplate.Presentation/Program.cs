using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var contentRootPath = builder.Environment.ContentRootPath;

var directoryInfo = new DirectoryInfo(contentRootPath);

while (directoryInfo != null && !Directory.GetFiles(directoryInfo.FullName, "*.sln", SearchOption.TopDirectoryOnly).Any())
{
    directoryInfo = directoryInfo.Parent;
}

if (directoryInfo == null)
    throw new Exception("Solution (.sln) file not found.");

var solutionFile = Directory.GetFiles(directoryInfo.FullName, "*.sln", SearchOption.TopDirectoryOnly).FirstOrDefault();
var solutionName = Path.GetFileNameWithoutExtension(solutionFile);

var projectRootPath = directoryInfo.FullName;

var persistencePath = Path.Combine(projectRootPath, solutionName + ".Persistence", "bin", "Debug", "net6.0", $"{solutionName}.Persistence.dll");
var persistenceContractPath = Path.Combine(projectRootPath, solutionName + ".PersistenceContract", "bin", "Debug", "net6.0", $"{solutionName}.PersistenceContract.dll");
var domainPath = Path.Combine(projectRootPath, solutionName + ".Domain", "bin", "Debug", "net6.0", $"{solutionName}.Domain.dll");
var businessPath = Path.Combine(projectRootPath, solutionName + ".Business", "bin", "Debug", "net6.0", $"{solutionName}.Business.dll");

builder.Configuration["Assemblies:PersistencePath"] = persistencePath;
builder.Configuration["Assemblies:PersistenceContractPath"] = persistenceContractPath;
builder.Configuration["Assemblies:DomainPath"] = domainPath;
builder.Configuration["Assemblies:BusinessPath"] = businessPath;

var persistenceAssembly = Assembly.LoadFrom(builder.Configuration.GetSection("Assemblies:PersistencePath").Value);
var businessAssembly = Assembly.LoadFrom(builder.Configuration.GetSection("Assemblies:BusinessPath").Value);
var persistenceContractAssembly = Assembly.LoadFrom(builder.Configuration.GetSection("Assemblies:PersistenceContractPath").Value);
var domainAssembly = Assembly.LoadFrom(builder.Configuration.GetSection("Assemblies:DomainPath").Value);

RegisterDIContainer(persistenceAssembly, businessAssembly, persistenceContractAssembly, domainAssembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterDIContainer(Assembly persistenceAssembly, Assembly businessAssembly, Assembly persistenceContractAssembly, Assembly domainAssembly)
{
    var genericRepositoryInterfaceType = persistenceContractAssembly.GetTypes()
     .FirstOrDefault(t => t.IsInterface && t.Name == "IRepository`1");

    var genericRepositoryImplementationType = persistenceAssembly.GetTypes()
        .FirstOrDefault(t => t.IsClass && t.Name == "Repository`1");

    if (genericRepositoryInterfaceType == null || genericRepositoryImplementationType == null)
    {
        throw new Exception("Generic repository types could not be found in the provided assemblies.");
    }

    var entityTypes = domainAssembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract); 

    foreach (var entityType in entityTypes)
    {
        var genericCustomRepositoryInterfaceType = persistenceContractAssembly.GetTypes()
            .FirstOrDefault(t => t.IsInterface && t.Name == $"I{entityType.Name}Repository`1");
        var genericCustomRepositoryImplementationType = persistenceAssembly.GetTypes()
            .FirstOrDefault(t => t.IsClass && t.Name == $"{entityType.Name}Repository`1");

        if (genericCustomRepositoryInterfaceType != null && genericCustomRepositoryImplementationType != null)
        {
            var genericCustomRepositoryInterface = genericCustomRepositoryInterfaceType.MakeGenericType(entityType);
            var genericCustomRepositoryImplementation = genericCustomRepositoryImplementationType.MakeGenericType(entityType);
            builder.Services.AddScoped(genericCustomRepositoryInterface, genericCustomRepositoryImplementation);
        }

        var genericRepositoryInterface = genericRepositoryInterfaceType.MakeGenericType(entityType);
        var genericRepositoryImplementation = genericRepositoryImplementationType.MakeGenericType(entityType);
        builder.Services.AddScoped(genericRepositoryInterface, genericRepositoryImplementation);
    }

    var dbContextType = persistenceAssembly.GetTypes().FirstOrDefault(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(DbContext)));

    if (dbContextType == null)
    {
        throw new Exception("DbContext not found in the provided assembly.");
    }

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var addDbContextMethods = typeof(EntityFrameworkServiceCollectionExtensions)
     .GetMethods()
    .Where(m => m.Name == "AddDbContext" && m.IsGenericMethodDefinition)
    .ToList();

    var addDbContextMethod = addDbContextMethods
       .FirstOrDefault(addDbContextMethod =>
       {
           var parameters = addDbContextMethod.GetParameters();
           return parameters.Length == 4
                  && parameters[0].ParameterType == typeof(IServiceCollection)
                  && parameters[1].ParameterType == typeof(Action<DbContextOptionsBuilder>)
                  && parameters[2].ParameterType == typeof(ServiceLifetime)
                  && parameters[3].ParameterType == typeof(ServiceLifetime);
       });

    if (addDbContextMethod == null)
    {
        throw new Exception("AddDbContext method not found.");
    }

    var genericAddDbContextMethod = addDbContextMethod.MakeGenericMethod(dbContextType);

    genericAddDbContextMethod.Invoke(null, new object[] {
        builder.Services,
        (Action<DbContextOptionsBuilder>)(options => options.UseSqlServer(connectionString)),
        ServiceLifetime.Scoped,
        ServiceLifetime.Scoped
    });

    builder.Services.AddMediatR(businessAssembly);
}
