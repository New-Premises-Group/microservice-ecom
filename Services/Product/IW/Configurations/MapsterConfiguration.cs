using IW.Models;
using IW.Models.DTOs;
using IW.Models.Entities;
using Mapster;

namespace IW.Configurations
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .Default
                .PreserveReference(false);
        }
    }
}
