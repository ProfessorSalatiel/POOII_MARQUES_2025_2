

var builder = WebApplication.CreateBuilder(args);

// Connection string - update with your DB server details
builder.Configuration["ConnectionStrings:DefaultConnection"] = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? "Server=localhost;Database=SuporteUnip;Trusted_Connection=True;";

builder.Services.AddRazorPages();
//builder.Services.AddScoped<ChamadoController>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();