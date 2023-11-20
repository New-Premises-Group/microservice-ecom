using IW.Models;
using IW.Models.DTOs;
using IW.Models.DTOs.User;
using IW.Models.Entities;
using Mapster;

namespace IW.Configurations
{
    /// <summary>
    /// Configuration file for global type adapter.Remember to load it in when unit Testing
    /// </summary>
    public class MapsterConfiguration : IRegister
    {
        /// <summary>
        /// Register your TypeAdapterConfig for global using.
        /// </summary>
        /// <param name = "config" ></ param >
        public void Register(TypeAdapterConfig config)
    {
        //config
        //    .NewConfig<OrderItem, ItemDto>()
        //    .Ignore(dst => dst.Order);
        //config
        //    .NewConfig<Order, OrderCreatedMessage>()
        //    .Map(dst => dst.Items, src => src.Items);
        //config
        //    .Default
        //    .PreserveReference(true);
        //config
        //    .NewConfig<ItemDto, OrderItem>()
        //    .Map(dst => dst.Subtotal, src => src.Price * src.Quantity);
        //config
        //    .NewConfig<ItemDto, OrderItem>()
        //    .Map(dst => dst.Subtotal, src => src.Price * src.Quantity);
        //config
        //    .NewConfig<CreateItem, OrderItem>()
        //    .Map(dst => dst.Subtotal, src => src.Price * src.Quantity);
        config
            .NewConfig<User, UserDto>()
            .Map(dst => dst.Address, src => src.Addresses!=null ? src.Addresses : null);
    }
}
}
