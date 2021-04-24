using AutoMapper;

namespace Application.Infrastructure.AutoMapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}