using BeajLearner.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            #region Repositories
            //services.AddTransient<ISupplierRepository, SupplierRepository>();
            //services.AddTransient<IDocumentRepository, DocumentRepository>();
            //services.AddTransient<ICategoryRepository, CategoryRepository>();
            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IArticleRepository, ArticleRepository>();
            //services.AddTransient<ISizeRepository, SizeRepository>();
            //services.AddTransient<ITaxRepository, TaxRepository>();
            //services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            //services.AddTransient<IOrderRepository, OrderRepository>();
            //services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            //services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            //services.AddTransient<ICustomerRepository, CustomerRepository>();


            #endregion

            #region Singleton

            //services.AddSingleton<IEncryptionService, EncryptionService>();
            #endregion Singleton
        }
    }
}
