using Shop.Bll.Abstract.IManager;
using Shop.Bll.Concrete.Manager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using Shop.Dal.Concrete.EntityFramework.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddSwaggerDocument(opt =>
{
    opt.PostProcess = doc =>
    {
        doc.Info.Title = "Shop Discount API";
        doc.Info.Description = "API service for store discounts";
    };
});
builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/swagger", "swagger");
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseOpenApi();
app.UseSwaggerUi3(s =>
{
    s.DocumentTitle = "Shop Discount API";
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();