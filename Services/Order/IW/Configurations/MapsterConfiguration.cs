using IW.Models;
using IW.Models.DTOs.Item;
using IW.Models.Entities;
using Mapster;

namespace IW.Configurations
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .NewConfig<OrderItem, ItemDto>()
                .Ignore(dst => dst.Order);
            config
                .NewConfig<Order, OrderCreatedMessage>()
                .Map(dst => dst.Items, src => src.Items);
            config
                .Default
                .PreserveReference(true);
            config
                .NewConfig<ItemDto,OrderItem>()
                .Map(dst => dst.Subtotal,src=>src.Price*src.Quantity);
        }
    }
}
