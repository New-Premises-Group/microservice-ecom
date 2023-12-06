using IW.Common;

namespace IW.Interfaces.Payloads
{
    public interface IPayloadFactory
    {
        public int TotalObjectsCreated { get; }
        public IResponsePayload GetResponsePayload(PAYLOAD_TYPE type);
    }
}
