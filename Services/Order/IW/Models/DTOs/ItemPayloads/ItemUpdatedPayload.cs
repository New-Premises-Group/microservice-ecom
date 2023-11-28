using IW.Interfaces.Payloads;

namespace IW.Models.DTOs.ItemPayloads
{
    public class ItemUpdatedPayload: IResponsePayload
    {
        private string _message;
        public ItemUpdatedPayload(string message)
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
