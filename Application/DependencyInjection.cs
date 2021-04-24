using System.Reflection;
using Application.Infrastructure.AutoMapper;
using Application.Infrastructure.Mediator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(DependencyInjection).GetTypeInfo()
                    .Assembly);
            services
                .AddMediatorPipelineBehaviours();

            services
                .AddValidatorsFromAssembly(typeof(AutoMapperProfile).GetTypeInfo()
                    .Assembly);

            services
                .AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo()
                    .Assembly);

            services
                .AddMemoryCache();
        }

        private static void AddMediatorPipelineBehaviours(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IPipelineBehavior<,>),
                    typeof(LogExceptionsBehaviour<,>));
            services
                .AddTransient(typeof(IPipelineBehavior<,>),
                    typeof(RequestValidationBehavior<,>));
            services
                .AddTransient(typeof(IPipelineBehavior<,>),
                    typeof(LogAllRequestsBehavior<,>));
            services
                .AddTransient(typeof(IPipelineBehavior<,>),
                    typeof(RequestPerformanceBehaviour<,>));
            services
                .AddTransient(typeof(IPipelineBehavior<,>),
                    typeof(MemoryCacheBehaviour<,>));
           
        }
    }
}