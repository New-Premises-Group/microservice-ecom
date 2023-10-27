using IW.Models;
using IW.Models.DTOs;
using IW.Models.Entities;
using Mapster;

namespace IW.Configurations
{
    /// <summary>
    /// Configuration file for global type adapter. Remember to load it in when unit Testing
    /// </summary>
    public class MapsterConfiguration : IRegister
    {
        /// <summary>
        /// Register your TypeAdapterConfig for global using.
        /// </summary>
        /// <param name="config"></param>
        public void Register(TypeAdapterConfig config)
        {
            config
                .Default
                .PreserveReference(false);
        }
    }
}
