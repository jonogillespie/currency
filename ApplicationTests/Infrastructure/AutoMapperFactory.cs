using Application.Infrastructure.AutoMapper;
using AutoMapper;

namespace ApplicationTests.Infrastructure
{
    public static class AutoMapperFactory
    {
        #region Other

        public static IMapper Create()
        {
            var mappingConfig = 
                new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });

            return mappingConfig.CreateMapper();
        }

        #endregion
    }
}