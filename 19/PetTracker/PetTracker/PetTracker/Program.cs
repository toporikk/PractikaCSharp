var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку MVC (контроллеры с представлениями)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Настройка конвейера HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pets}/{action=Index}/{id?}");

app.Run();