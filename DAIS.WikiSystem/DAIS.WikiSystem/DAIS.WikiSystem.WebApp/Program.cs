using DAIS.WikiSystem.Repository;
using DAIS.WikiSystem.Repository.Implementation.Category;
using DAIS.WikiSystem.Repository.Implementation.Collection;
using DAIS.WikiSystem.Repository.Implementation.CollectionDocument;
using DAIS.WikiSystem.Repository.Implementation.Document;
using DAIS.WikiSystem.Repository.Implementation.DocumentTag;
using DAIS.WikiSystem.Repository.Implementation.DocumentVersion;
using DAIS.WikiSystem.Repository.Implementation.Tag;
using DAIS.WikiSystem.Repository.Implementation.User;
using DAIS.WikiSystem.Repository.Interfaces;
using DAIS.WikiSystem.Repository.Interfaces.Collection;
using DAIS.WikiSystem.Repository.Interfaces.CollectionDocument;
using DAIS.WikiSystem.Repository.Interfaces.Document;
using DAIS.WikiSystem.Repository.Interfaces.DocumentTag;
using DAIS.WikiSystem.Repository.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Repository.Interfaces.Tag;
using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.Implementation.Authentication;
using DAIS.WikiSystem.Services.Implementation.Category;
using DAIS.WikiSystem.Services.Implementation.Collection;
using DAIS.WikiSystem.Services.Implementation.Document;
using DAIS.WikiSystem.Services.Implementation.DocumentVersion;
using DAIS.WikiSystem.Services.Implementation.Tag;
using DAIS.WikiSystem.Services.Implementation.User;
using DAIS.WikiSystem.Services.Interfaces.Authentication;
using DAIS.WikiSystem.Services.Interfaces.Category;
using DAIS.WikiSystem.Services.Interfaces.Collection;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.Tag;
using DAIS.WikiSystem.Services.Interfaces.User;

namespace DAIS.WikiSystem.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
            builder.Services.AddScoped<ICollectionDocumentRepository, CollectionDocumentRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IDocumentVersionsRepository, DocumentVersionRepository>();
            builder.Services.AddScoped<IDocumentTagRepository, DocumentTagRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICollectionService, CollectionService>();
            builder.Services.AddScoped<IDocumentVersionService, DocumentVersionService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IUserService, UserService>();

            ConnectionFactory.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
