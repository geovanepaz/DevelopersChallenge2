using API.Configuration;
using API.ViewQuery;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.Login;
using Application.ViewModels.Validations;
using Domain.Interfaces;
using Domain.Services;
using FluentValidation;
using Infra.Contexts;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            //Validator 
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
            services.AddTransient<IValidator<UserRequest>, UserValidator>();
            services.AddTransient<IValidator<UploadQuery>, UploadQueryValidator>();

            //Repository
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            //Appsercice
            services.AddScoped<ITransactionAppService, TransactionAppService>();

            //Service
            services.AddScoped<IOfxService, OfxService>();

            //Context
            services.AddScoped<DefaultContext>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}
