using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Principal;
using System.Text;
using WebServer.Data;
using WebServer.Helpers;
using WebServer.Interfaces;
using WebServer.Models;
using WebServer.Reposotory;

namespace WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var environment = builder.Environment;

            #region CORS настройки
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    p =>
                    {
#if DEBUG
                        p.AllowAnyOrigin()
#else
                        p.AllowAnyOrigin()
                        //p.WithOrigins("http://85.159.27.162")
#endif
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        //.WithMethods("GET", "POST", "PUT", "DELETE");
                    });
            });
            #endregion
            #region авторизации
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("tokenParams").GetSection("validIssuer").Value,
                    ValidAudience = configuration.GetSection("tokenParams").GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("tokenParams").GetSection("symKey").Value)),
                };
            });
            #endregion
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<WaterDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            #region DI св¤зи
            builder.Services.AddScoped(typeof(IAccount), typeof(AccountRepository));
            builder.Services.AddScoped(typeof(Interfaces.IRefKato), typeof(Reposotory.RefKatoRepository));
            builder.Services.AddScoped(typeof(Interfaces.IForms), typeof(Reposotory.FormRepository));
            builder.Services.AddScoped(typeof(Interfaces.IFormItem), typeof(Reposotory.FormItemRepository));
            builder.Services.AddScoped(typeof(Interfaces.IFormItemColumn), typeof(Reposotory.FormItemColumnRepository));
            builder.Services.AddScoped(typeof(Interfaces.IRefs), typeof(Reposotory.RefsRepository));
            builder.Services.AddScoped(typeof(Interfaces.IReport), typeof(Reposotory.ReportRepository));
            builder.Services.AddScoped(typeof(Interfaces.IData), typeof(Reposotory.DataRepository));
            builder.Services.AddScoped(typeof(Interfaces.ISeloForms), typeof(Reposotory.SeloFormsRepository));
            #endregion
            #region Swagger
            builder.Services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Water", Version = "v1" });
                sw.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "давай валидный такен.Bearer перед токеном писать не нужно",
                    Name = "афратизация",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                sw.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });
            #endregion
            var app = builder.Build();
            #region загрузка справочников,пользователей и тд тп...
            using (var scope = app.Services.CreateScope())
            {
                #if DEBUG
                var dbCtx = scope.ServiceProvider.GetRequiredService<WaterDbContext>();
                dbCtx.Database.EnsureCreated();
                var helper = new DatabaseHelper();
                helper.InitializeRefs(dbCtx, environment); //справочники
                helper.InitializeDefaultUsers(dbCtx, environment); //пользователи
                helper.InitializeDefaultForms(dbCtx, environment); //формы
                helper.InitializeFunctions(dbCtx, environment); //Инициализация функций
                #endif
            }
            #endregion
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            if (app.Environment.IsDevelopment())
            {
            }
            else
            {
                //app.UseHsts();
                //app.UseHttpsRedirection();
            }

            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
