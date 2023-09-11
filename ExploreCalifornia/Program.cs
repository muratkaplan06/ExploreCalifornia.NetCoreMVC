var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())//yani "development" dýþýndaysa"staging", ve "production" gibi farklý
                                     //çevreler vardýr
{
    app.UseExceptionHandler("/Error");
}

//Özellikle geliþtirme sýrasýnda, uygulamanýzýn hata verdiði durumlarý daha iyi anlamak ve
//sorunlarý hýzlýca teþhis etmek için bu middleware'i kullanabilirsiniz
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

if (builder.Configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions"))
{
    app.UseDeveloperExceptionPage();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("invalid"))
    {
        throw new Exception("Bu URL geçersiz");
    }

    await next();
});

app.UseStaticFiles();// Bu satýr, wwwroot dizinindeki statik dosyalarý sunar.
app.UseFileServer();// UseStaticFiles() ile ayný iþi yapar, ancak daha fazla seçenek sunar.
app.UseRouting();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseMvc(routes=>
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}")
    );

app.Run();
