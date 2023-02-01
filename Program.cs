using Microsoft.OpenApi.Models;
using open_ai_example.ai.Base;
using open_ai_example.ai.Completions;
using open_ai_example.ai.Completions.Transcripts;
using open_ai_example.Config;
using open_ai_example.Controllers;
using open_ai_example.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ModelDbConfig>(
    builder.Configuration.GetSection("ModelDbConfig"));

builder.Services.AddSingleton<OpenAICompletionService>();
builder.Services.AddSingleton<OpenAIServiceProvider>();


builder.Services.AddSingleton<OpenAIChatTranscriptService>();
builder.Services.AddSingleton<OpenAIModelService>();
builder.Services.AddSingleton<OpenAIChatService>();

builder.Services.AddSingleton<ChatTranscriptRepository>();
builder.Services.AddSingleton<ModelRepository>();


builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "OpenAI Example API",
//        Description = "A dotnet application to experiment with OpenAI"
//    });
//    var filePath = Path.Combine(System.AppContext.BaseDirectory, "open-ai-example.xml");
//    options.IncludeXmlComments(filePath);
//});

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.InputFormatters.Add(new TextPlainInputFormatter());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

