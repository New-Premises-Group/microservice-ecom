using IW.Interfaces.Payloads;

namespace IW.Models.DTOs.ItemPayloads
{
    public class ItemCreatedPayload : IResponsePayload
    {
        private string _message;
        public ItemCreatedPayload(string message)
        {
            _message = message;
        }

        public string Message => _message;

        public string GetDetail(string detail)
        {
            return $"{{ {_message},detail:\"{detail}\" }}";
        }
    }
}
