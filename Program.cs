using MudBlazor.Services;
using SoloX.BlazorJsBlob;
using Syncfusion.Blazor;
using WordReviser.Components;
using WordReviser.Components.Services;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Dependency Injection 
builder.Services.AddSingleton<IFileUploadService, FileUploadService>();
builder.Services.AddSingleton<IDirectoryManageService, DirectoryManageService>();
builder.Services.AddSingleton<IHtmlManageService, HtmlManageService>();
builder.Services.AddSingleton<ITextReviseService, TextReviseService>();

builder.Services.AddJsBlob();
builder.Services.AddMudServices();
builder.Services.AddBlazorBootstrap();
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
