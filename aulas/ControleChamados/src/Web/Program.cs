using Controller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// injeta o controller com a connection string do appsettings
builder.Services.AddScoped(sp =>
    new ChamadoController(builder.Configuration.GetConnectionString("PROFSALATIEL")!));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
