using Demo_Grapesjs.Core.DBContext;
using Demo_Grapesjs.Core.Mapper;
using Demo_Grapesjs.Helper;
using Demo_Grapesjs.Models;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Demo_Grapesjs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
            options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            builder.Logging.AddFile(builder.Configuration.GetSection("Logging"));


            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<INameCardTemplateService, NameCardTemplateService>();
            builder.Services.AddScoped<IVideoService, VideoService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserNameCardService, UserNameCardService>();
            builder.Services.AddScoped<IQRCodeGeneratorHelper, QRCodeGeneratorHelper>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                }
            });

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
