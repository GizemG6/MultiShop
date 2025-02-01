using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOderingServices;
using Newtonsoft.Json.Linq;

namespace MultiShop.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IBasketService, BasketService>();

            builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>();

            builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>();

            builder.Services.AddHttpClient<IDiscountService, DiscountService>();

            builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>();

            builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>();

            builder.Services.AddHttpClient<ICategoryService, CategoryService>();

            builder.Services.AddHttpClient<IProductService, ProductService>();

            builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>();

            builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>();

            builder.Services.AddHttpClient<IFeatureService, FeatureService>();

            builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>();

            builder.Services.AddHttpClient<IBrandService, BrandService>();

            builder.Services.AddHttpClient<IAboutService, AboutService>();

            builder.Services.AddHttpClient<IProductImageService, ProductImageService>();

            builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>();

            builder.Services.AddHttpClient<ICommentService, CommentService>();

            builder.Services.AddHttpClient<IContactService, ContactService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
