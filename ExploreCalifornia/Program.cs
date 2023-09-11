var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())//yani "development" d���ndaysa"staging", ve "production" gibi farkl�
                                     //�evreler vard�r
{
    app.UseExceptionHandler("/Error");
}

//�zellikle geli�tirme s�ras�nda, uygulaman�z�n hata verdi�i durumlar� daha iyi anlamak ve
//sorunlar� h�zl�ca te�his etmek i�in bu middleware'i kullanabilirsiniz
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
        throw new Exception("Bu URL ge�ersiz");
    }

    await next();
});

app.UseStaticFiles();// Bu sat�r, wwwroot dizinindeki statik dosyalar� sunar.
app.UseFileServer();// UseStaticFiles() ile ayn� i�i yapar, ancak daha fazla se�enek sunar.
app.UseRouting();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseMvc(routes=>
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}")
    );

app.Run();
