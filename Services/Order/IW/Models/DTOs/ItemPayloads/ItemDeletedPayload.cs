using IW.Interfaces.Payloads;

namespace IW.Models.DTOs.ItemPayloads
{
    public class ItemDeletedPayload : IResponsePayload
    {
        private string _message;
        public ItemDeletedPayload(string message)
        {
            _message = message;
        }

        public string Message => _message;

        public string GetDetail(string detail)
        {
            return $"{{ message:{_message},detail:\"{detail}\" }}";
        }
    }
}

