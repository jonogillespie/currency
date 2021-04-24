using System.Reflection;
using AutoMapper;

namespace Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            LoadCustomMappings();
        }

        private void LoadCustomMappings()
        {
            var mapsFrom =
                MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
                map.CreateMappings(this);
        }
    }
}