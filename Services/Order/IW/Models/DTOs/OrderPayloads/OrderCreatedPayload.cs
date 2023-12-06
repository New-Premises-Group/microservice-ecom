﻿using IW.Interfaces.Payloads;

namespace IW.Models.DTOs.OrderPayloads
{
    public class OrderCreatedPayload : IResponsePayload
    {
        private string _message;
        public OrderCreatedPayload(string message)
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
