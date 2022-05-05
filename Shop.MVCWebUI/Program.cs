using Shop.Bll.Abstract.IManager;
using Shop.Bll.Concrete.Manager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using Shop.Dal.Concrete.EntityFramework.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}");
app.Run();