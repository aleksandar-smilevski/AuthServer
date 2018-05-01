using System;
using System.Linq;
using AuthServer.API.Database;
using AuthServer.API.Dto;
using AuthServer.API.Models;
using AuthServer.API.Repositories.Author;
using AuthServer.API.Repositories.Book;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AuthServer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<UsersDbContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("UsersDatabase")));
            
           
            services.AddScoped<IAuthorRepository,AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AuthServer.API", Version = "v1" });
            });
            
            services.AddCors();
            
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>()
                    .ForMember(x => x.Books, opt => opt.MapFrom(src => src.Books.Select(x => x.Book).ToList()));
                cfg.CreateMap<Book, BookPreviewDto>();
                cfg.CreateMap<AuthorDto, Author>().ForMember(x => x.Books, opt => opt.Ignore());
                cfg.CreateMap<BookPreviewDto, Book>().ForMember(x => x.Authors, opt => opt.Ignore())
                    .ForMember(x => x.Orders, opt => opt.Ignore());
                
                cfg.CreateMap<Book, BookDto>()
                    .ForMember(x => x.Authors, opt => opt.MapFrom(src => src.Authors.Select(x => x.Author).ToList()));
                cfg.CreateMap<BookDto, Book>().ForMember(x => x.Authors, opt => opt.Ignore())
                    .ForMember(x => x.Orders, opt => opt.Ignore());
                cfg.CreateMap<Author, AuthorPreviewDto>();
                cfg.CreateMap<AuthorPreviewDto, Author>().ForMember(x => x.Books, opt => opt.Ignore());
            });
            
            services.AddAuthentication("Bearer") 
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "AuthServer.API";
                });
           
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("FullAccess", builder =>
                    {
                        builder.RequireScope(JwtClaimTypes.Scope, "AuthServer.Full");
                    });
                
                opts.AddPolicy("ReadOnly", builder =>
                    {
                        builder.AddAuthenticationSchemes("Bearer").RequireAssertion(handler =>
                            {
                                return handler.User.HasClaim(x =>
                                    x.Type == JwtClaimTypes.Scope && x.Value == "AuthServer.ReadOnly");
                            });
                    });
                
            });
            
            Mapper.AssertConfigurationIsValid();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer.API");
            });

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
            });
            

            app.UseMvc();
        }
    }
}