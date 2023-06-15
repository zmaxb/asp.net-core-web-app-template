using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    c.DescribeAllParametersInCamelCase();
    c.EnableAnnotations();

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Application API",
        Description = "Description API",
        Contact = new OpenApiContact
        {
            Name = "Author",
            Email = "%@%.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.InjectStylesheet("/css/custom-swagger-ui.css");
    });
}

app.UseStaticFiles();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();