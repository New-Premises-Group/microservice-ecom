using IW.Common;
using IW.Interfaces.Payloads;

namespace IW.Models.DTOs.ItemPayloads
{
    public class ItemPayloadFactory:IItemPayloadFactory
    {
        private Dictionary<PAYLOAD_TYPE, IResponsePayload> storedPayload = new ();

        public int TotalObjectsCreated
        {
            get { return storedPayload.Count; }
        }

        public IResponsePayload GetResponsePayload(PAYLOAD_TYPE type)
        {
            if(storedPayload.ContainsKey(type))
            {
                return storedPayload[type];
            }
            switch (type)
            {
                case PAYLOAD_TYPE.Update:
                    {
                        var payload = new ItemUpdatedPayload("Item successfully updated"); ;
                        storedPayload.Add(type, payload);
                        return payload;
                    }

                case PAYLOAD_TYPE.Create:
                    {
                        var payload = new ItemCreatedPayload("Item successfully created"); ;
                        storedPayload.Add(type, payload);
                        return payload;
                    }

                case PAYLOAD_TYPE.Delete:
                    {
                        var payload = new ItemDeletedPayload("Item successfully deleted"); ;
                        storedPayload.Add(type, payload);
                        return payload;
                    }
                default:
                    return null;
            }
        }
    }
}
