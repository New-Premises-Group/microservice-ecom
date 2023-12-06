using IW.Common;
using IW.Interfaces.Payloads;
using IW.Models.DTOs.OrderPayloads;

namespace IW.Models.DTOs.OrderPayloads
{
    public class OrderPayloadFactory : IOrderPayloadFactory
    {
        private Dictionary<PAYLOAD_TYPE, IResponsePayload> storedPayload = new();

        public int TotalObjectsCreated
        {
            get { return storedPayload.Count; }
        }

        public IResponsePayload GetResponsePayload(PAYLOAD_TYPE type)
        {
            if (storedPayload.ContainsKey(type))
            {
                return storedPayload[type];
            }
            switch (type)
            {
                case PAYLOAD_TYPE.Update:
                    {
                        var payload = new OrderUpdatedPayload("Order successfully updated"); ;
                        storedPayload.Add(type, payload);
                        return payload;
                    }

                case PAYLOAD_TYPE.Create:
                    {
                        var payload = new OrderCreatedPayload("Order successfully created"); ;
                        storedPayload.Add(type, payload);
                        return payload;
                    }

                case PAYLOAD_TYPE.Delete:
                    {
                        var payload = new OrderDeletedPayload("Order successfully deleted"); ;
                        storedPayload.Add(type, payload);
                        return payload;
                    }
                default:
                    return null;
            }
        }
    }
}
